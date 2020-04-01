using System;
using System.Threading.Tasks;
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
		#endregion
		#endregion

		#region .ctor
		public BusinessmanBonusAccrualViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IPermissionsService permissionsService)
			: base(logProvider, navigationService)
		{
			_permissionsService = permissionsService;
		}
		#endregion

		#region Properties
		public IMvxCommand FurtherCommand
		{
			get
			{
				_furetherCommand = _furetherCommand ??
								   new MvxCommand(() =>
								   {
									   // NavigationService.Navigate<BusinessmanBonusAccrualDetailsViewModel>();
								   });
				return _furetherCommand;
			}
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
						await NavigationService.Navigate<BusinessmanBonusAccrualDetailsViewModel, Guid>(result);
					}
				});
				return _openScannerCommand;
			}
		}
	}
}
