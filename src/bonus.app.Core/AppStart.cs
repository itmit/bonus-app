using System;
using System.Threading.Tasks;
using bonus.app.Core.Models.UserModels;
using bonus.app.Core.Services;
using bonus.app.Core.ViewModels;
using bonus.app.Core.ViewModels.Auth;
using bonus.app.Core.ViewModels.Businessman;
using bonus.app.Core.ViewModels.Customer;
using bonus.app.Core.ViewModels.Manager;
using MvvmCross;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace bonus.app.Core
{
	public class AppStart : MvxAppStart
	{
		private readonly IAuthService _authService;

		public AppStart(IMvxApplication app, IMvxNavigationService mvxNavigationService, IAuthService authService)
			: base(app, mvxNavigationService) =>
			_authService = authService;

		protected override Task NavigateToFirstViewModel(object hint = null)
		{
			var user = _authService.User;

			if (user?.AccessToken == null)
			{
				return NavigationService.Navigate<AuthorizationViewModel>();
			}

			switch (user.Role)
			{
				case UserRole.Businessman:
					return NavigationService.Navigate<MainBusinessmanViewModel>();
				case UserRole.Customer:
					return NavigationService.Navigate<MainCustomerViewModel>();
				case UserRole.Manager:
					return NavigationService.Navigate<MainManagerViewModel>();
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
	}
}
