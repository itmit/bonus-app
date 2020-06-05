using System;

namespace bonus.app.Core.Services
{
	public interface IMessagingService
	{

		event EventHandler MessageReceived;

	}
}
