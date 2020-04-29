using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace bonus.app.Core.ViewModels.Customer.Shares
{
	public class SelectedSharesDetailViewModel : MvxNavigationViewModel
	{
		#region .ctor
		public SelectedSharesDetailViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService)
			: base(logProvider, navigationService)
		{
		}
		#endregion
	}
}
