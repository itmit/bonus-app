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

		public override async void ViewAppearing()
		{
			base.ViewAppearing();

			await NavigationService.Navigate<BusinessmanProfileViewModel>();
			await NavigationService.Navigate<BusinessmanServicesViewModel>();
			await NavigationService.Navigate<BusinessmanSharesViewModel>();
			await NavigationService.Navigate<BusinessmanNewsViewModel>();
			await NavigationService.Navigate<BusinessmanBonusAccrualViewModel>();
		}
	}
}
