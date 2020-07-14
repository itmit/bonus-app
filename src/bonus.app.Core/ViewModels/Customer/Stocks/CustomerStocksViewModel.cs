using System;
using System.Threading.Tasks;
using bonus.app.Core.Models;
using bonus.app.Core.Services;
using bonus.app.Core.ViewModels.Businessman.Stocks;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace bonus.app.Core.ViewModels.Customer.Stocks
{
	public class CustomerStocksViewModel : MvxNavigationViewModel
	{
		#region Data
		#region Fields
		private bool _isFavoriteStocks;
		private bool _isRefreshing;
		private MvxCommand _openCreateShareArchivePageCommand;
		private MvxCommand _openFavoriteStocksCommand;
		private MvxCommand _refreshCommand;
		private Stock _selectedStock;
		private MvxObservableCollection<Stock> _stocks;
		private readonly IStockService _stockService;
		#endregion
		#endregion

		#region .ctor
		public CustomerStocksViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IStockService stockService)
			: base(logProvider, navigationService) =>
			_stockService = stockService;
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

		public MvxCommand OpenFavoriteStocksCommand
		{
			get
			{
				_openFavoriteStocksCommand = _openFavoriteStocksCommand ??
											 new MvxCommand(() =>
											 {
												 NavigationService.Navigate<FavoriteStocksViewModel>();
											 });
				return _openFavoriteStocksCommand;
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
									  Stocks = new MvxObservableCollection<Stock>(await _stockService.GetAll());
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
				NavigationService.Navigate<CustomerStocksDetailViewModel, Guid>(value.Uuid);
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

			Stocks = new MvxObservableCollection<Stock>(await _stockService.GetAll());
		}
		#endregion
	}
}
