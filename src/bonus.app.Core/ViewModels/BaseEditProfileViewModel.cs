using System;
using System.Threading.Tasks;
using bonus.app.Core.Models;
using bonus.app.Core.Services;
using bonus.app.Core.Validations;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.Permissions.Abstractions;

namespace bonus.app.Core.ViewModels
{
	public abstract class BaseEditProfileViewModel : MvxViewModel<EditProfileViewModelArguments, User>
	{
		#region Data
		#region Fields
		private ValidatableObject<string> _address = new ValidatableObject<string>();

		private string _imageName;
		private string _imageSource;
		private bool _isAuthorization;
		private bool _isBusy;

		private ValidatableObject<string> _phoneNumber = new ValidatableObject<string>();
		private MvxCommand _picPhotoCommand;
		private string _title;
		private User _user;
		#endregion
		#endregion

		#region .ctor
		public BaseEditProfileViewModel(IAuthService authService, IGeoHelperService geoHelperService, IPermissionsService permissionsService)
		{
			AuthService = authService;
			PermissionsService = permissionsService;

			CountryAndCityViewModel = new PicCountryAndCityViewModel(geoHelperService, authService);
		}
		#endregion

		#region Properties
		public PicCountryAndCityViewModel CountryAndCityViewModel
		{
			get;
		}

		public EditProfileViewModelArguments Parameters
		{
			get;
			private set;
		}

		protected IAuthService AuthService
		{
			get;
		}

		protected IPermissionsService PermissionsService
		{
			get;
		}

		public ValidatableObject<string> Address
		{
			get => _address;
			set => SetProperty(ref _address, value);
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

		public bool IsShowDefaultImage => string.IsNullOrEmpty(ImageSource);

		public ValidatableObject<string> PhoneNumber
		{
			get => _phoneNumber;
			set => SetProperty(ref _phoneNumber, value);
		}

		public MvxCommand PicImageCommand
		{
			get
			{
				_picPhotoCommand = _picPhotoCommand ?? new MvxCommand(PicImageCommandExecute);
				return _picPhotoCommand;
			}
		}

		public string Title
		{
			get => _title;
			private set => SetProperty(ref _title, value);
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
			await CountryAndCityViewModel.Initialize();

			User = AuthService.User;
			IsAuthorization = User != null;
			if (Parameters.IsActiveUser & (User != null))
			{
				Address.Value = User.Address;
				var p = string.Empty;

				p = User.Phone.Substring(0, 2) +
					" (" +
					User.Phone.Substring(2, 3) +
					") " +
					User.Phone.Substring(5, 3) +
					"-" +
					User.Phone.Substring(8, 2) +
					"-" +
					User.Phone.Substring(10, 2);
				PhoneNumber.Value = p;
				ImageSource = User.PhotoSource;
				if (!string.IsNullOrWhiteSpace(ImageSource))
				{
					ImageName = ImageSource.Substring(ImageSource.LastIndexOf('/') + 1);
				}
			}
		}

		public override void Prepare(EditProfileViewModelArguments parameter)
		{
			Parameters = parameter;
			Title = parameter.IsActiveUser ? "Редактор профиля" : "Заполните данные";
		}
		#endregion

		#region Private
		private async void PicImageCommandExecute()
		{
			if (await PermissionsService.CheckPermission(Permission.Storage, "Для загрузки аватара необходимо разрешение на использование хранилища."))
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
			}
		}
		#endregion
	}

	public class EditProfileViewModelArguments
	{
		#region .ctor
		public EditProfileViewModelArguments(Guid guid, bool isActiveUser, string password = null)
		{
			Guid = guid;
			Password = password;
			IsActiveUser = isActiveUser;
		}
		#endregion

		#region Properties
		public Guid Guid
		{
			get;
		}

		public bool IsActiveUser
		{
			get;
		}

		public string Password
		{
			get;
		}
		#endregion
	}
}
