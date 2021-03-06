﻿using System;
using System.Threading.Tasks;
using bonus.app.Core.Models;
using bonus.app.Core.Services;
using bonus.app.Core.Services.Interfaces;
using bonus.app.Core.ViewModels.Businessman.Stocks;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace bonus.app.Core.ViewModels.Customer.Stocks
{
	public class FavoriteStocksViewModel : MvxViewModel
	{
		#region Data
		#region Fields
		private bool _isRefreshing;
		private readonly IMvxNavigationService _navigationService;
		private MvxCommand _openCreateShareArchivePageCommand;
		private MvxCommand _openFavoriteStocksCommand;
		private MvxCommand _refreshCommand;
		private Stock _selectedStock;
		private MvxObservableCollection<Stock> _stocks;
		private readonly IStockService _stockService;
		#endregion
		#endregion

		#region .ctor
		public FavoriteStocksViewModel(IMvxNavigationService navigationService, IStockService stockService)
		{
			_stockService = stockService;
			_navigationService = navigationService;
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
														 _navigationService.Navigate<StockArchiveViewModel>();
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
												 _navigationService.Navigate<FavoriteStocksViewModel>();
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
									  Stocks = new MvxObservableCollection<Stock>(await _stockService.All());
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
				_navigationService.Navigate<CustomerStocksDetailViewModel, Guid>(value.Uuid);
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

			Stocks = new MvxObservableCollection<Stock>(await _stockService.FavoriteStocks());
		}
		#endregion
	}
}
