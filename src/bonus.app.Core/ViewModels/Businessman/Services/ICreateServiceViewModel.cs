using System;
using System.Threading.Tasks;
using bonus.app.Core.Models;

namespace bonus.app.Core.ViewModels.Businessman.Services
{
	public interface ICreateServiceViewModel
	{
		#region Overridable
		Task<ServiceTypeItem> CreateServiceTypeItem(string name);

		Task<bool> EditServiceTypeItem(Guid uuid, string name);

		Task<bool> RemoveServiceTypeItem(Guid uuid);
		#endregion
	}
}
