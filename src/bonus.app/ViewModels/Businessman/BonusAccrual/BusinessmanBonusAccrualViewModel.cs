using System;
using System.Threading.Tasks;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using ZXing;

namespace bonus.app.Core.ViewModels.Businessman.BonusAccrual
{
	public class BusinessmanBonusAccrualViewModel : MvxNavigationViewModel
	{
		#region Data
		#region Fields
		private IMvxCommand _furetherCommand;
		private bool _isEnabledScan;
		#endregion
		#endregion

		#region .ctor
		public BusinessmanBonusAccrualViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService)
			: base(logProvider, navigationService)
		{
		}
		#endregion

		public override async Task Initialize()
		{
			await base.Initialize();
			IsEnabledScan = await CheckPermission(Permission.Camera);
		}

		public bool IsEnabledScan
		{
			get => _isEnabledScan;
			set => SetProperty(ref _isEnabledScan, value);
		}

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

		#region Private
		/// <summary>
		/// Проверяет разрешения.
		/// </summary>
		/// <param name="permission">Разрешение.</param>
		/// <returns>Было ли получено разрешение.</returns>
		private async Task<bool> CheckPermission(Permission permission)
		{
			var status = await CrossPermissions.Current.CheckPermissionStatusAsync(permission);
			if (status != PermissionStatus.Granted)
			{
				await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(permission);

				try
				{
					await CrossPermissions.Current.RequestPermissionsAsync(permission);
				}
				catch (TaskCanceledException e)
				{
					Console.WriteLine(e);
				}

				status = await CrossPermissions.Current.CheckPermissionStatusAsync(permission);
			}

			return await Task.FromResult(status == PermissionStatus.Granted);
		}
		#endregion

		public void ScanResult(Result result)
		{
			if (Guid.TryParse(result.Text, out var guid))
			{
				NavigationService.Navigate<BusinessmanBonusAccrualDetailsViewModel, Guid>(guid);
			}
		}
	}
}
