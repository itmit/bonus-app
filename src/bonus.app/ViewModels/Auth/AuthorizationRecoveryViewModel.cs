using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace bonus.app.Core.ViewModels.Auth
{
	public class AuthorizationRecoveryViewModel : MvxViewModel
	{
		private IMvxNavigationService _navigationService;

		public AuthorizationRecoveryViewModel(IMvxNavigationService navigationService) => _navigationService = navigationService;
	}
}
