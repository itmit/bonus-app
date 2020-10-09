using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using bonus.app.Core.Dtos;
using bonus.app.Core.Models.UserModels;
using bonus.app.Core.Repositories;
using bonus.app.Core.Services.Interfaces;
using bonus.app.Core.ViewModels;
using bonus.app.Core.ViewModels.Auth;
using bonus.app.Core.ViewModels.Businessman;
using bonus.app.Core.ViewModels.Customer;
using bonus.app.Core.ViewModels.Manager;
using MvvmCross;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Newtonsoft.Json;
using Xamarin.Essentials;

namespace bonus.app.Core
{
	public class AppStart : MvxAppStart
	{
		private readonly IAuthService _authService;
		private readonly IUserRepository _userRepository;

		public AppStart(IMvxApplication app, IMvxNavigationService mvxNavigationService, IAuthService authService, IUserRepository userRepository)
			: base(app, mvxNavigationService)
		{
			_userRepository = userRepository;
			_authService = authService;
		}

		protected override async Task NavigateToFirstViewModel(object hint = null)
		{
			var firstRun = Preferences.Get("FirstRun", "true");
			if (firstRun.Equals("true"))
			{
				Preferences.Set("FirstRun", "false");
				await NavigationService.Navigate<BusinessmanAndCustomerViewModel>();
				return;
			}

			if (!string.IsNullOrEmpty(_authService.Token?.ToString()))
			{
				switch (_authService.User.Role)
				{
					case UserRole.Businessman:
						await NavigationService.Navigate<MainBusinessmanViewModel>();
						break;
					case UserRole.Customer:
						await NavigationService.Navigate<MainCustomerViewModel>();
						break;
					case UserRole.Manager:
						await NavigationService.Navigate<MainManagerViewModel>();
						break;
					default:
						throw new ArgumentOutOfRangeException();
				}
				return;
			}
			_userRepository.RemoveAll();
			await NavigationService.Navigate<AuthorizationViewModel>();
		}
	}
}
