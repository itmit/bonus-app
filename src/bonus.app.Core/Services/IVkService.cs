using System.Threading.Tasks;

namespace bonus.app.Core.Services
{
	public interface IVkService
	{
		Task<LoginResult> Login();
		void Logout();
	}
}
