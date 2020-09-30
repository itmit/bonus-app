using bonus.app.Core.Models;
using bonus.app.Core.Models.UserModels;
using bonus.app.Core.Services;
using bonus.app.Core.Services.Interfaces;
using bonus.app.Core.ViewModels.Chats;
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
		private MvxCommand _openDialogsCommand;
		private MvxCommand _openEditProfileCommand;
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
		public MvxCommand OpenDialogsCommand
		{
			get
			{
				_openDialogsCommand = _openDialogsCommand ??
									  new MvxCommand(() =>
									  {
										  NavigationService.Navigate<DialogsViewModel>();
									  });
				return _openDialogsCommand;
			}
		}

		public MvxCommand OpenEditProfileCommand
		{
			get
			{
				_openEditProfileCommand = _openEditProfileCommand ??
										  new MvxCommand(async () =>
										  {
											  var user = await NavigationService.Navigate<EditProfileCustomerViewModel, EditProfileViewModelArguments, User>(
															 new EditProfileViewModelArguments(User.Uuid, true));
											  User = user ?? User;
										  });
				return _openEditProfileCommand;
			}
		}

		public MvxCommand OpenSubscribesCommand
		{
			get
			{
				_openSubscribesCommand = _openSubscribesCommand ??
										 new MvxCommand(() =>
										 {
											 NavigationService.Navigate<CustomerSubscribersViewModel>();
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
