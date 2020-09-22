using System;
using System.Linq;
using bonus.app.Core.Helpers;
using bonus.app.Core.Models.UserModels;
using bonus.app.Core.Services;
using bonus.app.Core.ViewModels.Auth;
using bonus.app.Core.ViewModels.Chats;
using bonus.app.Core.ViewModels.Customer.BonusAccrual;
using bonus.app.Core.ViewModels.Customer.Profile;
using MvvmCross.Commands;
using MvvmCross.Forms.Views;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Xamarin.Forms;

namespace bonus.app.Core.ViewModels.Customer
{
	public class MenuCustomerViewModel : MvxViewModel
	{
		#region Data
		#region Fields
		private readonly IAuthService _authService;
		private MvxCommand _logOutCommand;
		private readonly IMvxNavigationService _navigationService;
		private MvxCommand _openBonusAccrualCommand;
		private MvxCommand _openProfileCommand;
		private MvxCommand _openSupportCommand;
		#endregion
		#endregion

		#region .ctor
		public MenuCustomerViewModel(IMvxNavigationService navigationService, IAuthService authService)
		{
			_navigationService = navigationService;
			_authService = authService;
		}
		#endregion

		#region Properties
		public MvxCommand LogoutCommand
		{
			get
			{
				_logOutCommand = _logOutCommand ?? new MvxCommand(LogOutCommandExecute);
				return _logOutCommand;
			}
		}

		public MvxCommand OpenBonusAccrualCommand
		{
			get
			{
				_openBonusAccrualCommand = _openBonusAccrualCommand ?? new MvxCommand(OpenBonusAccrualCommandExecute);
				return _openBonusAccrualCommand;
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

		public MvxCommand OpenSupportCommand
		{
			get
			{
				_openSupportCommand = _openSupportCommand ??
									  new MvxCommand(() =>
									  {
										  _navigationService.Navigate<ChatViewModel, ChatViewModelArguments>(new ChatViewModelArguments(new User
										  {
											  Uuid = Guid.Parse(Secrets.SupportClientUuid),
											  Name = "Тех. поддержка"
										  }, null));
										  ((MasterDetailPage) Application.Current.MainPage).IsPresented = false;
									  });
				return _openSupportCommand;
			}
		}
		#endregion

		#region Private
		private async void LogOutCommandExecute()
		{
			await _authService.Logout(_authService.User);
			await _navigationService.Navigate<AuthorizationViewModel>();
		}

		private void OpenBonusAccrualCommandExecute()
		{
			if (Application.Current.MainPage is MasterDetailPage masterDetailPage)
			{
				if (masterDetailPage.Detail is TabbedPage tabbedPage)
				{
					var bonusAccrualPage = tabbedPage.Children.Single(p => ((p as NavigationPage)?.RootPage as IMvxPage)?.ViewModel is CustomerBonusAccrualViewModel ||
																		   (p as IMvxPage)?.ViewModel is CustomerBonusAccrualViewModel);
					if (bonusAccrualPage is NavigationPage bonusAccrualNavigationPage && bonusAccrualNavigationPage.RootPage != bonusAccrualNavigationPage.CurrentPage)
					{
						foreach (var page in bonusAccrualNavigationPage.Navigation.NavigationStack)
						{
							if (bonusAccrualNavigationPage.RootPage == bonusAccrualNavigationPage.CurrentPage)
							{
								break;
							}

							_navigationService.Close(((IMvxPage) page).ViewModel);
						}
					}

					tabbedPage.CurrentPage = bonusAccrualPage;
				}

				masterDetailPage.IsPresented = false;
			}
		}

		private void OpenProfileCommandExecute()
		{
			if (!(Application.Current.MainPage is MasterDetailPage masterDetailPage))
			{
				return;
			}

			if (masterDetailPage.Detail is TabbedPage tabbedPage)
			{
				var profilePage = tabbedPage.Children.SingleOrDefault(p => ((p as NavigationPage)?.RootPage as IMvxPage)?.ViewModel is CustomerProfileViewModel ||
																  (p as IMvxPage)?.ViewModel is CustomerProfileViewModel);
				tabbedPage.CurrentPage = profilePage;

				if (profilePage is NavigationPage profileNavigationPage 
					&& profileNavigationPage.RootPage != profileNavigationPage.CurrentPage)
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
			}

			masterDetailPage.IsPresented = false;
		}
		#endregion
	}
}
