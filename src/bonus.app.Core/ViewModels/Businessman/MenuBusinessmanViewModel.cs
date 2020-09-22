using System;
using System.Linq;
using bonus.app.Core.Helpers;
using bonus.app.Core.Models.UserModels;
using bonus.app.Core.Pages.Businessman;
using bonus.app.Core.Services;
using bonus.app.Core.ViewModels.Auth;
using bonus.app.Core.ViewModels.Businessman.Managers;
using bonus.app.Core.ViewModels.Businessman.Pay;
using bonus.app.Core.ViewModels.Businessman.Profile;
using bonus.app.Core.ViewModels.Businessman.Statistics;
using bonus.app.Core.ViewModels.Chats;
using MvvmCross.Commands;
using MvvmCross.Forms.Views;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Xamarin.Forms;
using XF.Material.Forms.UI.Dialogs;

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
		private MvxCommand _openHelpPageCommand;
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
		public MvxCommand OpenHelpPageCommand
		{
			get
			{
				_openHelpPageCommand = _openHelpPageCommand ??
								  new MvxCommand(() =>
								  {
									  _navigationService.Navigate<HelpViewModel>();
									  ((MasterDetailPage) Application.Current.MainPage).IsPresented = false;
								  });
				return _openHelpPageCommand;
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
		private async void LogOutCommandExecute()
		{
			var loading = await MaterialDialog.Instance.LoadingDialogAsync("Загрузка ...");
			try
			{
				await _authService.Logout(_authService.User);
				await _navigationService.Navigate<AuthorizationViewModel>();
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
			await loading.DismissAsync();
		}
		#endregion

		#region Private
		private void OpenProfileCommandExecute()
		{
			if (!(Application.Current.MainPage is MasterDetailPage masterDetailPage))
			{
				return;
			}

			if (masterDetailPage.Detail is TabbedPage tabbedPage)
			{
				var profilePage = tabbedPage.Children.SingleOrDefault(p => ((p as NavigationPage)?.RootPage as IMvxPage)?.ViewModel is BusinessmanProfileViewModel ||
																  (p as IMvxPage)?.ViewModel is BusinessmanProfileViewModel);
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

				tabbedPage.CurrentPage = profilePage;
			}

			masterDetailPage.IsPresented = false;
		}
		#endregion
	}
}
