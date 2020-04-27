using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using bonus.app.Core.Dtos.GeoHelper;
using bonus.app.Core.Models;
using bonus.app.Core.Services;
using bonus.app.Core.Validations;
using MvvmCross.Base;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.Permissions.Abstractions;
using Realms;

namespace bonus.app.Core.ViewModels
{
	public abstract class BaseEditProfileViewModel : MvxViewModel<EditProfileViewModelArguments>
	{
		#region Data
		#region Fields
		private ValidatableObject<string> _address = new ValidatableObject<string>();

		private readonly IAuthService _authService;

		protected byte[] ImageBytes
		{
			get;
			private set;
		}

		private string _imageName;
		private string _imageSource;
		private bool _isAuthorization;
		private bool _isBusy;
		private readonly IPermissionsService _permissionsService;
		private ValidatableObject<string> _phoneNumber = new ValidatableObject<string>();
		private MvxCommand _picPhotoCommand;
		private User _user;

		public PicCountryAndCityViewModel CountryAndCityViewModel
		{
			get;
		}
		#endregion
		#endregion

		#region .ctor
		public BaseEditProfileViewModel(IAuthService authService, IGeoHelperService geoHelperService, IPermissionsService permissionsService)
		{
			_authService = authService;
			_permissionsService = permissionsService;

			CountryAndCityViewModel = new PicCountryAndCityViewModel(geoHelperService, authService);
		}
		#endregion

		#region Properties
		public EditProfileViewModelArguments Parameters
		{
			get;
			private set;
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

			User = _authService.User;
			IsAuthorization = User != null;
			if (Parameters.IsActiveUser)
			{
				Address.Value = User?.Address;
				PhoneNumber.Value = User?.Phone;
				ImageSource = User?.PhotoSource;
				if (!string.IsNullOrWhiteSpace(ImageSource))
				{
					ImageName = ImageSource.Substring(ImageSource.LastIndexOf('/') + 1);
				}
			}
		}

		public override void Prepare(EditProfileViewModelArguments parameter)
		{
			Parameters = parameter;
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
					ImageBytes = memoryStream.ToArray();
				}
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
