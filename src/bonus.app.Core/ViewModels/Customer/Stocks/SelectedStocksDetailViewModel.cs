using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace bonus.app.Core.ViewModels.Customer.Stocks
{
	public class SelectedStocksDetailViewModel : MvxNavigationViewModel
	{
		#region .ctor
		public SelectedStocksDetailViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService)
			: base(logProvider, navigationService)
		{
		}
		#endregion
	}
}
