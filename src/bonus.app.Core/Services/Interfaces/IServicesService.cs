using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using bonus.app.Core.Dtos.BusinessmanDtos;
using bonus.app.Core.Models.GeoHelperModels;
using bonus.app.Core.Models.ServiceModels;

namespace bonus.app.Core.Services.Interfaces
{
	public interface IServicesService
	{
		#region Overridable
		Task<bool> CreateService(CreateServiceDto createServiceDto);

		Task<ServiceType> CreateServiceType(string name);

		Task<ServiceTypeItem> CreateServiceTypeItem(string name, Guid serviceTypeUuid);

		Task<List<Service>> GetAllServices();

		Task<List<Service>> GetAllServices(Country country, City city, int? serviceId);

		Task<IEnumerable<Service>> GetBusinessmenService();

		Task<IEnumerable<Service>> GetBusinessmenService(Guid businessmenUuid);

		Task<IEnumerable<ServiceType>> GetMyServices();

		Task<bool> RemoveServiceTypeItem(Guid uuid);

		Task<bool> UpdateService(CreateServiceDto service, Guid uuid);
		#endregion

		event EventHandler MyServicesListChanged;
	}
}
