using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using bonus.app.Core.Models;
using bonus.app.Core.Services;
using bonus.app.Core.Validations;
using bonus.app.Core.ViewModels.Businessman.Services;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Xamarin.Forms;

namespace bonus.app.Core.ViewModels.Businessman.Stocks
{
	public class EditorStockViewModel : MvxViewModel<Stock, Stock>
	{
		#region Data
		#region Fields
		private bool _canCreateShareCommand = true;
		private ValidatableObject<string> _description = new ValidatableObject<string>();
		private byte[] _imageBytes;
		private string _imageSource;
		private ValidatableObject<string> _name = new ValidatableObject<string>();
		private readonly IMvxNavigationService _navigationService;
		private readonly IPermissionsService _permissionsService;
		private MvxCommand _picPhotoCommand;
		private MvxCommand _saveCommand;
		private ValidatableObject<DateTime?> _shareTime = new ValidatableObject<DateTime?>
		{
			Value = DateTime.Today
		};
		private Stock _stock;
		private readonly IStockService _stockService;
		#endregion
		#endregion

		#region .ctor
		public EditorStockViewModel(IGeoHelperService geoHelperService,
									IServicesService servicesServices,
									IStockService stockService,
									IPermissionsService permissionsService,
									IMvxNavigationService navigationService,
									IAuthService authService)
		{
			_navigationService = navigationService;
			_permissionsService = permissionsService;
			_stockService = stockService;

			PicCountryAndCityViewModel = new PicCountryAndCityViewModel(geoHelperService, authService);
			MyServicesViewModel = new MyServicesViewModel(servicesServices, authService);

			AddValidations();
		}
		#endregion

		#region Properties
		public MyServicesViewModel MyServicesViewModel
		{
			get;
			set;
		}

		public PicCountryAndCityViewModel PicCountryAndCityViewModel
		{
			get;
			set;
		}

		public bool CanCreateShareCommand
		{
			get => _canCreateShareCommand;
			set => SetProperty(ref _canCreateShareCommand, value);
		}

		public ValidatableObject<string> Description
		{
			get => _description;
			set => SetProperty(ref _description, value);
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

		public MvxCommand SaveCommand
		{
			get
			{
				_saveCommand = _saveCommand ?? new MvxCommand(SaveCommandExecute);
				return _saveCommand;
			}
		}

		public ValidatableObject<DateTime?> ShareTime
		{
			get => _shareTime;
			set
			{
				SetProperty(ref _shareTime, value);
				if (value.Value != null)
				{
					Stock.ShareTime = value.Value.Value;
				}
			}
		}

		public Stock Stock
		{
			get => _stock;
			private set => SetProperty(ref _stock, value);
		}
		#endregion

		#region Overrided
		public override async Task Initialize()
		{
			await base.Initialize();
			await MyServicesViewModel.Initialize();
			await PicCountryAndCityViewModel.Initialize();

			try
			{
				Stock = await _stockService.GetStockForEdit(Stock.Uuid);

				ImageSource = Stock.ImageSource;

				_name.Value = Stock.Name;
				_description.Value = Stock.Description;
				_shareTime.Value = Stock.ShareTime;
				await RaisePropertyChanged(() => ShareTime);
				await RaisePropertyChanged(() => Name);
				await RaisePropertyChanged(() => Description);

				var temp = MyServicesViewModel.Services.SingleOrDefault(model => model.Services.Any(service => service.Uuid == Stock.Service));
				var ser = temp?.Services.SingleOrDefault(service => service.Uuid == Stock.Service);
				MyServicesViewModel.SelectedService = ser;
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}

		public override void Prepare(Stock parameter)
		{
			Stock = parameter;
		}
		#endregion

		#region Private
		private void AddValidations()
		{
			Name.Validations.Add(new IsNotNullOrEmptyRule
			{
				ValidationMessage = "Укажите название акции."
			});
			Name.Validations.Add(new MinLengthRule(3)
			{
				ValidationMessage = "Название акции не может содержать меньше 4 символов."
			});
			Description.Validations.Add(new IsNotNullOrEmptyRule
			{
				ValidationMessage = "Описание не может быть пустым."
			});
			ShareTime.Validations.Add(new IsValidDateRule(DateTime.Now, new DateTime(2099, 1, 1))
			{
				ValidationMessage = "Срок размещения акции должен быть актуальным."
			});
		}

		private bool IsValid()
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

			if (!Name.Validate() | !Description.Validate() | !ShareTime.Validate())
			{
				result = false;
			}

			CanCreateShareCommand = !result;
			return result;
		}

		private async void PicImageCommandExecute()
		{
			if (!await _permissionsService.RequestPermissionAsync<StoragePermission>(Permission.Storage, "Для загрузки аватара необходимо разрешение на использование хранилища."))
			{
				return;
			}

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

		private async void SaveCommandExecute()
		{
			if (!IsValid())
			{
				return;
			}

			Stock.Country = PicCountryAndCityViewModel.SelectedCountry.LocalizedNames.Ru;
			Stock.City = PicCountryAndCityViewModel.SelectedCity.LocalizedNames.Ru;
			Stock.Service = MyServicesViewModel.SelectedService.Uuid;
			Stock.Name = Name.Value;
			Stock.Description = Description.Value;

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
