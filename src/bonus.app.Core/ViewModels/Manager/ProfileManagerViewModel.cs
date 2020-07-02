using bonus.app.Core.Models.UserModels;
using bonus.app.Core.Services;
using MvvmCross.ViewModels;

namespace bonus.app.Core.ViewModels.Manager
{
	public class ProfileManagerViewModel : MvxViewModel
	{
		private User _user;
		public ProfileManagerViewModel(IAuthService authService) =>
			User = authService.User;

		public User User
		{
			get => _user;
			private set => SetProperty(ref _user, value);
		}
	}
}
