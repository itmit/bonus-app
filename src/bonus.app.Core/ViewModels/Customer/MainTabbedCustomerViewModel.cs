using bonus.app.Core.ViewModels.Customer.BonusAccrual;
using bonus.app.Core.ViewModels.Customer.Profile;
using bonus.app.Core.ViewModels.Customer.Services;
using bonus.app.Core.ViewModels.Customer.Stocks;
using bonus.app.Core.ViewModels.News;
using MvvmCross.Commands;
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

			ShowCustomerProfileViewModelCommand = new MvxAsyncCommand(async () => await navigationService.Navigate<CustomerProfileViewModel>());
			ShowCustomerServicesViewModelCommand = new MvxAsyncCommand(async () => await navigationService.Navigate<CustomerServicesViewModel>());
			ShowCustomerStocksViewModelCommand = new MvxAsyncCommand(async () => await navigationService.Navigate<CustomerStocksViewModel>());
			ShowNewsViewModelCommand = new MvxAsyncCommand(async () => await navigationService.Navigate<NewsViewModel>());
			ShowCustomerBonusAccrualViewModelCommand = new MvxAsyncCommand(async () => await navigationService.Navigate<CustomerBonusAccrualViewModel>());
		}
		#endregion


		public MvxAsyncCommand ShowCustomerBonusAccrualViewModelCommand
		{
			get;
		}

		public MvxAsyncCommand ShowNewsViewModelCommand
		{
			get;
		}

		public MvxAsyncCommand ShowCustomerStocksViewModelCommand
		{
			get;
		}

		public MvxAsyncCommand ShowCustomerServicesViewModelCommand
		{
			get;
		}

		public MvxAsyncCommand ShowCustomerProfileViewModelCommand
		{
			get;
		}
	}
}
