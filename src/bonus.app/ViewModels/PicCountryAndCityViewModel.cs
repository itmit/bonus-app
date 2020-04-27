using System;
using System.Linq;
using System.Threading.Tasks;
using bonus.app.Core.Dtos.GeoHelper;
using bonus.app.Core.Models;
using bonus.app.Core.Services;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using Realms;

namespace bonus.app.Core.ViewModels
{
	public class PicCountryAndCityViewModel : MvxViewModel
	{
		private readonly IGeoHelperService _geoHelperService;
		private Country _selectedCountry;
		private MvxObservableCollection<Country> _countries;
		private int _currentPageNumber;
		private MvxObservableCollection<City> _cities;
		private MvxCommand _loadMoreCitiesCommand;
		private readonly IAuthService _authService;
		private bool _isVisibleCountries = true;
		private MvxCommand _showOrHideCountriesCommand;
		private int _countryShapeRotation;
		private bool _isVisibleSelectedCity;
		private MvxCommand _showOrHideCitiesCommand;
		private int _cityShapeRotation;
		private bool _isVisibleCities = true;
		private City _selectedCity;

		public PicCountryAndCityViewModel(IGeoHelperService geoHelperService, IAuthService authService)
		{
			_geoHelperService = geoHelperService;
			_authService = authService;
		}

		public override async Task Initialize()
		{
			await base.Initialize();
			User = _authService.User;
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


			if (!string.IsNullOrEmpty(User.Country))
			{
				_selectedCountry = Countries.Single(c => c.LocalizedNames.Ru.Equals(User.Country));
				await RaisePropertyChanged(() => SelectedCountry);
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

		public MvxCommand LoadMoreCitiesCommand
		{
			get
			{
				_loadMoreCitiesCommand = _loadMoreCitiesCommand ?? new MvxCommand(() => LoadCities(SelectedCountry, _currentPageNumber + 1), () => !IsBusy);
				return _loadMoreCitiesCommand;
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
			}
		}

		public int CountryShapeRotation
		{
			get => _countryShapeRotation;
			set => SetProperty(ref _countryShapeRotation, value);
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

		public int CityShapeRotation
		{
			get => _cityShapeRotation;
			set => SetProperty(ref _cityShapeRotation, value);
		}

		public MvxCommand ShowOrHideCitiesCommand
		{
			get
			{
				_showOrHideCitiesCommand = _showOrHideCitiesCommand ??
										   new MvxCommand(() =>
										   {
											   CityShapeRotation = IsVisibleCities ? 0 : 180;
											   IsVisibleCities= !IsVisibleCities;
										   });
				return _showOrHideCitiesCommand;
			}
		}

		public bool IsVisibleSelectedCity
		{
			get => _isVisibleSelectedCity;
			set => SetProperty(ref _isVisibleSelectedCity, value);
		}

		public bool IsVisibleCountries
		{
			get => _isVisibleCountries;
			set => SetProperty(ref _isVisibleCountries, value);
		}

		public bool IsVisibleCities
		{
			get => _isVisibleCities;
			set => SetProperty(ref _isVisibleCities, value);
		}

		public MvxObservableCollection<Country> Countries
		{
			get => _countries;
			set => SetProperty(ref _countries, value);
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
																   CountryIso = country.Iso,
															   },
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

			if (!string.IsNullOrEmpty(User.City))
			{
				_selectedCity = Cities.SingleOrDefault(c => c.LocalizedNames.Ru.Equals(User.City)) ??
							   new City
				{
					LocalizedNames = new LocalizedName
					{
						Ru = User.City
					}
				};
				await RaisePropertyChanged(() => SelectedCity);
			}
		}

		public bool IsBusy
		{
			get;
			set;
		}

		public MvxObservableCollection<City> Cities
		{
			get => _cities;
			set => SetProperty(ref _cities, value);
		}

		public User User { get; private set; }
	}
}
