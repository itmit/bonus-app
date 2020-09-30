using System;
using System.Threading.Tasks;
using bonus.app.Core.Services.Interfaces;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using XF.Material.Forms.UI.Dialogs;

namespace bonus.app.Core.Services.Implementations
{
	public class PermissionsService : IPermissionsService
	{
		#region Data
		#region Fields
		private readonly ISettingsHelper _settingsHelper;
		#endregion
		#endregion

		#region .ctor
		public PermissionsService(ISettingsHelper settingsHelper) => _settingsHelper = settingsHelper;
		#endregion

		#region IPermissionsService members
		public async Task<bool> RequestPermissionAsync<T>(Permission permission, string message) where T : BasePermission, new()
		{
			try
			{
				var status = await CrossPermissions.Current.CheckPermissionStatusAsync<T>();

				if (status == PermissionStatus.Granted)
				{
					return true;
				}

				if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(permission))
				{
					Console.WriteLine($"Request {permission} permission rationale is showed");
				}
				
				status = await CrossPermissions.Current.RequestPermissionAsync<T>();

				if (status == PermissionStatus.Granted)
				{
					return true;
				}

				var answer = await MaterialDialog.Instance.ConfirmAsync(message, "Внимание", "Ок", "Отмена");

				if (answer != null && answer.Value)
				{
					_settingsHelper.OpenAppSettings();
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}

			return false;
		}
		#endregion
	}
}
