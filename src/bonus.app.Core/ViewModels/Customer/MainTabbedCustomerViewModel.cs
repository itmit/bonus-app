using bonus.app.Core.ViewModels.Customer.BonusAccrual;
using bonus.app.Core.ViewModels.Customer.Profile;
using bonus.app.Core.ViewModels.Customer.Services;
using bonus.app.Core.ViewModels.Customer.Shares;
using bonus.app.Core.ViewModels.News;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace bonus.app.Core.ViewModels.Customer
{
	public class MainTabbedCustomerViewModel : MvxNavigationViewModel
	{
		#region .ctor
		public MainTabbedCustomerViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService)
			: base(logProvider, navigationService)
		{
		}
		#endregion

		#region Overrided
		public override void ViewAppearing()
		{
			base.ViewAppearing();

			NavigationService.Navigate<CustomerProfileViewModel>();
			NavigationService.Navigate<CustomerServicesViewModel>();
			NavigationService.Navigate<CustomerSharesViewModel>();
			NavigationService.Navigate<NewsViewModel>();
			NavigationService.Navigate<CustomerBonusAccrualViewModel>();
		}
		#endregion
	}
}
