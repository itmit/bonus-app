using System;
using bonus.app.Core.Services;
using bonus.app.Core.Services.Interfaces;
using Firebase.Messaging;

namespace bonus.app.Droid.Services
{
	public class MessagingService : IMessagingService
	{
		public event EventHandler MessageReceived;

		internal void ReceiveMessage(RemoteMessage.Notification notification)
		{
			MessageReceived?.Invoke(this, EventArgs.Empty);
		}
	}
}
