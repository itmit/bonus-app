using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using bonus.app.Core.Dtos.BusinessmanDtos;
using bonus.app.Core.Models;

namespace bonus.app.Core.Services
{
	public interface IServicesService
	{
		#region Overridable
		Task<bool> CreateService(CreateServiceDto createServiceDto);

		Task<ServiceTypeItem> CreateServiceTypeItem(string name, Guid serviceTypeUuid);

		Task<bool> RemoveServiceTypeItem(Guid uuid);

		Task<ServiceType> CreateServiceType(string name);

		Task<IEnumerable<ServiceType>> GetAll();

		Task<IEnumerable<Service>> GetBusinessmenService();
		#endregion
	}
}
