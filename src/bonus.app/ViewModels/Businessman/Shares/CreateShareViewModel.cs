using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using bonus.app.Core.Dtos.GeoHelper;
using bonus.app.Core.Models;
using bonus.app.Core.Services;
using bonus.app.Core.ViewModels.Businessman.Popups;
using bonus.app.Core.ViewModels.Businessman.Services;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.Permissions.Abstractions;
using Realms;
using Xamarin.Forms;

namespace bonus.app.Core.ViewModels.Businessman.Shares
{
	public class CreateShareViewModel : MvxViewModel, IServiceParentViewModel
	{
		#region Data
		#region Fields
		private string _description;
		private Dictionary<string, string> _errors = new Dictionary<string, string>();
		private string _imageName;
		private string _imageSource;
		private bool _isSubscriberOnly;
		private readonly Mapper _mapper;
		private string _name;
		private readonly IMvxNavigationService _navigationService;
		private readonly IPermissionsService _permissionsService;
		private MvxCommand _picPhotoCommand;
		private ServiceViewModel _selectedService;
		private MvxObservableCollection<ServiceTypeViewModel> _services;
		private readonly IServicesService _servicesServices;
		private readonly IStockService _stockService;
		private DateTime _shareTime = DateTime.Today;
		private MvxCommand _showShareCommand;
		private MvxCommand _createShareCommand;
		private byte[] _imageBytes;
		private bool _canCreateShareCommand = true;
		#endregion
		#endregion

		#region .ctor
		public CreateShareViewModel(IStockService stockService,
									IGeoHelperService geoHelperService,
									IPermissionsService permissionsService,
									IServicesService servicesServices,
									IMvxNavigationService navigationService,
									IAuthService authService)
		{
			_stockService = stockService;
			_permissionsService = permissionsService;
			_servicesServices = servicesServices;
			_navigationService = navigationService;
			_mapper = new Mapper(new MapperConfiguration(cfg =>
			{
				cfg.CreateMap<ServiceType, ServiceTypeViewModel>()
				   .ForMember(vm => vm.Services, m => m.MapFrom(model => model.Services));

				cfg.CreateMap<Service, ServiceViewModel>()
				   .ForMember(vm => vm.ParentViewModel, m => m.MapFrom(model => this));
			}));
			PicCountryAndCityViewModel = new PicCountryAndCityViewModel(geoHelperService, authService);
		}
		#endregion

		#region Properties
		public string Description
		{
			get => _description;
			set
			{
				SetProperty(ref _description, value);
				if (string.IsNullOrEmpty(value?.Trim()))
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

		public string ImageName
		{
			get => _imageName;
			private set => SetProperty(ref _imageName, value);
		}

		public string ImageSource
		{
			get => _imageSource;
			private set
			{
				SetProperty(ref _imageSource, value);
				RaisePropertyChanged(() => IsShowDefaultImage);
			}
		}

		public bool IsSubscriberOnly
		{
			get => _isSubscriberOnly;
			set => SetProperty(ref _isSubscriberOnly, value);
		}

		public string Name
		{
			get => _name;
			set
			{
				SetProperty(ref _name, value);
				var tVal = value?.Trim();
				if (string.IsNullOrEmpty(tVal))
				{
					Errors[nameof(Name)
							   .ToLower()] = "Название акции не может быть пустым.";
				}
				else if (tVal.Length < 4)
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

		public MvxCommand PicImageCommand
		{
			get
			{
				_picPhotoCommand = _picPhotoCommand ?? new MvxCommand(PicImageCommandExecute);
				return _picPhotoCommand;
			}
		}

		public MvxObservableCollection<ServiceTypeViewModel> Services
		{
			get => _services;
			private set => SetProperty(ref _services, value);
		}

		public bool IsShowDefaultImage => string.IsNullOrEmpty(ImageSource);

		public MvxCommand CreateShareCommand
		{
			get
			{
				_createShareCommand = _createShareCommand ?? new MvxCommand(CreateShareCommandExecute);
				return _createShareCommand;
			}
		}

		private async void CreateShareCommandExecute()
		{
			if (!CheckValidFields())
			{
				return;
			}

			bool res = false;
			try
			{
				res = await _stockService.CreateStock(new Stock
				{
					Country = PicCountryAndCityViewModel.SelectedCountry.LocalizedNames.Ru,
					City = PicCountryAndCityViewModel.SelectedCity.LocalizedNames.Ru,
					Service = SelectedService.Uuid,
					Description = Description.Trim(),
					ImageSource = ImageName,
					Name = Name.Trim(),
					ShareTime = ShareTime,
					IsSubscriberOnly = IsSubscriberOnly
				}, _imageBytes);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}

			CanCreateShareCommand = !res;
			if (res)
			{
				var vmRes = await _navigationService.Navigate<SuccessCreateSharesPopupViewModel, object, bool>(null);
				if (vmRes)
				{
					await _navigationService.Close(this);
				}
				return;
			}

			Device.BeginInvokeOnMainThread(() =>
			{
				Application.Current.MainPage.DisplayAlert("Внимание", "Ошибка попробуйте повторить запрос позже.", "Ок");
			});
		}

		private bool CheckValidFields()
		{
			CanCreateShareCommand = false;
			bool result = true;
			if (PicCountryAndCityViewModel.SelectedCountry == null)
			{
				Device.BeginInvokeOnMainThread(() =>
				{
					Application.Current.MainPage.DisplayAlert("Внимание", "Выберите страну.", "Ок");
				});
				result = false;
			}

			if (PicCountryAndCityViewModel.SelectedCity == null && result)
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

			if (string.IsNullOrEmpty(ImageSource) && result)
			{
				Device.BeginInvokeOnMainThread(() =>
				{
					Application.Current.MainPage.DisplayAlert("Внимание", "Выберите изображение акции.", "Ок");
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

		public bool CanCreateShareCommand
		{
			get => _canCreateShareCommand;
			set => SetProperty(ref _canCreateShareCommand, value);
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

		public MvxCommand ShowShareCommand
		{
			get
			{
				_showShareCommand = _showShareCommand ??
									new MvxCommand(() =>
									{
										_navigationService.Navigate<SharePopupViewModel, Stock>(new Stock
										{
											ImageSource = ImageSource,
											Description = Description,
											Name = Name,
											ShareTime = ShareTime
										});
									});
				return _showShareCommand;
			}
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

				value.Color = Color.FromHex("#BB8D91");
				SetProperty(ref _selectedService, value);
			}
		}

		public PicCountryAndCityViewModel PicCountryAndCityViewModel
		{
			get;
		}
		#endregion

		#region Overrided
		public override async Task Initialize()
		{
			await base.Initialize();

			try
			{
				var a = await _servicesServices.GetAll();
				var typesVm = _mapper.Map<ServiceTypeViewModel[]>(a);
				Services = new MvxObservableCollection<ServiceTypeViewModel>(typesVm);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}
		#endregion

		#region Private
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
				ImageSource = image.Path;

				using (var memoryStream = new MemoryStream())
				{
					image.GetStream()
								  .CopyTo(memoryStream);
					image.Dispose();
					_imageBytes = memoryStream.ToArray();
				}
			}
		}
		#endregion
	}
}
