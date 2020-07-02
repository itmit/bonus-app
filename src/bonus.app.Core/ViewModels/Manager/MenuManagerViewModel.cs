using System.Linq;
using bonus.app.Core.Services;
using bonus.app.Core.ViewModels.Auth;
using bonus.app.Core.ViewModels.Chats;
using MvvmCross.Commands;
using MvvmCross.Forms.Views;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Xamarin.Forms;

namespace bonus.app.Core.ViewModels.Manager
{
	public class MenuManagerViewModel : MvxNavigationViewModel
	{
		private MvxCommand _openProfileCommand;
		private MvxCommand _logOutCommand;
		private MvxCommand _openSupportCommand;
		private readonly IAuthService _authService;

		public MenuManagerViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IAuthService authService)
			: base(logProvider, navigationService)
		{
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


		public MvxCommand OpenProfileCommand
		{
			get
			{
				_openProfileCommand = _openProfileCommand ?? new MvxCommand(() =>
				{

					NavigationService.Navigate<ProfileManagerViewModel>();
				});
				return _openProfileCommand;
			}
		}


		public MvxCommand OpenSupportCommand
		{
			get
			{
				_openSupportCommand = _openSupportCommand ??
									  new MvxCommand(() =>
									  {
										  NavigationService.Navigate<ChatViewModel>();
										  ((MasterDetailPage)Application.Current.MainPage).IsPresented = false;
									  });
				return _openSupportCommand;
			}
		}

		private async void LogOutCommandExecute()
		{
			await _authService.Logout(_authService.User);
			await NavigationService.Navigate<AuthorizationViewModel>();
		}
	}
}
