using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace bonus.app.Core.ViewModels.Businessman
{
	public class MainBusinessmanViewModel : MvxNavigationViewModel
	{
		#region .ctor
		public MainBusinessmanViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService)
			: base(logProvider, navigationService)
		{
		}
		#endregion

		#region Overrided
		public override async void ViewAppearing()
		{
			base.ViewAppearing();
			await NavigationService.Navigate<MenuBusinessmanViewModel>();
			await NavigationService.Navigate<MainTabbedBusinessmanViewModel>();
		}
		#endregion
	}
}
