using bonus.app.Core.Pages.Manager;
using bonus.app.Core.ViewModels.Businessman;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace bonus.app.Core.ViewModels.Manager
{
	public class MainManagerViewModel : MvxViewModel
	{
		private readonly IMvxNavigationService _navigationService;

		public MainManagerViewModel(IMvxNavigationService navigationService) => _navigationService = navigationService;

		#region Overrided
		public override async void ViewAppearing()
		{
			base.ViewAppearing();
			await _navigationService.Navigate<MenuManagerViewModel>();
			await _navigationService.Navigate<ManagerTabbedViewModel>();
		}
		#endregion
	}
}
