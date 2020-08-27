using System;
using System.Threading.Tasks;
using MvvmCross.Forms.Presenters;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Xamarin.Forms;
using XF.Material.Forms.UI.Dialogs;

namespace bonus.app.Core.Services
{
	public class PermissionsService : IPermissionsService
	{
		#region Data
		#region Fields
		private readonly ISettingsHelper _settingsHelper;
		#endregion
		#endregion

		#region .ctor
		public PermissionsService(ISettingsHelper settingsHelper, IMvxFormsViewPresenter platformPresenter)
		{
			_platformPresenter = platformPresenter; _settingsHelper = settingsHelper;
		}

		private readonly IMvxFormsViewPresenter _platformPresenter;

		private Application _formsApplication;
		private Application FormsApplication => _formsApplication ?? (_formsApplication = _platformPresenter.FormsApplication);
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
