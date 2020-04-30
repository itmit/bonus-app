﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using bonus.app.Core.Dtos.GeoHelper;
using bonus.app.Core.Models;
using bonus.app.Core.Services;
using bonus.app.Core.ViewModels.Businessman.Services;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.Permissions.Abstractions;
using Realms;
using Xamarin.Forms;

namespace bonus.app.Core.ViewModels.Businessman.Stocks
{
	public class EditorStockViewModel : MvxViewModel<Stock, Stock>, IServiceParentViewModel
	{
		#region Data
		#region Fields
		private bool _canCreateShareCommand = true;
		private MvxObservableCollection<City> _cities;
		private MvxObservableCollection<Country> _countries;
		private int _currentPageNumber;
		private Dictionary<string, string> _errors = new Dictionary<string, string>();
		private readonly IGeoHelperService _geoHelperService;
		private byte[] _imageBytes;
		private string _imageSource;
		private bool _isBusy;
		private bool _isFirstCall = true;
		private bool _isVisibleCity;
		private MvxCommand _loadMoreCitiesCommand;
		private readonly Mapper _mapper;
		private readonly IMvxNavigationService _navigationService;
		private readonly IPermissionsService _permissionsService;
		private MvxCommand _picPhotoCommand;
		private MvxCommand _saveCommand;
		private City _selectedCity;
		private Country _selectedCountry;
		private ServiceViewModel _selectedService;
		private MvxObservableCollection<ServiceTypeViewModel> _services;
		private readonly IServicesService _servicesServices;
		private DateTime _shareTime = DateTime.Today;
		private Stock _stock;
		private readonly IStockService _stockService;
		#endregion
		#endregion

		#region .ctor
		public EditorStockViewModel(IGeoHelperService geoHelperService,
									IServicesService servicesServices,
									IStockService stockService,
									IPermissionsService permissionsService,
									IMvxNavigationService navigationService)
		{
			_navigationService = navigationService;
			_permissionsService = permissionsService;
			_geoHelperService = geoHelperService;
			_servicesServices = servicesServices;
			_stockService = stockService;
			_mapper = new Mapper(new MapperConfiguration(cfg =>
			{
				cfg.CreateMap<ServiceType, ServiceTypeViewModel>()
				   .ForMember(vm => vm.Services, m => m.MapFrom(model => model.Services));

				cfg.CreateMap<Service, ServiceViewModel>()
				   .ForMember(vm => vm.ParentViewModel, m => m.MapFrom(model => this));
			}));
		}
		#endregion

		#region Properties
		public bool CanCreateShareCommand
		{
			get => _canCreateShareCommand;
			set => SetProperty(ref _canCreateShareCommand, value);
		}

		public MvxObservableCollection<City> Cities
		{
			get => _cities;
			private set => SetProperty(ref _cities, value);
		}

		public MvxObservableCollection<Country> Countries
		{
			get => _countries;
			private set => SetProperty(ref _countries, value);
		}

		public string Description
		{
			get => Stock.Description;
			set
			{
				Stock.Description = value;
				RaisePropertyChanged(() => Description);
			}
		}

		public Dictionary<string, string> Errors
		{
			get => _errors;
			set
			{
				if (value == null)
				{
					SetProperty(ref _errors, new Dictionary<string, string>());
					return;
				}

				SetProperty(ref _errors, value);
			}
		}

		public string ImageName
		{
			get => Stock.ImageSource;
			private set
			{
				Stock.ImageSource = value;
				RaisePropertyChanged(() => ImageName);
			}
		}

		public string ImageSource
		{
			get => _imageSource;
			set => SetProperty(ref _imageSource, value);
		}

		public bool IsBusy
		{
			get => _isBusy;
			set => SetProperty(ref _isBusy, value);
		}

		public bool IsVisibleCity
		{
			get => _isVisibleCity;
			set => SetProperty(ref _isVisibleCity, value);
		}

		public MvxCommand LoadMoreCitiesCommand
		{
			get
			{
				_loadMoreCitiesCommand = _loadMoreCitiesCommand ?? new MvxCommand(() => LoadCities(SelectedCountry, _currentPageNumber + 1), () => !IsBusy);
				return _loadMoreCitiesCommand;
			}
		}

		public string Name
		{
			get => Stock.Name;
			set
			{
				Stock.Name = value;
				RaisePropertyChanged(() => Name);
			}
		}

