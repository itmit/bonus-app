using System.Threading.Tasks;
using bonus.app.Core.Models;
using bonus.app.Core.Services;
using bonus.app.Core.ViewModels.Businessman.Shares;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace bonus.app.Core.ViewModels.Customer.Shares
{
	public class CustomerSharesViewModel : MvxNavigationViewModel
	{
		private IShareService _shareService;
		private MvxObservableCollection<Share> _shares;
		private Share _selectedShare;
		private bool _isRefreshing;
		private MvxCommand _refreshCommand;

		public CustomerSharesViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IShareService shareService)
			: base(logProvider, navigationService)
		{
			_shareService = shareService;
		}

		public override async Task Initialize()
		{
			await base.Initialize();

			Shares = new MvxObservableCollection<Share>(await _shareService.GetAll());
		}

		public MvxObservableCollection<Share> Shares
		{
			get => _shares;
			set => SetProperty(ref _shares, value);
		}

		public MvxCommand RefreshCommand
		{
			get
			{
				_refreshCommand = _refreshCommand ??
								  new MvxCommand(async () =>
								  {
									  IsRefreshing = true;
									  Shares = new MvxObservableCollection<Share>(await _shareService.GetAll());
									  IsRefreshing = false;
								  });
				return _refreshCommand;
			}
		}

		public bool IsRefreshing
		{
			get => _isRefreshing;
			private set => SetProperty(ref _isRefreshing, value);
		}

		public Share SelectedShare
		{
			get => _selectedShare;
			set
			{
				if (value != null)
				{
					SetProperty(ref _selectedShare, value);
					NavigationService.Navigate<CustomerSharesDetailViewModel, Share>(value);
				}
			}
		}
	}
}
