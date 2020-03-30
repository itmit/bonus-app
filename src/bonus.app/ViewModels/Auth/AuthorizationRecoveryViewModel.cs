using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace bonus.app.Core.ViewModels.Auth
{
	public class AuthorizationRecoveryViewModel : MvxViewModel
	{
		#region Data
		#region Fields
		private IMvxNavigationService _navigationService;
		#endregion
		#endregion

		#region .ctor
		public AuthorizationRecoveryViewModel(IMvxNavigationService navigationService) => _navigationService = navigationService;
		#endregion
	}
}
