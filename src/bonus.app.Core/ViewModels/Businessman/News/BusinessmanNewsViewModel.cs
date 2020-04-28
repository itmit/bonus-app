using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace bonus.app.Core.ViewModels.Businessman.News
{
	public class BusinessmanNewsViewModel : MvxNavigationViewModel
	{
		#region .ctor
		public BusinessmanNewsViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService)
			: base(logProvider, navigationService)
		{
		}
		#endregion

		#region Public
		public void OpenDetail()
		{
			NavigationService.Navigate<BusinessmanNewsDetailsViewModel>();
		}
		#endregion
	}
}
