using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
		#region Data
		#region Fields
		private readonly IAuthService _authService;
		private MvxCommand _openChatCommand;
		private MvxCommand _openEditProfilePageCommand;
		private MvxCommand _openSubscribersCommand;
		private IEnumerable<Service> _services;
		private readonly IServicesService _servicesService;
		private User _user;
		#endregion
		#endregion

		#region .ctor
		public BusinessmanProfileViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IAuthService authService, IServicesService servicesService)
			: base(logProvider, navigationService)
		{
			_servicesService = servicesService;
			_authService = authService;
			User = _authService.User;
			PhotoSource = string.IsNullOrEmpty(User.PhotoSource) ? "about:blank" : User.PhotoSource;
		}

		public string PhotoSource
		{
			get;
		}
		#endregion

		#region Properties
		public MvxCommand OpenChatCommand
		{
			get
			{
				_openChatCommand = _openChatCommand ??
								   new MvxCommand(() =>
								   {
									   NavigationService.Navigate<MessageListViewModel>();
								   });
				return _openChatCommand;
			}
		}

		public MvxCommand OpenEditProfilePageCommand
		{
			get
			{
				_openEditProfilePageCommand = _openEditProfilePageCommand ??
											  new MvxCommand(async () =>
											  {
												  var user = await NavigationService.Navigate<EditProfileBusinessmanViewModel, EditProfileViewModelArguments, User>(
																 new EditProfileViewModelArguments(_authService.User.Uuid, true));
												  if (user == null)
												  {
													  return;
												  }

												  User = user;
											  });
				return _openEditProfilePageCommand;
			}
		}

		public MvxCommand OpenSubscribersCommand
		{
			get
			{
				_openSubscribersCommand = _openSubscribersCommand ??
										  new MvxCommand(() =>
										  {
											  NavigationService.Navigate<SubscribersViewModel>();
										  });
				return _openSubscribersCommand;
			}
		}

		public IEnumerable<Service> Services
		{
			get => _services;
			private set => SetProperty(ref _services, value);
		}

		public User User
		{
			get => _user;
			private set => SetProperty(ref _user, value);
		}
		#endregion

		#region Overrided
		public override async Task Initialize()
		{
			await base.Initialize();

			try
			{
				Services = await _servicesService.GetBusinessmenService();
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}
		#endregion
	}
}
