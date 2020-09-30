using System;
using bonus.app.Core.Services;
using bonus.app.Core.Services.Interfaces;

namespace bonus.app.iOS.Services
{
	public class MessagingService : IMessagingService
	{
		public event EventHandler MessageReceived;

		internal void ReceiveMessage()
		{
			MessageReceived?.Invoke(this, EventArgs.Empty);
		}
	}
}
