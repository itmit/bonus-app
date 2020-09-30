using System.Threading.Tasks;

namespace bonus.app.Core.Services.Interfaces
{
	public interface IVkService
	{
		Task<LoginResult> Login();
		void Logout();
	}
}
