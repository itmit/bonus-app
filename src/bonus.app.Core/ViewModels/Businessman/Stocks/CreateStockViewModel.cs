using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using bonus.app.Core.Models;
using bonus.app.Core.Services;
using bonus.app.Core.Validations;
using bonus.app.Core.ViewModels.Businessman.Popups;
using bonus.app.Core.ViewModels.Businessman.Services;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.Permissions.Abstractions;
using Xamarin.Forms;

namespace bonus.app.Core.ViewModels.Businessman.Stocks
{
	public class CreateStockViewModel : MvxViewModel
	{
		#region Data
		#region Fields
		private bool _canCreateShareCommand = true;
		private MvxCommand _createShareCommand;
		private ValidatableObject<string> _description = new ValidatableObject<string>();
		private byte[] _imageBytes;
		private string _imageName;
		private string _imageSource;
		private bool _isSubscriberOnly;
		private ValidatableObject<string> _name = new ValidatableObject<string>();
		private readonly IMvxNavigationService _navigationService;
		private readonly IPermissionsService _permissionsService;
		private MvxCommand _picPhotoCommand;
		private ValidatableObject<DateTime?> _shareTime = new ValidatableObject<DateTime?>
		{
			Value = DateTime.Now
		};
		private MvxCommand _showShareCommand;
		private readonly IStockService _stockService;
		#endregion
		#endregion

		#region .ctor
		public CreateStockViewModel(IStockService stockService,
									IGeoHelperService geoHelperService,
									IPermissionsService permissionsService,
									IServicesService servicesServices,
									IMvxNavigationService navigationService,
									IAuthService authService)
		{
			_stockService = stockService;
			_permissionsService = permissionsService;
			_navigationService = navigationService;
			PicCountryAndCityViewModel = new PicCountryAndCityViewModel(geoHelperService, authService);
			MyServicesViewModel = new MyServicesViewModel(servicesServices, authService);

			AddValidations();
		}

		private void AddValidations()
		{
			Description.Validations.Add(new IsNotNullOrEmptyRule
			{
				ValidationMessage = "Заполните описание акции."
			});
			Name.Validations.Add(new IsNotNullOrEmptyRule
			{
				ValidationMessage = "Укажите название акции."
			});
			Name.Validations.Add(new MinLengthRule(3)
			{
				ValidationMessage = "Название акции не может содержать меньше 4 символов."
			});
			ShareTime.Validations.Add(new IsValidDateRule(DateTime.Now, new DateTime(2099, 1, 1))
			{
				ValidationMessage = "Срок размещения акции должен быть актуальным."
			});
		}
		#endregion

		#region Properties
		public PicCountryAndCityViewModel PicCountryAndCityViewModel
		{
			get;
		}
		public MyServicesViewModel MyServicesViewModel { get; }
		public bool CanCreateShareCommand
		{
			get => _canCreateShareCommand;
			set => SetProperty(ref _canCreateShareCommand, value);
		}

		public MvxCommand CreateShareCommand
		{
			get
			{
				_createShareCommand = _createShareCommand ?? new MvxCommand(CreateShareCommandExecute);
				return _createShareCommand;
			}
		}

		public ValidatableObject<string> Description
		{
			get => _description;
			set => SetProperty(ref _description, value);
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

		public bool IsShowDefaultImage => string.IsNullOrEmpty(ImageSource);

		public bool IsSubscriberOnly
		{
			get => _isSubscriberOnly;
			set => SetProperty(ref _isSubscriberOnly, value);
		}

		public ValidatableObject<string> Name
		{
			get => _name;
			set => SetProperty(ref _name, value);
		}

		public MvxCommand PicImageCommand
		{
			get
			{
				_picPhotoCommand = _picPhotoCommand ?? new MvxCommand(PicImageCommandExecute);
				return _picPhotoCommand;
			}
		}


		public ValidatableObject<DateTime?> ShareTime
		{
			get => _shareTime;
			set => SetProperty(ref _shareTime, value);
		}

		public MvxCommand ShowShareCommand
		{
			get
			{
				_showShareCommand = _showShareCommand ??
									new MvxCommand(() =>
									{
										if (!CheckValidFields())
										{
											return;
										}
										if (ShareTime.Value == null)
										{
											ShareTime.Value = DateTime.Now;
										}

										_navigationService.Navigate<SharePopupViewModel, Stock>(new Stock
										{
											ImageSource = ImageSource,
											Description = Description.Value,
											Name = Name.Value,
											ShareTime = ShareTime.Value.Value
										});
										
									});
				return _showShareCommand;
			}
		}
		#endregion

		#region Private
		private bool CheckValidFields()
		{
			CanCreateShareCommand = false;
			var result = true;
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

			if (MyServicesViewModel.SelectedService == null && result)
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

			if (!Name.Validate() | !Description.Validate() | !ShareTime.Validate())
			{
				result = false;
			}
			
			CanCreateShareCommand = !result;
			return result;
		}

		private async void CreateShareCommandExecute()
		{
			if (!CheckValidFields())
			{
				return;
			}

			var res = false;
			try
			{
				if (ShareTime.Value == null)
				{
					ShareTime.Value = DateTime.Now;
				}

				res = await _stockService.CreateStock(new Stock
													  {
														  Country = PicCountryAndCityViewModel.SelectedCountry.LocalizedNames.Ru,
														  City = PicCountryAndCityViewModel.SelectedCity.LocalizedNames.Ru,
														  Service = MyServicesViewModel.SelectedService.Uuid,
														  Description = Description.Value,
														  ImageSource = ImageName,
														  Name = Name.Value,
														  ShareTime = ShareTime.Value.Value,
														  IsSubscriberOnly = IsSubscriberOnly
													  },
													  _imageBytes);
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

		public override async Task Initialize() 
		{
			await base.Initialize();
			await PicCountryAndCityViewModel.Initialize();
			await MyServicesViewModel.Initialize();
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
