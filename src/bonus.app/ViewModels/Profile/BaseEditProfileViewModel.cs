using System;
using System.Linq;
using System.Threading.Tasks;
using bonus.app.Core.Dto.GeoHelper;
using bonus.app.Core.Models;
using bonus.app.Core.Repositories;
using bonus.app.Core.Services;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Realms;

namespace bonus.app.Core.ViewModels.Profile
{
	public abstract class BaseEditProfileViewModel : MvxViewModel
	{
		#region Data
		#region Fields
		private MvxObservableCollection<City> _cities = new MvxObservableCollection<City>();
		private MvxObservableCollection<Country> _countries;
		private int _currentPageNumber;
		private readonly IGeoHelperService _geoHelperService;
		private bool _isAuthorization;
		private bool _isBusy;
		private MvxCommand _loadMoreCitiesCommand;
		private readonly IMvxNavigationService _navigationService;
		private City _selectedCity;
		private Country _selectedCountry;
		private User _user;

		private readonly IUserRepository _userRepository;
		#endregion
		#endregion

		#region .ctor
		public BaseEditProfileViewModel(IUserRepository userRepository, IMvxNavigationService navigationService, IGeoHelperService geoHelperService)
		{
			_userRepository = userRepository;
			_navigationService = navigationService;
			_geoHelperService = geoHelperService;
		}
		#endregion

		#region Properties
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

		public bool IsAuthorization
		{
			get => _isAuthorization;
			private set => SetProperty(ref _isAuthorization, value);
		}

		public bool IsBusy
		{
			get => _isBusy;
			set => SetProperty(ref _isBusy, value);
		}

		public MvxCommand LoadMoreCitiesCommand
		{
			get
			{
				_loadMoreCitiesCommand = _loadMoreCitiesCommand ?? new MvxCommand(() => LoadCities(SelectedCountry, _currentPageNumber + 1), () => !IsBusy);
				return _loadMoreCitiesCommand;
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

		public User User
		{
			get => _user;
			private set => SetProperty(ref _user, value);
		}
		#endregion

		#region Overrided
		public override async Task Initialize()
		{
			await base.Initialize();

			User = _userRepository.GetAll()
								  .SingleOrDefault();
			IsAuthorization = User != null;

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
		}
		#endregion

		#region Private
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
