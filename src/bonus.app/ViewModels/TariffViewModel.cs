using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace bonus.app.Core.ViewModels
{
	public class TariffViewModel : MvxViewModel
	{
		#region Data
		#region Fields
		private IMvxNavigationService _navigationService;
		#endregion
		#endregion

		#region .ctor
		public TariffViewModel(IMvxNavigationService navigationService) => _navigationService = navigationService;
		#endregion
	}
}
