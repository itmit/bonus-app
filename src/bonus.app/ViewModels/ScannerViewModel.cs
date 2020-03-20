using System;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using ZXing;

namespace bonus.app.Core.ViewModels
{
	public class ScannerViewModel: MvxViewModel<object, Guid>
	{
		private readonly IMvxNavigationService _navigationService;

		public ScannerViewModel(IMvxNavigationService navigationService)
		{
			_navigationService = navigationService;
		}

		public void OnScanResult(Result result)
		{
			if (Guid.TryParse(result.Text, out var guid))
			{
				_navigationService.Close(this, guid);
			}
		}

		public override void Prepare(object parameter)
		{ }
	}
}
