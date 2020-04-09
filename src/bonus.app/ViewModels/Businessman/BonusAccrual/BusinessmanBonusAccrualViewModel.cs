using System;
using System.Threading.Tasks;
using bonus.app.Core.Models;
using bonus.app.Core.Services;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Xamarin.Forms;
using ZXing;

namespace bonus.app.Core.ViewModels.Businessman.BonusAccrual
{
	public class BusinessmanBonusAccrualViewModel : MvxNavigationViewModel
	{
		#region Data
		#region Fields
		private IMvxCommand _furetherCommand;
		private MvxCommand _openScannerCommand;
		private readonly IPermissionsService _permissionsService;
		private readonly ICustomerService _customerService;
		private string _userLogin;
		#endregion
		#endregion

		#region .ctor
		public BusinessmanBonusAccrualViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IPermissionsService permissionsService, ICustomerService customerService)
			: base(logProvider, navigationService)
		{
			_permissionsService = permissionsService;
			_customerService = customerService;
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

		public string UserLogin
		{
			get => _userLogin;
			set => SetProperty(ref _userLogin, value);
		}
		#endregion

		public MvxCommand OpenScannerCommand
		{
			get
			{
				_openScannerCommand = _openScannerCommand ?? new MvxCommand(async () =>
				{
					if (await _permissionsService.CheckPermission(Permission.Camera, "Для сканирования QR-кода необходимо разрешение на использование камеры."))
					{
						Guid result = await NavigationService.Navigate<ScannerViewModel, object, Guid>(null);
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
					}
				});
				return _openScannerCommand;
			}
		}
	}
}
