using bonus.app.Core.ViewModels.Businessman.BonusAccrual;
using bonus.app.Core.ViewModels.Businessman.Profile;
using bonus.app.Core.ViewModels.Businessman.Services;
using bonus.app.Core.ViewModels.Businessman.Stocks;
using bonus.app.Core.ViewModels.News;
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

		#region Overrided
		public override void ViewAppearing()
		{
			base.ViewAppearing();

			NavigationService.Navigate<BusinessmanProfileViewModel>();
			NavigationService.Navigate<BusinessmanServicesViewModel>();
			NavigationService.Navigate<BusinessmanStocksViewModel>();
			NavigationService.Navigate<NewsViewModel>();
			NavigationService.Navigate<BusinessmanBonusAccrualViewModel>();
		}
		#endregion
	}
}
