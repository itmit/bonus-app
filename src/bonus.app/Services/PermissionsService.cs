using System;
using System.Threading.Tasks;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Xamarin.Forms;

namespace bonus.app.Core.Services
{
	public class PermissionsService: IPermissionsService
	{
		private readonly ISettingsHelper _settingsHelper;

		public PermissionsService(ISettingsHelper settingsHelper)
		{
			_settingsHelper = settingsHelper;
		}

		public async Task<bool> CheckPermission(Permission permission,  string message)
		{
			try
			{
				var status = await CrossPermissions.Current.CheckPermissionStatusAsync(permission);
				if (status == PermissionStatus.Granted)
				{
					return true;
				}
				await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(permission);
				await CrossPermissions.Current.RequestPermissionsAsync(permission);
				
				status = await CrossPermissions.Current.CheckPermissionStatusAsync(permission);

				if (status == PermissionStatus.Granted)
				{
					return true;
				}

				Device.BeginInvokeOnMainThread(async () =>
				{
					var answer = await Application.Current.MainPage.DisplayAlert("Внимание",
																				 message,
																				 "Ок",
																				 "Отмена");

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
	}
}
