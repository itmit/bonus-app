using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using bonus.app.Core.Dtos.GeoHelper;
using bonus.app.Core.Models;
using bonus.app.Core.Repositories;
using bonus.app.Core.Services;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Realms;
using Xamarin.Forms;

namespace bonus.app.Core.ViewModels.Businessman.Shares
{
	public class CreateShareViewModel : MvxViewModel
	{
		#region Data
		#region Fields
		private MvxObservableCollection<City> _cities;
		private MvxObservableCollection<Country> _countries;
		private int _currentPageNumber;
		private Dictionary<string, string> _errors = new Dictionary<string, string>();
		private readonly IGeoHelperService _geoHelperService;
		private bool _isBusy;
		private MvxCommand _loadMoreCitiesCommand;
		private Country _selectedCountry;
		private readonly IShareService _shareService;
		private string _name;
		private string _description;
		private City _selectedCity;
		private MvxCommand _picPhotoCommand;
		private readonly IPermissionsService _permissionsService;
		private string _imageName;
		private bool _hasNoImage = true;
		private DateTime _shareTime = DateTime.Today;
		#endregion
		#endregion

		#region .ctor
		public CreateShareViewModel(IShareService shareService, IGeoHelperService geoHelperService, IPermissionsService permissionsService)
		{
			_shareService = shareService;
			_geoHelperService = geoHelperService;
			_permissionsService = permissionsService;
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

		public Dictionary<string, string> Errors
		{
			get => _errors;
			protected set
			{
				if (value == null)
				{
					SetProperty(ref _errors, new Dictionary<string, string>());
					return;
				}
				SetProperty(ref _errors, value);
			}
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
				}
				RaisePropertyChanged(() => Errors);
			}
		}

		public bool IsBusy
		{
			get => _isBusy;
			set => SetProperty(ref _isBusy, value);
		}

		public MvxCommand PicImageCommand
		{
			get
			{
				_picPhotoCommand = _picPhotoCommand ?? new MvxCommand(PicImageCommandExecute);
				return _picPhotoCommand;
			}
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

				ImageName = image.Path.Substring(image.Path.LastIndexOf('/') + 1);

				using (var memoryStream = new MemoryStream())
				{
					image.GetStream()
						 .CopyTo(memoryStream);
					image.Dispose();
				}
			}
		}

		public bool HasNoImage
		{
			get => _hasNoImage;
			private set => SetProperty(ref _hasNoImage, value);
		}

		public string ImageName
		{
			get => _imageName;
			private set
			{
				SetProperty(ref _imageName, value);
				HasNoImage = string.IsNullOrEmpty(value);
			}
		}

		public string Name
		{
			get => _name;
			set
			{
				SetProperty(ref _name, value);
				if (string.IsNullOrEmpty(value))
				{
					Errors[nameof(Name)
							   .ToLower()] = "Название акции не может быть пустым.";
				}
				else if (value.Length < 4)
				{
					Errors[nameof(Name)
							   .ToLower()] = "Название акции не может содержать меньше 4 символов.";
				}
				else
				{
					Errors[nameof(Name)
							   .ToLower()] = null;
				}
				RaisePropertyChanged(() => Errors);
			}
		}

		public string Description
		{
			get => _description;
			set
			{
				SetProperty(ref _description, value);
				if (string.IsNullOrEmpty(value))
				{
					Errors[nameof(Description)
							   .ToLower()] = "Описание не может быть пустым.";
				}
				else
				{
					Errors[nameof(Description)
							   .ToLower()] = null;
				}
				RaisePropertyChanged(() => Errors);
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
