using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace bonus.app.Core.ViewModels.Businessman.Statistics
{
	public class TransitionsProfileViewModel : MvxNavigationViewModel
	{
		#region .ctor
		public TransitionsProfileViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService)
			: base(logProvider, navigationService)
		{
		}
		#endregion
	}
}
