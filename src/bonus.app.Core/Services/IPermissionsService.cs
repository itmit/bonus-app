using System.Threading.Tasks;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;

namespace bonus.app.Core.Services
{
	public interface IPermissionsService
	{
		#region Overridable
		Task<bool> RequestPermissionAsync<T>(Permission permission, string message) where T : BasePermission, new();
		#endregion
	}
}
