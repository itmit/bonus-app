using System.Windows.Input;
using bonus.app.Core.Models;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using User = Realms.Sync.User;

namespace bonus.app.Core.ViewModels.Auth
{
	public class BusinessmanAndCustomerViewModel : MvxNavigationViewModel
	{
		private MvxCommand _openPurchaserRegistrationCommand;
		private MvxCommand _openBuyerRegistrationCommand;


		public ICommand OpenEntrepreneurRegistrationCommand
		{
			get
			{
				_openPurchaserRegistrationCommand = _openPurchaserRegistrationCommand ?? new MvxCommand(() =>
				{
					NavigationService.Navigate<PublicOfferViewModel, UserRole>(UserRole.Businessman);
				});
				return _openPurchaserRegistrationCommand;
			}
		}

		public ICommand OpenBuyerRegistrationCommand
		{
			get
			{
				_openBuyerRegistrationCommand = _openBuyerRegistrationCommand ?? new MvxCommand(() =>
				{
					NavigationService.Navigate<PublicOfferViewModel, UserRole>(UserRole.Customer);
				});
				return _openBuyerRegistrationCommand;
			}
		}

		public BusinessmanAndCustomerViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService)
			: base(logProvider, navigationService)
		{
		}
	}
}
