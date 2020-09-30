using System;
using System.Linq;
using System.Threading.Tasks;
using bonus.app.Core.Dtos.GeoHelper;
using bonus.app.Core.Models;
using bonus.app.Core.Models.GeoHelperModels;
using bonus.app.Core.Models.UserModels;
using bonus.app.Core.Services;
using bonus.app.Core.Services.Interfaces;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using Realms;

namespace bonus.app.Core.ViewModels
{
	public class PicCountryAndCityViewModel : MvxViewModel
	{
		#region Data
		#region Fields
		private readonly IAuthService _authService;
		private MvxObservableCollection<City> _cities;
		private int _cityShapeRotation;
		private MvxObservableCollection<Country> _countries;
		private int _countryShapeRotation;
		private readonly IGeoHelperService _geoHelperService;
		private bool _isVisibleCities;
		private bool _isVisibleCountries;
		private bool _isVisibleSelectedCity;
		private City _selectedCity;
		private Country _selectedCountry;
		private MvxCommand _showOrHideCitiesCommand;
		private MvxCommand _showOrHideCountriesCommand;
		private string _searchCountry;
		private MvxCommand _searchCountryCommand;
		private MvxCommand _searchCityCommand;
		private string _searchCity;
		private bool _isFirstSelectCountry = true;
		private bool _isFirstSelectCity = true;
		private bool _canPicCountryOrCity = true;
		#endregion
		#endregion

		#region .ctor
		public PicCountryAndCityViewModel(IGeoHelperService geoHelperService, IAuthService authService)
		{
			_geoHelperService = geoHelperService;
			_authService = authService;
		}

		public PicCountryAndCityViewModel(IGeoHelperService geoHelperService) => _geoHelperService = geoHelperService;

		public PicCountryAndCityViewModel(IGeoHelperService geoHelperService, IAuthService authService, MvxViewModel parentViewModel)
		{
			ParentViewModel = parentViewModel;
			_geoHelperService = geoHelperService;
			_authService = authService;
		}

		public PicCountryAndCityViewModel(IGeoHelperService geoHelperService, MvxViewModel parentViewModel)
		{
			_geoHelperService = geoHelperService;
			ParentViewModel = parentViewModel;
		}

		public MvxViewModel ParentViewModel
		{
			get;
		}
		#endregion

		#region Properties
		public bool IsBusy
		{
			get;
			set;
		}

		public MvxCommand SearchCountryCommand
		{
			get
			{
				_searchCountryCommand = _searchCountryCommand ?? new MvxCommand(async () => {
					var countries = await _geoHelperService.GetCountries(new LocaleDto
					{
						FallbackLang = "en",
						Lang = "ru"
					}, SearchCountry);
					if (string.IsNullOrEmpty(SearchCountry))
					{
						countries.Move(countries.Single(c => c.Iso.Equals("RU")), 0);
						countries.Move(countries.Single(c => c.Iso.Equals("UA")), 1);
						countries.Move(countries.Single(c => c.Iso.Equals("BY")), 2);
						countries.Move(countries.Single(c => c.Iso.Equals("KZ")), 3);
						countries.Move(countries.Single(c => c.Iso.Equals("AZ")), 4);
					}
					Countries = new MvxObservableCollection<Country>(countries.Where(c => !string.IsNullOrEmpty(c.LocalizedNames.Ru)));
				});

				return _searchCountryCommand;
			}
		}

		public string SearchCountry
		{
			get => _searchCountry;
			set
			{
				SetProperty(ref _searchCountry, value);
				SearchCountryCommand.Execute();
			}
		}
		
		public MvxCommand SearchCityCommand
		{
			get
			{
				_searchCityCommand = _searchCityCommand ?? new MvxCommand(() => {
					Cities.Clear();
					LoadCities(SelectedCountry);
				});

				return _searchCityCommand;
			}
		}

		public string SearchCity
		{
			get => _searchCity;
			set
			{
				SetProperty(ref _searchCity, value);
				SearchCityCommand.Execute();
			}
		}

		public User User
		{
			get;
			private set;
		}

		public MvxObservableCollection<City> Cities
		{
			get => _cities;
			set => SetProperty(ref _cities, value);
		}

		public int CityShapeRotation
		{
			get => _cityShapeRotation;
			set => SetProperty(ref _cityShapeRotation, value);
		}

		public MvxObservableCollection<Country> Countries
		{
			get => _countries;
			set => SetProperty(ref _countries, value);
		}

		public int CountryShapeRotation
		{
			get => _countryShapeRotation;
			set => SetProperty(ref _countryShapeRotation, value);
		}

		public bool IsVisibleCities
		{
			get => _isVisibleCities;
			set => SetProperty(ref _isVisibleCities, value);
		}

		public bool IsVisibleCountries
		{
			get => _isVisibleCountries;
			set => SetProperty(ref _isVisibleCountries, value);
		}

		public bool IsVisibleSelectedCity
		{
			get => _isVisibleSelectedCity;
			set => SetProperty(ref _isVisibleSelectedCity, value);
		}

		public City SelectedCity
		{
			get => _selectedCity;
			set
			{
				if (!CanPicCountryOrCity)
				{
					return;
				}

				if (_isFirstSelectCity && _selectedCity != null)
				{
					_isFirstSelectCity = false;
					return;
				}

				SetProperty(ref _selectedCity, value);
				ShowOrHideCitiesCommand.Execute();
			}
		}

