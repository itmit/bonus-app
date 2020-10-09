using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using bonus.app.Core.Dtos;
using bonus.app.Core.Models;
using bonus.app.Core.Models.UserModels;

namespace bonus.app.Core.Services.Interfaces
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

		event EventHandler TokenUpdated;

		#region Overridable
		Task<bool> UserIsAuthorized();

		Task<User> Login(AuthDto authData);

		Task<bool> Logout(User user);

		Task<User> Register(User user, string password, string confirmPassword);

		Task<User> AuthorizationAnExternalService(string email, string accessToken, ExternalAuthService authServiceType);

		Task<bool> SendRecoveryCode(string email);

		Task<bool> Recovery(string email, string code, string password, string passwordConfirmation);
		#endregion
	}
}
