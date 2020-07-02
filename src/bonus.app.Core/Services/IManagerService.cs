using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using bonus.app.Core.Models.UserModels;

namespace bonus.app.Core.Services
{
	public interface IManagerService
	{
		Task<Guid> StoreManager(User user, string password, string confirmPassword);

		Task<List<User>> GetManagers();
		
		Task<User> GetManager();

		Task<User> EditManager(string  name, string phone);

		Task<bool> DeleteManager();
	}
}
