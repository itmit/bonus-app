using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using bonus.app.Core.Models;
using bonus.app.Core.Services;
using bonus.app.Core.ViewModels.Chats;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Xamarin.Essentials;

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
		private readonly IProfileService _profileService;
		private MvxObservableCollection<PortfolioImage> _portfolioImages;
		private MvxCommand _openVkCommand;
		private MvxCommand _openClassmatesCommand;
		private MvxCommand _openFacebookCommand;
		private MvxCommand _openInstagramCommand;
		#endregion
		#endregion

		#region .ctor
		public BusinessmanProfileViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IAuthService authService, IServicesService servicesService, IProfileService profileService)
			: base(logProvider, navigationService)
		{
			_servicesService = servicesService;
			_profileService = profileService;
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
		public MvxObservableCollection<PortfolioImage> PortfolioImages
		{
			get => _portfolioImages;
			private set => SetProperty(ref _portfolioImages, value);
		}

		public MvxCommand OpenChatCommand
		{
			get
			{
				_openChatCommand = _openChatCommand ??
								   new MvxCommand(() =>
								   {
									   NavigationService.Navigate<DialogsViewModel>();
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

		public MvxCommand OpenVkCommand
		{
			get
			{
				_openVkCommand = _openVkCommand ?? new MvxCommand(async () =>
				{
					await OpenBrowser(User.VkLink);
				});
				return _openVkCommand;
			}
		}
		public MvxCommand OpenInstagramCommand
		{
			get
			{
				_openInstagramCommand = _openInstagramCommand ?? new MvxCommand(async () =>
				{
					await OpenBrowser(User.InstagramLink);
				});
				return _openInstagramCommand;
			}
		}
		public MvxCommand OpenFacebookCommand
		{
			get
			{
				_openFacebookCommand = _openFacebookCommand ?? new MvxCommand(async () =>
				{
					await OpenBrowser(User.FacebookLink);
				});
				return _openFacebookCommand;
			}
		}
		public MvxCommand OpenClassmatesCommand
		{
			get
			{
				_openClassmatesCommand = _openClassmatesCommand ?? new MvxCommand(async () =>
				{
					await OpenBrowser(User.ClassmatesLink);
				});
				return _openClassmatesCommand;
			}
		}

		private async Task OpenBrowser(string link)
		{
			if (Uri.TryCreate(link, UriKind.Absolute, out var uriResult) && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps))
			{
				await Browser.OpenAsync(uriResult, BrowserLaunchMode.SystemPreferred);
			}
		}
		#endregion

		#region Overrided
		public override async Task Initialize()
		{
			await base.Initialize();

			try
			{
				Services = await _servicesService.GetBusinessmenService();
				PortfolioImages = new MvxObservableCollection<PortfolioImage>(await _profileService.GetPortfolio());
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}
		#endregion
	}
}
