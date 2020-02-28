using System;
using System.Threading.Tasks;
using bonus.app.Core.Models;

namespace bonus.app.Core.Services
{
	public interface ICustomerService
	{
		Task<User> GetCustomerByUuid(Guid uuid);
	}
}
