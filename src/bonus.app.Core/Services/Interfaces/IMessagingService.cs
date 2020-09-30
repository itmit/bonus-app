using System;

namespace bonus.app.Core.Services.Interfaces
{
	public interface IMessagingService
	{

		event EventHandler MessageReceived;

	}
}
