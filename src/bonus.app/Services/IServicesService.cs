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

		Task<bool> CreateService(string name, Guid serviceTypeUuid);

		Task<bool> CreateServiceType(string name);

		Task<IEnumerable<ServiceType>> GetAll();

		Task<IEnumerable<Service>> GetBusinessmenService();
		#endregion
	}
}
