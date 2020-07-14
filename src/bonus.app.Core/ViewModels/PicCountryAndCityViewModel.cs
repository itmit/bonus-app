using System;
using System.Linq;
using System.Threading.Tasks;
using bonus.app.Core.Dtos.GeoHelper;
using bonus.app.Core.Models;
using bonus.app.Core.Models.GeoHelperModels;
using bonus.app.Core.Models.UserModels;
using bonus.app.Core.Services;
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
		private int _currentPageNumber;
		private readonly IGeoHelperService _geoHelperService;
		private bool _isVisibleCities;
		private bool _isVisibleCountries;
		private bool _isVisibleSelectedCity;
		private MvxCommand _loadMoreCitiesCommand;
		private City _selectedCity;
		private Country _selectedCountry;
		private MvxCommand _showOrHideCitiesCommand;
		private MvxCommand _showOrHideCountriesCommand;
		private string _searchCountry;
		private MvxCommand _searchCountryCommand;
		private MvxCommand _searchCityCommand;
		private string _searchCity;
		#endregion
		#endregion

		#region .ctor
		public PicCountryAndCityViewModel(IGeoHelperService geoHelperService, IAuthService authService)
		{
			_geoHelperService = geoHelperService;
			_authService = authService;
		}

		public PicCountryAndCityViewModel(IGeoHelperService geoHelperService) => _geoHelperService = geoHelperService;
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
				if (value.Length > 2 || string.IsNullOrEmpty(value))
				{
					SearchCountryCommand.Execute();
				}
			}
		}
		
		public MvxCommand SearchCityCommand
		{
			get
			{
				_searchCityCommand = _searchCityCommand ?? new MvxCommand(() => {
					Cities.Clear();
					LoadCities(SelectedCountry, 1);
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
				if (value.Length > 2 || string.IsNullOrEmpty(value))
				{
					SearchCityCommand.Execute();
				}
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

		public MvxCommand LoadMoreCitiesCommand
		{
			get
			{
				_loadMoreCitiesCommand = _loadMoreCitiesCommand ?? new MvxCommand(() => LoadCities(SelectedCountry, _currentPageNumber + 1));
				return _loadMoreCitiesCommand;
			}
		}

		public City SelectedCity
		{
			get => _selectedCity;
			set
			{
				SetProperty(ref _selectedCity, value);
				ShowOrHideCitiesCommand.Execute();
			}
		}

		public Country SelectedCountry
		{
			get => _selectedCountry;
			set
			{
				SetProperty(ref _selectedCountry, value);
				ShowOrHideCountriesCommand.Execute();
				IsVisibleSelectedCity = true;
				_cities = new MvxObservableCollection<City>();
				LoadCities(value, 1);
				if (User == null || string.IsNullOrEmpty(User.City))
				{
					return;
				}

				_selectedCity = Cities.SingleOrDefault(c => c.LocalizedNames.Ru.Equals(User.City)) ??
								new City
								{
									LocalizedNames = new LocalizedName
									{
										Ru = User.City
									}
								};
				RaisePropertyChanged(() => SelectedCity);
			}
		}

		public MvxCommand ShowOrHideCitiesCommand
		{
			get
			{
				_showOrHideCitiesCommand = _showOrHideCitiesCommand ??
										   new MvxCommand(() =>
										   {
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
			User = _authService?.User;
			try
			{
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

			if (User != null && !string.IsNullOrEmpty(User.Country))
			{
				IsVisibleCities = true;
				IsVisibleCountries = true;
				_selectedCountry = Countries.Single(c => c.LocalizedNames.Ru.Equals(User.Country));
				await RaisePropertyChanged(() => SelectedCountry);
			}
		}
		#endregion

		#region Private
		private async void LoadCities(Country country, int pageNumber)
		{
			if (country == null)
			{
				return;
			}

			var filter = new CityFilterDto
			{
				CountryIso = country.Iso
			};
			if (!string.IsNullOrWhiteSpace(SearchCity))
			{
				filter.Name = SearchCity;
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
															   filter,
															   new PaginationRequestDto
															   {
																   Limit = 250,
																   Page = _currentPageNumber
															   },
															   new OrderDto
															   {
																   By = "population",
																   Dir = "desc"
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
		#endregion
	}
}
