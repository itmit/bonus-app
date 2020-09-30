using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace bonus.app.Core.ViewModels.Businessman
{
	public class MainBusinessmanViewModel : MvxViewModel
	{
		#region .ctor
		public MainBusinessmanViewModel(IMvxNavigationService navigationService)
		{
			ShowMenuBusinessmanViewModelCommand = new MvxAsyncCommand(async () => await navigationService.Navigate<MenuBusinessmanViewModel>());
			ShowMainTabbedBusinessmanViewModelCommand = new MvxAsyncCommand(async () => await navigationService.Navigate<MainTabbedBusinessmanViewModel>());
		}

		public MvxAsyncCommand ShowMainTabbedBusinessmanViewModelCommand
		{
			get;
		}

		public MvxAsyncCommand ShowMenuBusinessmanViewModelCommand
		{
			get;
		}
		#endregion
	}
}
