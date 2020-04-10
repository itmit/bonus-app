using bonus.app.Core.Models;
using bonus.app.Core.Services;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace bonus.app.Core.ViewModels.Businessman.Profile
{
	public class BusinessmanProfileViewModel : MvxNavigationViewModel
	{
		private MvxCommand _openEditProfilePageCommand;
		private IAuthService _authService;
		private User _user;
        private MvxCommand _openChatCommand;
        private MvxCommand _openSubscribersCommand;

        #region .ctor
        public BusinessmanProfileViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IAuthService authService)
			: base(logProvider, navigationService)
		{
			_authService = authService;
			User = _authService.User;
		}

		public User User
		{
			get => _user;
			set => SetProperty(ref _user, value);
		}
		#endregion

		public MvxCommand OpenEditProfilePageCommand
		{
			get
			{
				_openEditProfilePageCommand = _openEditProfilePageCommand ?? new MvxCommand(() =>
				{
					NavigationService.Navigate<EditProfileBusinessmanViewModel, EditProfileViewModelArguments>(new EditProfileViewModelArguments(_authService.User.Guid,  true));
				});
				return _openEditProfilePageCommand;
			}
		}

		public MvxCommand OpenChatCommand
		{
			get
			{
				_openChatCommand = _openChatCommand ?? new MvxCommand(() =>
				{
					NavigationService.Navigate<ChatViewModel>();
				});
				return _openChatCommand;
			}
		}

		public MvxCommand OpenSubscribersCommand
		{
			get
			{
				_openSubscribersCommand = _openSubscribersCommand ?? new MvxCommand(() =>
				{
					NavigationService.Navigate<SubscribersViewModel>();
				});
				return _openSubscribersCommand;
			}
		}
	}
}
