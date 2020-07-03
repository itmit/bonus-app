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

		Task<bool> EditManager(int managerId, string  name, string phone);

		Task<bool> DeleteManager(int managerId);

		string LastError
		{
			get;
		}
	}
}
