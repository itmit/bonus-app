using bonus.app.Core.Models;
using bonus.app.Core.Services;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace bonus.app.Core.ViewModels.Customer.Profile
{
	public class CustomerProfileViewModel : MvxNavigationViewModel
	{
		#region Data
		#region Fields
		private MvxCommand _openEditProfileCommand;
		private MvxCommand _openMessageCommand;
		private MvxCommand _openSubscribesCommand;

		private User _user;
		#endregion
		#endregion

		#region .ctor
		public CustomerProfileViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IAuthService authService)
			: base(logProvider, navigationService) =>
			User = authService.User;
		#endregion

		#region Properties
		public MvxCommand OpenEditProfileCommand
		{
			get
			{
				_openEditProfileCommand = _openEditProfileCommand ??
										  new MvxCommand(() =>
										  {
											  NavigationService.Navigate<EditProfileCustomerViewModel, EditProfileViewModelArguments>(
												  new EditProfileViewModelArguments(User.Uuid, true));
										  });
				return _openEditProfileCommand;
			}
		}

		public MvxCommand OpenMessageCommand
		{
			get
			{
				_openMessageCommand = _openMessageCommand ??
									  new MvxCommand(() =>
									  {
										  NavigationService.Navigate<MessageListViewModel>();
									  });
				return _openMessageCommand;
			}
		}

		public MvxCommand OpenSubscribesCommand
		{
			get
			{
				_openSubscribesCommand = _openSubscribesCommand ??
										 new MvxCommand(() =>
										 {
											 NavigationService.Navigate<SubscribesViewModel>();
										 });
				return _openSubscribesCommand;
			}
		}

		public User User
		{
			get => _user;
			private set => SetProperty(ref _user, value);
		}
		#endregion
	}
}
