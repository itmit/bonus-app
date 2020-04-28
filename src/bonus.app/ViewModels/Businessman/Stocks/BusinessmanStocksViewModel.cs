using System;
using System.Threading.Tasks;
using bonus.app.Core.Models;
using bonus.app.Core.Services;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace bonus.app.Core.ViewModels.Businessman.Stocks
{
	public class BusinessmanStocksViewModel : MvxNavigationViewModel
	{
		private MvxObservableCollection<Stock> _stocks;
		private Stock _selectedStock;
		private readonly IStockService _stockService;
		private MvxCommand _openCreateSharePageCommand;
		private MvxCommand _openCreateShareArchivePageCommand;
		private MvxCommand _refreshCommand;
		private bool _isRefreshing;

		public BusinessmanStocksViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService,  IStockService stockService)
			: base(logProvider, navigationService)
		{
			_stockService = stockService;
			_stockService.CreatedStockEventHandler += StockServiceOnCreatedStockEventHandler;
		}

		private void StockServiceOnCreatedStockEventHandler(object sender, EventArgs e)
		{
			RefreshCommand.Execute();
		}

		public MvxObservableCollection<Stock> Stocks
		{
			get => _stocks;
			set => SetProperty(ref _stocks, value);
		}
		
		public override async Task Initialize()
		{
			await base.Initialize();

			Stocks = new MvxObservableCollection<Stock>(await _stockService.GetMyStock());
		}

		public MvxCommand OpenCreateSharePageCommand
		{
			get
			{
				_openCreateSharePageCommand = _openCreateSharePageCommand ??
											  new MvxCommand(() =>
											  {
												  NavigationService.Navigate<CreateStockViewModel>();
											  });
				return _openCreateSharePageCommand;
			}
		}

		public MvxCommand OpenCreateShareArchivePageCommand
		{
			get
			{
				_openCreateShareArchivePageCommand = _openCreateShareArchivePageCommand ??
													 new MvxCommand(() =>
													 {
														 NavigationService.Navigate<StockArchiveViewModel>();
													 });
				return _openCreateShareArchivePageCommand;
			}
		}
		public MvxCommand RefreshCommand
		{
			get
			{
				_refreshCommand = _refreshCommand ??
								  new MvxCommand(async () =>
								  {
									  IsRefreshing = true;
									  Stocks = new MvxObservableCollection<Stock>(await _stockService.GetMyStock());
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
				if (value == null)
				{
					return;
				}

				SetProperty(ref _selectedStock, value);
				NavigationService.Navigate<BusinessmanStocksDetailViewModel, Stock>(value);
				SetProperty(ref _selectedStock, null);
			}
		}
	}
}
