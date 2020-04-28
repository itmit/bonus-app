using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace bonus.app.Core.ViewModels.Customer
{
	public class MainCustomerViewModel : MvxNavigationViewModel
	{
		#region .ctor
		public MainCustomerViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService)
			: base(logProvider, navigationService)
		{
		}
		#endregion

		#region Overrided
		public override async void ViewAppearing()
		{
			base.ViewAppearing();
			await NavigationService.Navigate<MenuCustomerViewModel>();
			await NavigationService.Navigate<MainTabbedCustomerViewModel>();
		}
		#endregion
	}
}
