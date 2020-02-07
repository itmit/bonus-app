using System.Windows.Input;
using bonus.app.Core.Models;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using User = Realms.Sync.User;

namespace bonus.app.Core.ViewModels.Auth
{
	public class EntrepreneurAndBuyerViewModel : MvxNavigationViewModel
	{
		private MvxCommand _openPurchaserRegistrationCommand;
		private MvxCommand _openBuyerRegistrationCommand;


		public ICommand OpenEntrepreneurRegistrationCommand
		{
			get
			{
				_openPurchaserRegistrationCommand = _openPurchaserRegistrationCommand ?? new MvxCommand(() =>
				{
					NavigationService.Navigate<PublicOfferViewModel, UserRole>(UserRole.Entrepreneur);
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
					NavigationService.Navigate<PublicOfferViewModel, UserRole>(UserRole.Buyer);
				});
				return _openBuyerRegistrationCommand;
			}
		}

		public EntrepreneurAndBuyerViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService)
			: base(logProvider, navigationService)
		{
		}
	}
}
