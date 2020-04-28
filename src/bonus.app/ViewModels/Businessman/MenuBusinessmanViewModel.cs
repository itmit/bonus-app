using System.Linq;
using bonus.app.Core.Services;
using bonus.app.Core.ViewModels.Auth;
using bonus.app.Core.ViewModels.Businessman.Pay;
using bonus.app.Core.ViewModels.Businessman.Profile;
using bonus.app.Core.ViewModels.Businessman.Statistics;
using MvvmCross.Commands;
using MvvmCross.Forms.Views;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Xamarin.Forms;

namespace bonus.app.Core.ViewModels.Businessman
{
	public class MenuBusinessmanViewModel : MvxViewModel
	{
		#region Data
		#region Fields
		private readonly IAuthService _authService;
		private MvxCommand _logOutCommand;
		private readonly IMvxNavigationService _navigationService;
		private MvxCommand _openPayCommand;

		private MvxCommand _openProfileCommand;
		private MvxCommand _openStatisticsCommand;
		private MvxCommand _openSupportCommand;
		private MvxCommand _openTariffCommand;
		#endregion
		#endregion

		#region .ctor
		public MenuBusinessmanViewModel(IAuthService authService, IMvxNavigationService navigationService)
		{
			_authService = authService;
			_navigationService = navigationService;
		}
		#endregion

		#region Properties
		public MvxCommand LogOutCommand
		{
			get
			{
				_logOutCommand = _logOutCommand ?? new MvxCommand(LogOutCommandExecute);
				return _logOutCommand;
			}
		}

		public MvxCommand OpenPayCommand
		{
			get
			{
				_openPayCommand = _openPayCommand ??
								  new MvxCommand(() =>
								  {
									  _navigationService.Navigate<PaySubscribesViewModel>();
									  ((MasterDetailPage) Application.Current.MainPage).IsPresented = false;
								  });
				return _openPayCommand;
			}
		}

		public MvxCommand OpenProfileCommand
		{
			get
			{
				_openProfileCommand = _openProfileCommand ?? new MvxCommand(OpenProfileCommandExecute);
				return _openProfileCommand;
			}
		}

		public MvxCommand OpenStatisticsCommand
		{
			get
			{
				_openStatisticsCommand = _openStatisticsCommand ??
										 new MvxCommand(() =>
										 {
											 _navigationService.Navigate<StatisticsViewModel>();
											 ((MasterDetailPage) Application.Current.MainPage).IsPresented = false;
										 });
				return _openStatisticsCommand;
			}
		}

		public MvxCommand OpenSupportCommand
		{
			get
			{
				_openSupportCommand = _openSupportCommand ??
									  new MvxCommand(() =>
									  {
										  _navigationService.Navigate<ChatViewModel>();
										  ((MasterDetailPage) Application.Current.MainPage).IsPresented = false;
									  });
				return _openSupportCommand;
			}
		}

		public MvxCommand OpenTariffCommand
		{
			get
			{
				_openTariffCommand = _openTariffCommand ??
									 new MvxCommand(() =>
									 {
										 _navigationService.Navigate<TariffViewModel>();
										 ((MasterDetailPage) Application.Current.MainPage).IsPresented = false;
									 });
				return _openTariffCommand;
			}
		}
		#endregion

		#region Public
		public async void LogOutCommandExecute()
		{
			await _authService.Logout(_authService.User);
			await _navigationService.Navigate<AuthorizationViewModel>();
		}
		#endregion

		#region Private
		private void OpenProfileCommandExecute()
		{
			if (Application.Current.MainPage is MasterDetailPage masterDetailPage)
			{
				if (masterDetailPage.Detail is TabbedPage tabbedPage)
				{
					var profilePage = tabbedPage.Children.Single(p => ((p as NavigationPage)?.RootPage as IMvxPage)?.ViewModel is BusinessmanProfileViewModel ||
																	  (p as IMvxPage)?.ViewModel is BusinessmanProfileViewModel);
					tabbedPage.CurrentPage = profilePage;
					if (profilePage is NavigationPage profileNavigationPage && profileNavigationPage.RootPage != profileNavigationPage.CurrentPage)
					{
						foreach (var page in profileNavigationPage.Navigation.NavigationStack)
						{
							if (profileNavigationPage.RootPage == profileNavigationPage.CurrentPage)
							{
								break;
							}

							_navigationService.Close(((IMvxPage) page).ViewModel);
						}
					}

					tabbedPage.CurrentPage = profilePage;
				}

				masterDetailPage.IsPresented = false;
			}
		}
		#endregion
	}
}
