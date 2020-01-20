using System.Threading.Tasks;
using bonus.app.Core.Dto;
using bonus.app.Core.Models;

namespace bonus.app.Core.Services
{
	public interface IAuthService
	{
		Task<User> Login(AuthDto authData);

		ErrorsDto<AuthErrorDto> ServerAuthorizationError
		{
			get;
		}
	}
}
