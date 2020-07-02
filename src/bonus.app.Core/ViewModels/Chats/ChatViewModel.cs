using System;
using System.Linq;
using System.Threading.Tasks;
using bonus.app.Core.Models;
using bonus.app.Core.Models.ChatModels;
using bonus.app.Core.Models.UserModels;
using bonus.app.Core.Services;
using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;

namespace bonus.app.Core.ViewModels.Chats
{
	public class ChatViewModel : MvxViewModel<ChatViewModelArguments>
	{
		#region Data
		#region Fields
		private MvxCommand _attachImageCommand;
		private readonly IChatsService _chatsService;
		private string _imagePath = string.Empty;
		private MvxObservableCollection<Message> _messages = new MvxObservableCollection<Message>();
		private readonly IPermissionsService _permissionsService;
		private User _recipient;
		private MvxCommand _sendCommand;
		private string _textToSend;
		#endregion
		#endregion

		#region .ctor
		public ChatViewModel(IChatsService chatsService, IPermissionsService permissionsService)
		{
			_chatsService = chatsService;
			_permissionsService = permissionsService;

			var service = Mvx.IoCProvider.Resolve<IMessagingService>();

			if (service != null)
			{
				service.MessageReceived += OnMessageReceived;
			}
		}

		private async void OnMessageReceived(object sender, EventArgs e)
		{
			await Initialize();
		}
		#endregion

		#region Properties
		public Dialog Dialog
		{
			get;
			private set;
		}

		public int? DialogId
		{
			get;
			private set;
		}

		public MvxCommand AttachImageCommand
		{
			get
			{
				_attachImageCommand = _attachImageCommand ??
									  new MvxCommand(async () =>
									  {
										  if (!await _permissionsService.RequestPermissionAsync<StoragePermission>(Permission.Storage,
																								"Для загрузки аватара необходимо разрешение на использование хранилища."))
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

		public MvxObservableCollection<Message> Messages
		{
			get => _messages;
			set => SetProperty(ref _messages, value);
		}

		public User Recipient
		{
			get => _recipient;
			private set => SetProperty(ref _recipient, value);
		}

		public MvxCommand SendCommand
		{
			get
			{
				_sendCommand = _sendCommand ??
							   new MvxCommand(async () =>
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

		public string TextToSend
		{
			get => _textToSend;
			set => SetProperty(ref _textToSend, value);
		}
		#endregion

		#region Overrided
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

		public override void Prepare(ChatViewModelArguments parameter)
		{
			Recipient = parameter.User;
			if (parameter.DialogId != null)
			{
				DialogId = parameter.DialogId.Value;
			}

			Dialog = parameter.Dialog;
		}
		#endregion
	}

	public class ChatViewModelArguments
	{
		#region .ctor
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
		#endregion

		#region Properties
		public Dialog Dialog
		{
			get;
		}

		public int? DialogId
		{
			get;
		}

		public User User
		{
			get;
		}
		#endregion
	}
}
