using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using bonus.app.Core.Models.UserModels;

namespace bonus.app.Core.Services.Interfaces
{
	public interface ISubscribeService
	{
		#region Overridable
		Task<List<Subscription>> GetSubscriptions();

		Task<bool> SubscribeToBusinessman(Guid businessmanUuid);

		Task<bool> UnsubscribeToBusinessman(Guid businessmanUuid);
		#endregion
	}
}
