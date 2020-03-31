using bonus.app.Core.Services;
using bonus.app.Core.ViewModels.Auth;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace bonus.app.Core.ViewModels.Customer
{
	public class MenuCustomerViewModel : MvxViewModel
	{
		private MvxCommand _logOutCommand;
		private readonly IMvxNavigationService _navigationService;
		private readonly IAuthService _authService;

		public MenuCustomerViewModel(IMvxNavigationService navigationService, IAuthService authService)
		{
			_navigationService = navigationService;
			_authService = authService;
		}

		public MvxCommand LogOutCommand
		{
			get
			{
				_logOutCommand = _logOutCommand ?? new MvxCommand(LogOutCommandExecute);
				return _logOutCommand;
			}
		}

		private async void LogOutCommandExecute()
		{
			if (await _authService.LogOut(_authService.User))
			{
				await _navigationService.Navigate<AuthorizationViewModel>();
			}
		}
	}
}
