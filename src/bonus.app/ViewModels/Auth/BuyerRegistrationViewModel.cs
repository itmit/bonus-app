using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace bonus.app.Core.ViewModels.Auth
{
	public class BuyerRegistrationViewModel : MvxViewModel
	{
		private IMvxNavigationService _navigationService;

		public BuyerRegistrationViewModel(IMvxNavigationService navigationService)
			=> _navigationService = navigationService;


	}
}
