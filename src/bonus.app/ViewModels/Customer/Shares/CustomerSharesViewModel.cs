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
		private IStockService _stockService;
		private MvxObservableCollection<Stock> _shares;
		private Stock _selectedStock;
		private bool _isRefreshing;
		private MvxCommand _refreshCommand;

		public CustomerSharesViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IStockService stockService)
			: base(logProvider, navigationService)
		{
			_stockService = stockService;
		}

		public override async Task Initialize()
		{
			await base.Initialize();

			Shares = new MvxObservableCollection<Stock>(await _stockService.GetAll());
		}

		public MvxObservableCollection<Stock> Shares
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
									  Shares = new MvxObservableCollection<Stock>(await _stockService.GetAll());
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

		public Stock SelectedStock
		{
			get => _selectedStock;
			set
			{
				if (value != null)
				{
					SetProperty(ref _selectedStock, value);
					NavigationService.Navigate<CustomerSharesDetailViewModel, Stock>(value);
				}
			}
		}
	}
}
