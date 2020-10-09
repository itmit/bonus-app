using System.Threading.Tasks;
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
	public class CustomerProfileViewModel : MvxViewModel
	{
		#region Data
		#region Fields
		private MvxCommand _openDialogsCommand;
		private MvxCommand _openEditProfileCommand;
		private MvxCommand _openSubscribesCommand;
		private readonly IProfileService _profileService;
		private User _user;
		private MvxCommand _refreshCommand;
		private bool _isRefreshing;
		private readonly IMvxNavigationService _navigationService;
		#endregion
		#endregion

		#region .ctor
		public CustomerProfileViewModel(IMvxNavigationService navigationService
										, IProfileService profileService)
		{
			_profileService = profileService;
			_navigationService = navigationService;
			_profileService.UserUpdated += (sender, args) =>
			{
				LoadProfileTask = MvxNotifyTask.Create(LoadProfile);
			};
		}
		#endregion

		#region Properties
		public bool IsRefreshing
		{
			get => _isRefreshing;
			set => SetProperty(ref _isRefreshing, value);
		}

		public MvxCommand RefreshCommand
		{
			get
			{
				_refreshCommand = _refreshCommand ??
								  new MvxCommand(async () =>
								  {
									  IsRefreshing = true;
									  await Initialize();
									  IsRefreshing = false;
								  });
				return _refreshCommand;
			}
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

		public MvxCommand OpenEditProfileCommand
		{
			get
			{
				_openEditProfileCommand = _openEditProfileCommand ??
										  new MvxCommand(async () =>
										  {
											  var user = await _navigationService.Navigate<EditProfileCustomerViewModel, EditProfileViewModelArguments, User>(
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
											 _navigationService.Navigate<CustomerSubscribersViewModel>();
										 });
				return _openSubscribesCommand;
			}
		}

		public override Task Initialize() 
		{
			LoadProfileTask =  MvxNotifyTask.Create(LoadProfile);
			return base.Initialize();
		}

		public MvxNotifyTask LoadProfileTask
		{
			get;
			private set;
		}

		private async Task LoadProfile()
		{
			User = await _profileService.User();
		}


		public User User
		{
			get => _user;
			private set => SetProperty(ref _user, value);
		}
		#endregion
	}
}
