using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace bonus.app.Core.ViewModels
{
	public class MainViewModel : MvxNavigationViewModel
	{
		#region .ctor
		public MainViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService)
			: base(logProvider, navigationService)
		{
		}
		#endregion
	}
}
