using MvvmCross.Commands;
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

			ShowMenuCustomerViewModelCommand = new MvxAsyncCommand(async () => await navigationService.Navigate<MenuCustomerViewModel>());
			ShowMainTabbedCustomerViewModelCommand = new MvxAsyncCommand(async () => await navigationService.Navigate<MainTabbedCustomerViewModel>());
		}

		public MvxAsyncCommand ShowMainTabbedCustomerViewModelCommand
		{
			get;
		}

		public MvxAsyncCommand ShowMenuCustomerViewModelCommand
		{
			get;
		}
		#endregion
	}
}