		public Country SelectedCountry
		{
			get => _selectedCountry;
			set
			{
				if (!CanPicCountryOrCity)
				{
					return;
				}

				if (_isFirstSelectCountry && _selectedCountry != null)
				{
					_isFirstSelectCountry = false;
					return;
				}

				SetProperty(ref _selectedCountry, value);

				ShowOrHideCountriesCommand.Execute();
				IsVisibleSelectedCity = true;
				_cities = new MvxObservableCollection<City>();
				LoadCities(value);
				SelectedCity = null;

				if (User == null || string.IsNullOrEmpty(User.City))
				{
					return;
				}

				RaisePropertyChanged(() => SelectedCity);
			}
		}

		public bool CanPicCountryOrCity
		{
			get => _canPicCountryOrCity;
			set => SetProperty(ref _canPicCountryOrCity, value);
		}

		public MvxCommand ShowOrHideCitiesCommand
		{
			get
			{
				_showOrHideCitiesCommand = _showOrHideCitiesCommand ??
										   new MvxCommand(() =>
										   {
											   if (!CanPicCountryOrCity)
											   {
												   return;
											   }

											   CityShapeRotation = IsVisibleCities ? 0 : 180;
											   IsVisibleCities = !IsVisibleCities;
										   });
				return _showOrHideCitiesCommand;
			}
		}

		public MvxCommand ShowOrHideCountriesCommand
		{
			get
			{
				_showOrHideCountriesCommand = _showOrHideCountriesCommand ??
											  new MvxCommand(() =>
											  {
												  if (!CanPicCountryOrCity)
												  {
													  return;
												  }

												  CountryShapeRotation = IsVisibleCountries ? 0 : 180;
												  IsVisibleCountries = !IsVisibleCountries;
											  });
				return _showOrHideCountriesCommand;
			}
		}
		#endregion

		#region Overrided
		public override async Task Initialize()
		{
			await base.Initialize();

			try
			{
				var countries = await _geoHelperService.GetCountries(new LocaleDto
				{
					FallbackLang = "en",
					Lang = "ru"
				});
				var countriesCollection = new MvxObservableCollection<Country>
				{
					countries.Single(c => c.Iso.Equals("RU")),
					countries.Single(c => c.Iso.Equals("UA")),
					countries.Single(c => c.Iso.Equals("BY")),
					countries.Single(c => c.Iso.Equals("KZ")),
					countries.Single(c => c.Iso.Equals("AZ"))
				};
				Countries = new MvxObservableCollection<Country>(countriesCollection);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}

			if (_authService == null) { 
				return;
			}

			if (!_authService.UserIsAuthorized)
			{
				return;
			}

			User = _authService.User;

			if (User != null && !string.IsNullOrEmpty(User.Country))
			{
				_selectedCountry = Countries.Single(c => c.LocalizedNames.Ru.Equals(User.Country));
				await RaisePropertyChanged(() => SelectedCountry);
				if (SelectedCountry != null && !string.IsNullOrEmpty(User.City))
				{
					IsVisibleSelectedCity = true;
					var cities = await _geoHelperService.GetCities(new LocaleDto
															  {
																  FallbackLang = "en",
																  Lang = "ru"
															  },
															  new CityFilterDto
															  {
																  CountryIso = SelectedCountry.Iso,
																  Name = User.City
															  },
															  new PaginationRequestDto
															  {
																  Limit = 100,
																  Page = 1
															  },
															  new OrderDto
															  {
																  By = "population",
																  Dir = "desc"
															  });
					_selectedCity = cities.FirstOrDefault();
					await RaisePropertyChanged(() => SelectedCity);

					if (CanPicCountryOrCity)
					{
						LoadCities(SelectedCountry);
					}
				}
			}
		}
		#endregion

		#region Private
		private async void LoadCities(Country country)
		{
			if (country == null)
			{
				return;
			}

			var filter = new CityFilterDto
			{
				CountryIso = country.Iso,
				Name = SearchCity
			};

			IsBusy = true;
			try
			{
				var cities = await _geoHelperService.GetCities(new LocaleDto
															   {
																   FallbackLang = "en",
																   Lang = "ru"
															   },
															   filter,
															   new PaginationRequestDto
															   {
																   Limit = 100,
																   Page = 1
															   },
															   new OrderDto
															   {
																   By = "population",
																   Dir = "desc"
															   });
				MvxObservableCollection<City> collection;
				if (string.IsNullOrEmpty(SearchCity))
				{
					collection = new MvxObservableCollection<City>(cities.Where(c => !string.IsNullOrEmpty(c.LocalizedNames.Ru)).OrderBy(c => c.Name));
					var mos = collection.SingleOrDefault(c => c.Id == 4995);
					if (mos != null)
					{
						collection.Move(collection.IndexOf(mos), 0);
					}
					var pet = collection.SingleOrDefault(c => c.Id == 5000);
					if (pet != null)
					{
						collection.Move(collection.IndexOf(pet), 1);
					}
				}
				else
				{
					collection = new MvxObservableCollection<City>(cities.Where(c => !string.IsNullOrEmpty(c.LocalizedNames.Ru)));
				}
				Cities = collection;
				await RaisePropertyChanged(() => Cities);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}

			IsBusy = false;
		}
		#endregion
	}
}
