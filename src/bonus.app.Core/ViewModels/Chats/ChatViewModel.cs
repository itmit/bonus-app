using System;
using bonus.app.Core.Models;
using MvvmCross.Commands;
using MvvmCross.ViewModels;

namespace bonus.app.Core.ViewModels.Chats
{
	public class ChatViewModel : MvxViewModel<User>
	{
		private User _recipient;
		private MvxCommand _sendCommand;
		private MvxObservableCollection<Message> _messages = new MvxObservableCollection<Message>();
		private string _textToSend;

		public User Recipient
		{
			get => _recipient;
			private set => SetProperty(ref _recipient, value);
		}

		public override void Prepare(User parameter)
		{
			Recipient = parameter;
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

		public MvxCommand SendCommand
		{
			get
			{
				_sendCommand = _sendCommand ?? new MvxCommand(() =>
				{
					if (string.IsNullOrWhiteSpace(TextToSend))
					{
						return;
					}

					Messages.Add(new Message
					{
						Text = TextToSend,
						IsTextOut = false,
						CreatedAt = DateTime.Now,
						UpdatedAt = DateTime.Now
					});
					TextToSend = string.Empty;
					RaisePropertyChanged(() => Messages);
				});
				return _sendCommand;
			}
		}
	}
}
