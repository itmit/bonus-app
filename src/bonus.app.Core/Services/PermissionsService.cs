using System;
using System.Threading.Tasks;
using MvvmCross.Forms.Presenters;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Xamarin.Forms;

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

				Device.BeginInvokeOnMainThread(async () =>
				{
					var answer = await FormsApplication.MainPage.DisplayAlert("Внимание", message, "Ок", "Отмена");

					if (answer)
					{
						_settingsHelper.OpenAppSettings();
					}
				});
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
