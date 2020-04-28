using System;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using ZXing;

namespace bonus.app.Core.ViewModels
{
	public class ScannerViewModel : MvxViewModel<object, Guid>
	{
		#region Data
		#region Fields
		private readonly IMvxNavigationService _navigationService;
		#endregion
		#endregion

		#region .ctor
		public ScannerViewModel(IMvxNavigationService navigationService) => _navigationService = navigationService;
		#endregion

		#region Public
		public void OnScanResult(Result result)
		{
			if (Guid.TryParse(result.Text, out var guid))
			{
				_navigationService.Close(this, guid);
			}
		}
		#endregion

		#region Overrided
		public override void Prepare(object parameter)
		{
		}
		#endregion
	}
}
