using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace bonus.app.Core.ViewModels
{
	public class BusinessmanServicesViewModel : MvxNavigationViewModel
	{
		#region .ctor
		public BusinessmanServicesViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService)
			: base(logProvider, navigationService)
		{
		}
		#endregion
	}
}
