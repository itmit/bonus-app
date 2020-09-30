using bonus.app.Core.Models.UserModels;
using bonus.app.Core.Services;
using bonus.app.Core.Services.Interfaces;
using bonus.app.Core.ViewModels.Chats;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace bonus.app.Core.ViewModels.Manager
{
	public class ProfileManagerViewModel : MvxViewModel
	{
		private User _user;
		private MvxCommand _openDialogsCommand;
		private IMvxNavigationService _navigationService;

		public ProfileManagerViewModel(IAuthService authService, IMvxNavigationService navigationService)
		{
			_navigationService = navigationService;

			User = authService.User;
		}	

		public User User
		{
			get => _user;
			private set => SetProperty(ref _user, value);
		}

		public MvxCommand OpenDialogsCommand
		{
			get
			{
				_openDialogsCommand = _openDialogsCommand ??
									  new MvxCommand(() =>
									  {
										  _navigationService.Navigate<DialogsViewModel>();
									  });
				return _openDialogsCommand;
			}
		}

	}
}
