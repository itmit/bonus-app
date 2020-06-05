using System;
using bonus.app.Core.Services;
using Firebase.Messaging;

namespace bonus.app.Droid.Services
{
	public class MessagingService : IMessagingService
	{
		public event EventHandler MessageReceived;

		public string Uuid = Guid.NewGuid()
								 .ToString();
		internal void ReceiveMessage(RemoteMessage.Notification notification)
		{
			MessageReceived?.Invoke(this, EventArgs.Empty);
		}
	}
}