		public MvxCommand PicImageCommand
		{
			get
			{
				_picPhotoCommand = _picPhotoCommand ?? new MvxCommand(PicImageCommandExecute);
				return _picPhotoCommand;
			}
		}

		public MvxCommand SaveCommand
		{
			get
			{
				_saveCommand = _saveCommand ?? new MvxCommand(SaveCommandExecute);
				return _saveCommand;
			}
		}

		public City SelectedCity
		{
			get => _selectedCity;
			set => SetProperty(ref _selectedCity, value);
		}

		public Country SelectedCountry
		{
			get => _selectedCountry;
			set
			{
				SetProperty(ref _selectedCountry, value);
				_cities = new MvxObservableCollection<City>();
				LoadCities(value, 1);
			}
		}

		public MvxObservableCollection<ServiceTypeViewModel> Services
		{
			get => _services;
			private set => SetProperty(ref _services, value);
		}

		public DateTime ShareTime
		{
			get => _shareTime;
			set
			{
				SetProperty(ref _shareTime, value);
				if (value <= DateTime.Today)
				{
					Errors["share_time"] = "Срок размещения акции должен быть актуальным.";
				}
				else
				{
					Errors["share_time"] = null;
					Stock.ShareTime = value;
				}

				RaisePropertyChanged(() => Errors);
			}
		}

		public Stock Stock
		{
			get => _stock;
			private set => SetProperty(ref _stock, value);
		}
		#endregion

		#region IServiceParentViewModel members
		public ServiceViewModel SelectedService
		{
			get => _selectedService;
			set
			{
				if (_selectedService != null)
				{
					_selectedService.Color = Color.Transparent;
				}

				Stock.Service = value.Uuid;
				value.Color = Color.FromHex("#BB8D91");
				SetProperty(ref _selectedService, value);
			}
		}
		#endregion

		#region Overrided
		public override async Task Initialize()
		{
			await base.Initialize();

			try
			{
				var typesVm = _mapper.Map<ServiceTypeViewModel[]>(await _servicesServices.GetAll());
				Services = new MvxObservableCollection<ServiceTypeViewModel>(typesVm);
				var countries = await _geoHelperService.GetCountries(new LocaleDto
				{
					FallbackLang = "en",
					Lang = "ru"
				});
				countries.Move(countries.Single(c => c.Iso.Equals("RU")), 0);
				countries.Move(countries.Single(c => c.Iso.Equals("UA")), 1);
				countries.Move(countries.Single(c => c.Iso.Equals("BY")), 2);
				countries.Move(countries.Single(c => c.Iso.Equals("KZ")), 3);
				countries.Move(countries.Single(c => c.Iso.Equals("AZ")), 4);
				Countries = new MvxObservableCollection<Country>(countries.Where(c => !string.IsNullOrEmpty(c.LocalizedNames.Ru)));
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}

			await Task.Run(async () =>
			{
				try
				{
					Stock = await _stockService.GetStockForEdit(Stock.Uuid);
					_shareTime = Stock.ShareTime;
					ImageSource = Stock.ImageSource;
					await RaisePropertyChanged(() => ShareTime);

					_selectedCountry = Countries.SingleOrDefault(country => country.LocalizedNames.Ru.Equals(Stock.Country));
					await RaisePropertyChanged(() => SelectedCountry);

					_cities = new MvxObservableCollection<City>();
					LoadCities(_selectedCountry, 1);
					SelectedCity = Cities.SingleOrDefault(city => city.LocalizedNames.Ru.Equals(Stock.City));
					IsVisibleCity = SelectedCity != null;

					var temp = Services.SingleOrDefault(model => model.Services.Any(service => service.Uuid == Stock.Service));
					var ser = temp?.Services.SingleOrDefault(service => service.Uuid == Stock.Service);
					SelectedService = ser;
				}
				catch (Exception e)
				{
					Console.WriteLine(e);
				}
			});
		}

		public override void Prepare(Stock parameter)
		{
			Stock = parameter;
		}
		#endregion

