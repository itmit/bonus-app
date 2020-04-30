using System;
using System.Threading.Tasks;
using bonus.app.Core.Models;

namespace bonus.app.Core.ViewModels.Businessman.Services
{
	public interface ICreatedServiceParentViewModel
	{
		ServiceType UserServiceType
		{
			get;
		}

		Guid UserUuid
		{
			get;
		}

		Task<bool> ReloadServices();
	}
}
