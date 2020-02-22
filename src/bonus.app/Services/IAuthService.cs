using System.Collections.Generic;
using System.Threading.Tasks;
using bonus.app.Core.Dtos;
using bonus.app.Core.Models;

namespace bonus.app.Core.Services
{
	public interface IAuthService
	{
		Task<User> Login(AuthDto authData);

		Dictionary<string, string[]> ErrorDetails
		{
			get;
		}

		Task<User> Register(User user, string password, string confirmPassword);

		Task<bool> LogOut(User user);

		string Error
		{
			get;
		}
	}
}
