using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace bonus.app.Core.ViewModels.Customer
{
	public class MainCustomerViewModel : MvxNavigationViewModel
	{
		public MainCustomerViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService)
			: base(logProvider, navigationService)
		{
		}



		public override async void ViewAppearing()
		{
			base.ViewAppearing();
			await NavigationService.Navigate<MenuCustomerViewModel>();
			await NavigationService.Navigate<MainTabbedCustomerViewModel>();
		}
	}
}
