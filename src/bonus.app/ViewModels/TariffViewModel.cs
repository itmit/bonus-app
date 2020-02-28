using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Xamarin.Forms;

namespace bonus.app.Core.ViewModels
{
	public class TariffViewModel : MvxViewModel
	{
		private IMvxNavigationService _navigationService;

		public TariffViewModel(IMvxNavigationService navigationService)
		{
			_navigationService = navigationService;
		}
	}
}
