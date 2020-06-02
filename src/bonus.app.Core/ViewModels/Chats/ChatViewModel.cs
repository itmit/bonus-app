using System;
using System.Linq;
using System.Threading.Tasks;
using bonus.app.Core.Models;
using bonus.app.Core.Services;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.Permissions.Abstractions;

namespace bonus.app.Core.ViewModels.Chats
{
	public class ChatViewModel : MvxViewModel<ChatViewModelArguments>
	{
		private User _recipient;
		private MvxCommand _sendCommand;
		private MvxObservableCollection<Message> _messages = new MvxObservableCollection<Message>();
		private string _textToSend;
		private readonly IChatsService _chatsService;
		private string _imagePath = string.Empty;
		private MvxCommand _attachImageCommand;
		private readonly IPermissionsService _permissionsService;
		public ChatViewModel(IChatsService chatsService, IPermissionsService permissionsService)
		{
			_chatsService = chatsService;
			_permissionsService = permissionsService;
		}

		public User Recipient
		{
			get => _recipient;
			private set => SetProperty(ref _recipient, value);
		}

		public MvxCommand AttachImageCommand
		{
			get
			{
				_attachImageCommand = _attachImageCommand ?? new MvxCommand(async () => {
					if (!await _permissionsService.CheckPermission(Permission.Storage, "Для загрузки аватара необходимо разрешение на использование хранилища."))
					{
						return;
					}

					if (!CrossMedia.Current.IsPickPhotoSupported)
					{
						return;
					}

					var image = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
					{
						PhotoSize = PhotoSize.Medium
					});

					if (image == null)
					{
						return;
					}

					ImagePath = image.Path;
				});
				return _attachImageCommand;
			}
		}

		public string ImagePath
		{
			get => _imagePath;
			set => SetProperty(ref _imagePath, value);
		}

		public override void Prepare(ChatViewModelArguments parameter)
		{
			Recipient = parameter.User;
			if (parameter.DialogId != null)
			{
				DialogId = parameter.DialogId.Value;
			}

			Dialog = parameter.Dialog;
		}

		public int? DialogId
		{
			get;
			private set;
		}

		public MvxObservableCollection<Message> Messages
		{
			get => _messages;
			set => SetProperty(ref _messages, value);
		}

		public string TextToSend
		{
			get => _textToSend;
			set => SetProperty(ref _textToSend, value);
		}

		public override async Task Initialize()
		{
			await base.Initialize();

			if (DialogId == null)
			{

				if (_chatsService.SavedDialogs.Count == 0)
				{
					await _chatsService.GetDialogs();
				}

				Dialog = _chatsService.SavedDialogs.SingleOrDefault(d => d.UserTo.Uuid.Equals(Recipient.Uuid));
				if (Dialog != null)
				{
					DialogId = Dialog.Id;
				}
			}

			if (DialogId != null)
			{
				try
				{
					var messages = await _chatsService.GetMessages(DialogId.Value);
					messages.Reverse();
					Messages = new MvxObservableCollection<Message>(messages);
				}
				catch (Exception e)
				{
					Console.WriteLine(e);
				}
			}
		}

		public MvxCommand SendCommand
		{
			get
			{
				_sendCommand = _sendCommand ?? new MvxCommand(async () =>
				{
					if (DialogId == null)
					{
						try
						{
							DialogId = (await _chatsService.CreateDialog(Recipient)).Id;
						}
						catch (Exception e)
						{
							Console.WriteLine(e);
						}
					}

					if (string.IsNullOrWhiteSpace(TextToSend))
					{
						return;
					}

					if (DialogId == null)
					{
						throw new InvalidOperationException("Dialog id is null.");
					}

					try
					{
						var message = await _chatsService.SendMessage(DialogId.Value, TextToSend, ImagePath);
						Messages.Insert(0, message);
						await RaisePropertyChanged(() => Messages);

						if (Dialog != null)
						{
							Dialog.LastMessage = message;
						}
					}
					catch (Exception e)
					{
						Console.WriteLine(e);
					}

					TextToSend = string.Empty;
					ImagePath = string.Empty;
				});
				return _sendCommand;
			}
		}

		public Dialog Dialog { get; private set; }
	}

	public class ChatViewModelArguments
	{
		public ChatViewModelArguments(User user, int? dialogId)
		{
			User = user;
			if (dialogId != null)
			{
				DialogId = dialogId.Value;
			}
		}
		public ChatViewModelArguments(Dialog dialog)
		{
			User = dialog.UserTo;
			DialogId = dialog.Id;
			Dialog = dialog;
		}

		public User User
		{
			get;
		}

		public int? DialogId
		{
			get;
		}

		public Dialog Dialog
		{
			get;
		}
	}
}
