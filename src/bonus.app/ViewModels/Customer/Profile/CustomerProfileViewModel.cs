using bonus.app.Core.Models;
using bonus.app.Core.Services;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace bonus.app.Core.ViewModels.Customer.Profile
{
	public class CustomerProfileViewModel : MvxNavigationViewModel
	{
		public CustomerProfileViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IAuthService authService)
			: base(logProvider, navigationService)
		{
			User = authService.User;
		}

		private User _user;
		public User User
		{
			get => _user;
			private set => SetProperty(ref _user, value);
		}



	}
}
