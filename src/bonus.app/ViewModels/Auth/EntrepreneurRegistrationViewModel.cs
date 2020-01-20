using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace bonus.app.Core.ViewModels.Auth
{
	public class EntrepreneurRegistrationViewModel : MvxViewModel
	{
		private IMvxNavigationService _navigationService;

		public EntrepreneurRegistrationViewModel(IMvxNavigationService navigationService)
			=> _navigationService = navigationService;


	}
}
