using System.Collections.Generic;
using System.Threading.Tasks;
using bonus.app.Core.Dtos;
using bonus.app.Core.Models;

namespace bonus.app.Core.Services
{
	public interface IAuthService
	{
		#region Properties
		string Error
		{
			get;
		}

		Dictionary<string, string[]> ErrorDetails
		{
			get;
		}

		AccessToken Token
		{
			get;
		}

		User User
		{
			get;
		}
		#endregion

		#region Overridable
		Task<User> Login(AuthDto authData);

		Task<bool> Logout(User user);

		Task<User> Register(User user, string password, string confirmPassword);
		#endregion
	}
}
