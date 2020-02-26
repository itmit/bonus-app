using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace bonus.app.Core.ViewModels.Businessman.Pay
{
	public class PaySubscribesViewModel : MvxNavigationViewModel
	{
		public PaySubscribesViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService)
			: base(logProvider, navigationService)
		{
		}
	}
}
