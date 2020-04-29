using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace bonus.app.Core.ViewModels.Businessman.Statistics
{
	public class SalesTypesViewModel : MvxNavigationViewModel
	{
		#region .ctor
		public SalesTypesViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService)
			: base(logProvider, navigationService)
		{
		}
		#endregion
	}
}
