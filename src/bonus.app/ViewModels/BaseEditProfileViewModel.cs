﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bonus.app.Core.Dtos.GeoHelper;
using bonus.app.Core.Models;
using bonus.app.Core.Repositories;
using bonus.app.Core.Services;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Realms;

namespace bonus.app.Core.ViewModels
{
	public abstract class BaseEditProfileViewModel : MvxViewModel<EditProfileViewModelArguments>
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
		private City _selectedCity;
		private Country _selectedCountry;
		private User _user;

		private readonly IAuthService _authService;
		private string _phoneNumber;
		private string _address;
		private Dictionary<string, string> _errors;
		#endregion
		#endregion

		#region .ctor
		public BaseEditProfileViewModel(IAuthService authService, IGeoHelperService geoHelperService)
		{
			_authService = authService;
			_geoHelperService = geoHelperService;
		}
		#endregion

		#region Properties
		public MvxObservableCollection<City> Cities
		{
			get => _cities;
			private set => SetProperty(ref _cities, value);
		}

		public Dictionary<string, string> Errors
		{
			get => _errors;
			protected set => SetProperty(ref _errors, value);
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

		public string Address
		{
			get => _address;
			set => SetProperty(ref _address, value);
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

		public string PhoneNumber
		{
			get => _phoneNumber;
			set => SetProperty(ref _phoneNumber, value);
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

			User = _authService.User;
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

			if (Parameter.IsActiveUser)
			{
				SelectedCountry = Countries.Single(c => c.LocalizedNames.Ru.Equals(User.Country));
			}
		}

		public override void Prepare(EditProfileViewModelArguments parameter)
		{
			Parameter = parameter;
		}

		public EditProfileViewModelArguments Parameter
		{
			get;
			private set;
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

			if (Parameter.IsActiveUser && SelectedCountry != null && SelectedCountry.LocalizedNames.Ru.Equals(User.Country))
			{
				SelectedCity = Cities.Single(c => c.LocalizedNames.Ru.Equals(User.City));
			}
		}
		#endregion
	}


	public class EditProfileViewModelArguments
	{
		public EditProfileViewModelArguments(Guid guid, bool isActiveUser, string password = null)
		{
			Guid = guid;
			Password = password;
			IsActiveUser = isActiveUser;
		}

		public bool IsActiveUser
		{
			get;
		}

		public Guid Guid
		{
			get;
		}

		public string Password
		{
			get;
		}
	}
}
