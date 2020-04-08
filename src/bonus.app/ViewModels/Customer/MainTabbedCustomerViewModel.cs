using bonus.app.Core.ViewModels.Customer.BonusAccrual;
using bonus.app.Core.ViewModels.Customer.News;
using bonus.app.Core.ViewModels.Customer.Profile;
using bonus.app.Core.ViewModels.Customer.Services;
using bonus.app.Core.ViewModels.Customer.Shares;
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

		public override void ViewAppearing()
		{
			base.ViewAppearing();

			NavigationService.Navigate<CustomerProfileViewModel>();
			NavigationService.Navigate<CustomerServicesViewModel>();
			NavigationService.Navigate<CustomerSharesViewModel>();
			NavigationService.Navigate<CustomerNewsViewModel>();
			NavigationService.Navigate<CustomerBonusAccrualViewModel>();
		}
	}
}
