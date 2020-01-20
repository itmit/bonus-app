using System.Windows.Input;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace bonus.app.Core.ViewModels.Auth
{
	public class EntrepreneurAndBuyerViewModel : MvxViewModel
	{
		private IMvxNavigationService _navigationService;
		private MvxCommand _openPurchaserRegistrationCommand;

		public EntrepreneurAndBuyerViewModel(IMvxNavigationService navigationService) 
			=> _navigationService = navigationService;

		public ICommand OpenEntrepreneurRegistrationCommand
		{
			get
			{
				_openPurchaserRegistrationCommand = _openPurchaserRegistrationCommand ?? new MvxCommand(() =>
				{
					_navigationService.Navigate<EntrepreneurRegistrationViewModel>();
				});
				return _openPurchaserRegistrationCommand;
			}
		}

		public ICommand OpenBuyerRegistrationCommand
		{
			get
			{
				_openPurchaserRegistrationCommand = _openPurchaserRegistrationCommand ?? new MvxCommand(() =>
				{
					_navigationService.Navigate<EntrepreneurRegistrationViewModel>();
				});
				return _openPurchaserRegistrationCommand;
			}
		}
	}
}