		#region Private
		private bool IsValid()
		{
			CanCreateShareCommand = false;
			var result = true;
			if (SelectedCountry == null)
			{
				Device.BeginInvokeOnMainThread(() =>
				{
					Application.Current.MainPage.DisplayAlert("Внимание", "Выберите страну.", "Ок");
				});
				result = false;
			}

			if (SelectedCity == null && result)
			{
				Device.BeginInvokeOnMainThread(() =>
				{
					Application.Current.MainPage.DisplayAlert("Внимание", "Выберите город.", "Ок");
				});
				result = false;
			}

			if (SelectedService == null && result)
			{
				Device.BeginInvokeOnMainThread(() =>
				{
					Application.Current.MainPage.DisplayAlert("Внимание", "Выберите услугу.", "Ок");
				});
				result = false;
			}

			var name = Name?.Trim();
			if (string.IsNullOrEmpty(name))
			{
				Errors[nameof(Name)
						   .ToLower()] = "Название акции не может быть пустым.";

				result = false;
			}
			else if (name.Length < 4)
			{
				Errors[nameof(Name)
						   .ToLower()] = "Название акции не может содержать меньше 4 символов.";

				result = false;
			}
			else
			{
				Errors[nameof(Name)
						   .ToLower()] = null;
			}

			if (string.IsNullOrEmpty(Description?.Trim()))
			{
				Errors[nameof(Description)
						   .ToLower()] = "Описание не может быть пустым.";
				result = false;
			}
			else
			{
				Errors[nameof(Description)
						   .ToLower()] = null;
			}

			if (ShareTime <= DateTime.Today)
			{
				Errors["share_time"] = "Срок размещения акции должен быть актуальным.";
				result = false;
			}
			else
			{
				Errors["share_time"] = null;
			}

			CanCreateShareCommand = !result;
			RaisePropertyChanged(() => Errors);
			return result;
		}

		private async void LoadCities(Country country, int pageNumber)
		{
			if (country == null)
			{
				return;
			}

			IsBusy = true;
			_currentPageNumber = pageNumber;
			try
			{
				var cities = await _geoHelperService.GetCities(new LocaleDto
															   {
																   FallbackLang = "en",
																   Lang = "ru"
															   },
															   new CityFilterDto
															   {
																   CountryIso = country.Iso
															   },
															   new PaginationRequestDto
															   {
																   Limit = 250,
																   Page = _currentPageNumber
															   },
															   new OrderDto
															   {
																   By = "name",
																   Dir = "asc"
															   });
				cities.Insert(0,
							  new City
							  {
								  Name = "Москва",
								  Id = 4995,
								  LocalizedNames = new LocalizedName
								  {
									  En = "Moskva",
									  Ru = "Москва"
								  },
								  RegionId = 55
							  });
				cities.Insert(1,
							  new City
							  {
								  Name = "Санкт-Петербург",
								  Id = 5000,
								  LocalizedNames = new LocalizedName
								  {
									  En = "Sankt-Peterburg",
									  Ru = "Санкт-Петербург"
								  },
								  RegionId = 48
							  });
				Cities.AddRange(cities.Where(c => !string.IsNullOrEmpty(c.LocalizedNames.Ru)));
				await RaisePropertyChanged(() => Cities);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}

			IsBusy = false;
		}

		private async void PicImageCommandExecute()
		{
			if (await _permissionsService.CheckPermission(Permission.Storage, "Для загрузки аватара необходимо разрешение на использование хранилища."))
			{
				if (!CrossMedia.Current.IsPickPhotoSupported)
				{
					return;
				}

				var image = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
				{
					PhotoSize = PhotoSize.Medium
				});

				if (image == null)
				{
					return;
				}

				ImageSource = image.Path;
				ImageName = image.Path.Substring(image.Path.LastIndexOf('/') + 1);

				using (var memoryStream = new MemoryStream())
				{
					image.GetStream()
						 .CopyTo(memoryStream);
					image.Dispose();
					_imageBytes = memoryStream.ToArray();
				}
			}
		}

		private async void SaveCommandExecute()
		{
			if (!IsValid())
			{
				return;
			}

			var res = false;
			try
			{
				res = await _stockService.EditStock(Stock, _imageBytes);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}

			if (res)
			{
				await _navigationService.Close(this, Stock);
			}
			else
			{
				CanCreateShareCommand = true;
				Device.BeginInvokeOnMainThread(() =>
				{
					Application.Current.MainPage.DisplayAlert("Внимание", "Ошибка", "Ок");
				});
			}
		}
		#endregion
	}
}