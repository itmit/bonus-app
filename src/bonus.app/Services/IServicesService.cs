using System.Collections.Generic;
using System.Threading.Tasks;
using bonus.app.Core.Models;

namespace bonus.app.Core.Services
{
	public interface IServicesService
	{
		Task<IEnumerable<ServiceType>> GetAll();
	}
}
