using System;
using System.Threading.Tasks;
using bonus.app.Core.Models;
using MvvmCross.ViewModels;

namespace bonus.app.Core.ViewModels.Businessman.Services
{
	public interface ICreateServiceViewModel
	{
		Task<ServiceTypeItem> CreateServiceTypeItem(string name);

		Task<bool> RemoveServiceTypeItem(Guid uuid);
	}
}
