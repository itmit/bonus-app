using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using bonus.app.Core.Models;

namespace bonus.app.Core.Services
{
	public interface ISubscribeService
	{
		Task<List<Subscription>> GetSubscriptions();

		Task<bool> SubscribeToBusinessman(Guid businessmanUuid);

		Task<bool> UnsubscribeToBusinessman(Guid businessmanUuid);
	}
}
