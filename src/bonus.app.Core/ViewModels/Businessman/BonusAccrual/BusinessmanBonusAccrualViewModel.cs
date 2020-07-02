using System;
using bonus.app.Core.Models;
using bonus.app.Core.Models.UserModels;
using bonus.app.Core.Services;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Xamarin.Forms;

namespace bonus.app.Core.ViewModels.Businessman.BonusAccrual
{
	public class BusinessmanBonusAccrualViewModel : MvxNavigationViewModel
	{
		#region Data
		#region Fields
		private readonly ICustomerService _customerService;
		private IMvxCommand _furetherCommand;
		private MvxCommand _openScannerCommand;
		private readonly IPermissionsService _permissionsService;
		private string _userLogin;
		private Guid _userUuid;
		#endregion
		#endregion

		#region .ctor
		public BusinessmanBonusAccrualViewModel(IMvxLogProvider logProvider,
												IMvxNavigationService navigationService,
												IPermissionsService permissionsService,
												ICustomerService customerService,
												IAuthService authService)
			: base(logProvider, navigationService)
		{
			_permissionsService = permissionsService;
			_customerService = customerService;
			UserUuid = authService.User.Uuid;
		}
		#endregion

		#region Properties
		public IMvxCommand FurtherCommand
		{
			get
			{
				_furetherCommand = _furetherCommand ??
								   new MvxCommand(async () =>
								   {
									   var login = UserLogin?.Trim();
									   if (string.IsNullOrEmpty(login) || login.Length < 3)
									   {
										   Device.BeginInvokeOnMainThread(() =>
										   {
											   Application.Current.MainPage.DisplayAlert("Ошибка", "Заполните поле логин (не менее 3 символов).", "Ок");
										   });
									   }

									   User user = null;
									   try
									   {
										   user = await _customerService.GetCustomerByLogin(login);
									   }
									   catch (Exception e)
									   {
										   Console.WriteLine(e);
									   }

									   if (user == null)
									   {
										   Device.BeginInvokeOnMainThread(() =>
										   {
											   Application.Current.MainPage.DisplayAlert("Ошибка", "Покупатель не найден.", "Ок");
										   });
										   return;
									   }

									   await NavigationService.Navigate<BusinessmanBonusAccrualDetailsViewModel, User>(user);
								   });
				return _furetherCommand;
			}
		}

		public MvxCommand OpenScannerCommand
		{
			get
			{
				_openScannerCommand = _openScannerCommand ??
									  new MvxCommand(async () =>
									  {
										  if (!await _permissionsService.RequestPermissionAsync<CameraPermission>(Permission.Camera,
																												  "Для сканирования QR-кода необходимо разрешение на использование камеры.")
										  )
										  {
											  return;
										  }

										  var result = await NavigationService.Navigate<ScannerViewModel, object, Guid>(null);
										  User user = null;
										  try
										  {
											  user = await _customerService.GetCustomerByUuid(result);
										  }
										  catch (Exception e)
										  {
											  Console.WriteLine(e);
										  }

										  if (user == null)
										  {
											  Device.BeginInvokeOnMainThread(() =>
											  {
												  Application.Current.MainPage.DisplayAlert("Ошибка", "Покупатель не найден.", "Ок");
											  });
											  return;
										  }

										  await NavigationService.Navigate<BusinessmanBonusAccrualDetailsViewModel, User>(user);
									  });
				return _openScannerCommand;
			}
		}

		public string UserLogin
		{
			get => _userLogin;
			set => SetProperty(ref _userLogin, value);
		}

		public Guid UserUuid
		{
			get => _userUuid;
			set => SetProperty(ref _userUuid, value);
		}
		#endregion
	}
}
