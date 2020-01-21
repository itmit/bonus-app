using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace bonus.app.Core.ViewModels.Auth
{
	public class AuthVkFcViewModel : MvxViewModel
	{
		private IMvxNavigationService _navigationService;

		public AuthVkFcViewModel(IMvxNavigationService navigationService)
		{
			_navigationService = navigationService;
		}
	}
}
