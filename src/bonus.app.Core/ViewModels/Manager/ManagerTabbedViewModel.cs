using bonus.app.Core.ViewModels.Businessman.BonusAccrual;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace bonus.app.Core.ViewModels.Manager
{
	public class ManagerTabbedViewModel : MvxNavigationViewModel
	{
		#region .ctor
		public ManagerTabbedViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService)
			: base(logProvider, navigationService)
		{
		}
		#endregion

		#region Overrided
		public override void ViewAppearing()
		{
			base.ViewAppearing();

			NavigationService.Navigate<ProfileManagerViewModel>();
			NavigationService.Navigate<BusinessmanBonusAccrualViewModel>();
		}
		#endregion
	}
}
