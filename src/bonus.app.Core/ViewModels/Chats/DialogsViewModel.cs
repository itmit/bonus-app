using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace bonus.app.Core.ViewModels.Chats
{
	public class DialogsViewModel : MvxViewModel
	{
		private IMvxNavigationService _navigationService;

		#region .ctor
		public DialogsViewModel(IMvxNavigationService navigationService)
		{
			_navigationService = navigationService;
		}
		#endregion
	}
}
