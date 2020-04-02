using bonus.app.Core.Models;
using bonus.app.Core.Services;
using MvvmCross.ViewModels;

namespace bonus.app.Core.ViewModels.Businessman.Popups
{
	public class SharePopupViewModel : MvxViewModel<Share>
	{
		private Share _share;
		private User _user;

		public SharePopupViewModel(IAuthService authService)
		{
			User = authService.User;
		}

		public User User
		{
			get => _user;
			private set => SetProperty(ref _user, value);
		}

		public override void Prepare(Share parameter)
		{
			Share = parameter;
		}

		public Share Share
		{
			get => _share;
			private set => SetProperty(ref _share, value);
		}
	}
}
