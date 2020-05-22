﻿using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using bonus.app.Core.Models;
using bonus.app.Core.Services;
using bonus.app.Core.ViewModels.Businessman.Services;
using bonus.app.Core.Views.ContentViews.Stocks;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Xamarin.Forms;

namespace bonus.app.Core.ViewModels.Businessman.Stocks
{
	public class StockArchiveViewModel : MvxViewModel, IServiceParentViewModel, IFilterViewModel
	{
		#region Data
		#region Fields
		private readonly IStockService _stockService;
		private MvxObservableCollection<Stock> _stocks;
		private bool _isVisibleServices;
		private MvxCommand _showOrHideTypesServicesCommand;
		private int _shapeRotation;
		private ServiceViewModel _selectedService;
		private MvxObservableCollection<ServiceTypeViewModel> _services;
		private MvxObservableCollection<CreatedServiceViewModel> _myServiceTypes;
		private readonly Mapper _mapper;
		private readonly IServicesService _servicesService;
		private readonly IAuthService _authService;
		private MvxCommand _applyFiltersCommand;
		private bool _isMyStocks;
		private Stock _selectedItem;
		private readonly IMvxNavigationService _navigationService;
		private Stock _selectedStock;
		#endregion
		#endregion

		#region .ctor
		public StockArchiveViewModel(IStockService stockService, IGeoHelperService geoHelperService, IAuthService authService, IServicesService servicesService, IMvxNavigationService navigationService)
		{
			_navigationService = navigationService;
			_authService = authService;
			_stockService = stockService;
			_servicesService = servicesService;
			PicCountryAndCityViewModel = new PicCountryAndCityViewModel(geoHelperService, authService);
			_mapper = new Mapper(new MapperConfiguration(cfg =>
			{
				cfg.CreateMap<ServiceType, ServiceTypeViewModel>()
				   .ForMember(vm => vm.Services, m => m.MapFrom(model => model.Services));

				cfg.CreateMap<ServiceTypeItem, ServiceViewModel>()
				   .ForMember(vm => vm.ParentViewModel, m => m.MapFrom(model => this));
			}));
		}
		#endregion
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
				_navigationService.Navigate<BusinessmanStocksDetailViewModel, Stock>(value);
			}
		}
		#region Overrided
		public override async Task Initialize()
		{
			await PicCountryAndCityViewModel.Initialize();
			await base.Initialize();

			try
			{
				var types = await _servicesService.GetMyServices();
				var type = types.SingleOrDefault(t => t.Name.Equals(_authService.User.Uuid.ToString()));
				if (type != null)
				{
					type.Name = "Ваши услуги";
				}

				var typesVm = _mapper.Map<ServiceTypeViewModel[]>(types);
				Services = new MvxObservableCollection<ServiceTypeViewModel>(typesVm);

				Stocks = new MvxObservableCollection<Stock>(await _stockService.GetMyStock());
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}
		#endregion

		public Stock SelectedItem
		{
			get => _selectedItem;
			set => SetProperty(ref _selectedItem, value);
		}

		public bool IsMyStocks
		{
			get => _isMyStocks;
			set => SetProperty(ref _isMyStocks, value);
		}

		public MvxCommand ApplyFiltersCommand
		{
			get
			{
				_applyFiltersCommand = _applyFiltersCommand ?? new MvxCommand(async () =>
				{
					try
					{
						Stocks = IsMyStocks ? new MvxObservableCollection<Stock>(await _stockService.GetMyStock(SelectedService.Uuid, PicCountryAndCityViewModel.SelectedCity.LocalizedNames.Ru)) 
									 : new MvxObservableCollection<Stock>(await _stockService.GetArchiveStock(SelectedService.Uuid, PicCountryAndCityViewModel.SelectedCity.LocalizedNames.Ru));
					}
					catch (Exception e)
					{
						Console.WriteLine(e);
					}
				});
				return _applyFiltersCommand;
			}
		}

		public ServiceViewModel SelectedService
		{
			get => _selectedService;
			set
			{
				if (_selectedService != null)
				{
					_selectedService.Color = Color.Transparent;
				}

				value.Color = Color.FromHex("#BB8D91");
				SetProperty(ref _selectedService, value);
			}
		}

		public int ShapeRotation
		{
			get => _shapeRotation;
			set => SetProperty(ref _shapeRotation, value);
		}

		public MvxCommand ShowOrHideTypesServicesCommand
		{
			get
			{
				_showOrHideTypesServicesCommand = _showOrHideTypesServicesCommand ??
												  new MvxCommand(() =>
												  {
													  IsVisibleServices = !IsVisibleServices;
													  ShapeRotation = IsVisibleServices ? 180 : 0;
												  });
				return _showOrHideTypesServicesCommand;
			}
		}


		public MvxObservableCollection<CreatedServiceViewModel> MyServiceTypes
		{
			get => _myServiceTypes;
			set => SetProperty(ref _myServiceTypes, value);
		}


		public MvxObservableCollection<ServiceTypeViewModel> Services
		{
			get => _services;
			private set => SetProperty(ref _services, value);
		}


		public PicCountryAndCityViewModel PicCountryAndCityViewModel
		{
			get;
		}

		public bool IsVisibleServices
		{
			get => _isVisibleServices;
			set => SetProperty(ref _isVisibleServices, value);
		}

		public MvxObservableCollection<Stock> Stocks
		{
			get => _stocks;
			set => SetProperty(ref _stocks, value);
		}
	}
}
