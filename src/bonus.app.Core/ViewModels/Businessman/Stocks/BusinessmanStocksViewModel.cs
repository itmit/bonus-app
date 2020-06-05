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
		#region Data
		#region Fields
		private bool _isRefreshing;
		private MvxCommand _openCreateShareArchivePageCommand;
		private MvxCommand _openCreateSharePageCommand;
		private MvxCommand _refreshCommand;
		private Stock _selectedStock;
		private MvxObservableCollection<Stock> _stocks;
		private readonly IStockService _stockService;
		#endregion
		#endregion

		#region .ctor
		public BusinessmanStocksViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IStockService stockService)
			: base(logProvider, navigationService)
		{
			_stockService = stockService;
			_stockService.CreatedStockEventHandler += StockServiceOnCreatedStockEventHandler;
		}
		#endregion

		#region Properties
		public bool IsRefreshing
		{
			get => _isRefreshing;
			private set => SetProperty(ref _isRefreshing, value);
		}

		public MvxCommand OpenArchivePageCommand
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

		public MvxCommand OpenCreateStockPageCommand
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

		public MvxCommand RefreshCommand
		{
			get
			{
				_refreshCommand = _refreshCommand ??
								  new MvxCommand(async () =>
								  {
									  IsRefreshing = true; 
									  await Initialize();
									  IsRefreshing = false;
								  });
				return _refreshCommand;
			}
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

		public MvxObservableCollection<Stock> Stocks
		{
			get => _stocks;
			set => SetProperty(ref _stocks, value);
		}
		#endregion

		#region Overrided
		public override async Task Initialize()
		{
			await base.Initialize();

			Stocks = new MvxObservableCollection<Stock>(await _stockService.GetMyStocks());
		}
		#endregion

		#region Private
		private void StockServiceOnCreatedStockEventHandler(object sender, EventArgs e)
		{
			RefreshCommand.Execute();
		}
		#endregion
	}
}
