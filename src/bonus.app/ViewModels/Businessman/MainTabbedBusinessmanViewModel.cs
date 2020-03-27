using bonus.app.Core.ViewModels.Businessman.BonusAccrual;
using bonus.app.Core.ViewModels.Businessman.News;
using bonus.app.Core.ViewModels.Businessman.Profile;
using bonus.app.Core.ViewModels.Businessman.Services;
using bonus.app.Core.ViewModels.Businessman.Shares;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace bonus.app.Core.ViewModels.Businessman
{
	public class MainTabbedBusinessmanViewModel : MvxNavigationViewModel
	{
		#region .ctor
		public MainTabbedBusinessmanViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService)
			: base(logProvider, navigationService)
		{
		}
		#endregion

		public override void ViewAppearing()
		{
			base.ViewAppearing();

			NavigationService.Navigate<BusinessmanProfileViewModel>();
			NavigationService.Navigate<BusinessmanServicesViewModel>();
			NavigationService.Navigate<BusinessmanSharesViewModel>();
			NavigationService.Navigate<BusinessmanNewsViewModel>();
			NavigationService.Navigate<BusinessmanBonusAccrualViewModel>();
		}
	}
}
