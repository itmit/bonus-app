using System.Threading.Tasks;
using Plugin.Permissions.Abstractions;

namespace bonus.app.Core.Services
{
	public interface IPermissionsService
	{
		Task<bool> CheckPermission(Permission permission, string  message);
	}
}
