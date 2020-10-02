using bonus.app.Core.ViewModels.Businessman.BonusAccrual;
using bonus.app.Core.ViewModels.Businessman.Profile;
using bonus.app.Core.ViewModels.Businessman.Services;
using bonus.app.Core.ViewModels.Businessman.Stocks;
using bonus.app.Core.ViewModels.News;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace bonus.app.Core.ViewModels.Businessman
{
	public class MainTabbedBusinessmanViewModel : MvxViewModel
	{
		#region .ctor
		public MainTabbedBusinessmanViewModel(IMvxNavigationService navigationService)
		{
			ShowBusinessmanProfileViewModelCommand = new MvxAsyncCommand(async () => await navigationService.Navigate<BusinessmanProfileViewModel>());
			ShowBusinessmanServicesViewModelCommand = new MvxAsyncCommand(async () => await navigationService.Navigate<BusinessmanServicesViewModel>());
			ShowBusinessmanStocksViewModelCommand = new MvxAsyncCommand(async () => await navigationService.Navigate<BusinessmanStocksViewModel>());
			ShowNewsViewModelCommand = new MvxAsyncCommand(async () => await navigationService.Navigate<NewsViewModel>());
			ShowBusinessmanBonusAccrualViewModelCommand = new MvxAsyncCommand(async () => await navigationService.Navigate<BusinessmanBonusAccrualViewModel>());
		}
		#endregion

		public MvxAsyncCommand ShowBusinessmanBonusAccrualViewModelCommand
		{
			get;
		}

		public MvxAsyncCommand ShowNewsViewModelCommand
		{
			get;
		}

		public MvxAsyncCommand ShowBusinessmanStocksViewModelCommand
		{
			get;
		}

		public MvxAsyncCommand ShowBusinessmanServicesViewModelCommand
		{
			get;
		}

		public MvxAsyncCommand ShowBusinessmanProfileViewModelCommand
		{
			get;
		}
	}
}
