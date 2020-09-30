using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using bonus.app.Core.Models;
using bonus.app.Core.Models.ServiceModels;
using bonus.app.Core.Services;
using bonus.app.Core.Services.Interfaces;
using bonus.app.Core.ViewModels.Businessman.Services;
using bonus.app.Core.Views.ContentViews.Stocks;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Xamarin.Forms;

namespace bonus.app.Core.ViewModels.Businessman.Stocks
{
	public class StockArchiveViewModel : MvxViewModel, IFilterViewModel
	{
		#region Data
		#region Fields
		private MvxCommand _applyFiltersCommand;
		private readonly IAuthService _authService;
		private bool _isMyStocks;
		private bool _isVisibleServices;
		private readonly Mapper _mapper;
		private MvxObservableCollection<CreatedServiceViewModel> _myServiceTypes;
		private readonly IMvxNavigationService _navigationService;
		private Stock _selectedItem;
		private ServiceViewModel _selectedService;
		private Stock _selectedStock;
		private MvxObservableCollection<ServiceTypeViewModel> _services;
		private readonly IServicesService _servicesService;
		private int _shapeRotation;
		private MvxCommand _showOrHideTypesServicesCommand;
		private MvxObservableCollection<Stock> _stocks;
		private readonly IStockService _stockService;
		#endregion
		#endregion

		#region .ctor
		public StockArchiveViewModel(IStockService stockService,
									 IGeoHelperService geoHelperService,
									 IAuthService authService,
									 IServicesService servicesService,
									 IMvxNavigationService navigationService)
		{
			_navigationService = navigationService;
			_authService = authService;
			_stockService = stockService;
			_servicesService = servicesService;
			PicCountryAndCityViewModel = new PicCountryAndCityViewModel(geoHelperService, authService);
			MyServicesContentViewModel = new MyServicesViewModel(servicesService, authService)
			{
				CanAddService = false
			};
		}
		#endregion

		#region Properties
		public MyServicesViewModel MyServicesContentViewModel
		{
			get;
		}

		public PicCountryAndCityViewModel PicCountryAndCityViewModel
		{
			get;
		}

		public MvxCommand ApplyFiltersCommand
		{
			get
			{
				_applyFiltersCommand = _applyFiltersCommand ??
									   new MvxCommand(async () =>
									   {
										   try
										   {
											   Stocks = IsMyStocks
															? new MvxObservableCollection<Stock>(
																await _stockService.MyArchiveStock(MyServicesContentViewModel.SelectedService.Uuid, PicCountryAndCityViewModel.SelectedCity.LocalizedNames.Ru))
															: new MvxObservableCollection<Stock>(
																await _stockService.ArchiveStock(MyServicesContentViewModel.SelectedService.Uuid,
																									PicCountryAndCityViewModel.SelectedCity.LocalizedNames.Ru));
										   }
										   catch (Exception e)
										   {
											   Console.WriteLine(e);
										   }
									   });
				return _applyFiltersCommand;
			}
		}

		public bool IsVisibleServices
		{
			get => _isVisibleServices;
			set => SetProperty(ref _isVisibleServices, value);
		}

		public Stock SelectedItem
		{
			get => _selectedItem;
			set => SetProperty(ref _selectedItem, value);
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
				_navigationService.Navigate<BusinessmanStocksDetailViewModel, Guid>(value.Uuid);
			}
		}

		public MvxObservableCollection<Stock> Stocks
		{
			get => _stocks;
			set => SetProperty(ref _stocks, value);
		}
		#endregion

		#region IFilterViewModel members
		public bool IsMyStocks
		{
			get => _isMyStocks;
			set => SetProperty(ref _isMyStocks, value);
		}
		#endregion

		#region Overrided
		public override async Task Initialize()
		{
			await PicCountryAndCityViewModel.Initialize();
			await MyServicesContentViewModel.Initialize();
			await base.Initialize();

			try
			{
				Stocks = new MvxObservableCollection<Stock>(await _stockService.MyArchiveStock());
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}
		#endregion
	}
}
