using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace bonus.app.Core.ViewModels
{
	public class BusinessmanBonusAccrualViewModel : MvxNavigationViewModel
	{
		public BusinessmanBonusAccrualViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService)
			: base(logProvider, navigationService)
		{
		}
	}
}
