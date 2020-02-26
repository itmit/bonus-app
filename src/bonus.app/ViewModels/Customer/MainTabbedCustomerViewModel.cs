using bonus.app.Core.Page.Customer.Shares;
using bonus.app.Core.ViewModels.Customer.News;
using bonus.app.Core.ViewModels.Customer.Shares;
using bonus.app.Core.ViewModels.Profile;
using bonus.app.Core.ViewModels.Services;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace bonus.app.Core.ViewModels.Customer
{
	public class MainTabbedCustomerViewModel : MvxNavigationViewModel
	{
		public MainTabbedCustomerViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService)
			: base(logProvider, navigationService)
		{
		}

		public override async void ViewAppearing()
		{
			base.ViewAppearing();

			await NavigationService.Navigate<CustomerProfileViewModel>();
			await NavigationService.Navigate<CustomerServicesViewModel>();
			await NavigationService.Navigate<CustomerSharesViewModel>();
			await NavigationService.Navigate<CustomerNewsViewModel>();
			await NavigationService.Navigate<BonusAccrualViewModel>();
		}
	}
}
