using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using bonus.app.Core.Converters;
using bonus.app.Core.Dtos.BusinessmanDtos;
using bonus.app.Core.Models;
using bonus.app.Core.Repositories;
using bonus.app.Core.Services;
using bonus.app.Core.Validations;
using bonus.app.Core.ViewModels.Auth;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using Xamarin.Forms;

namespace bonus.app.Core.ViewModels.Businessman.Profile
{
	public class EditProfileBusinessmanViewModel : BaseEditProfileViewModel
	{
		#region Data
		#region Fields
		private ValidatableObject<string> _contact = new ValidatableObject<string>();
		private MvxCommand _editCommand;
		private readonly IMvxNavigationService _navigationService;
		private readonly IProfileService _profileService;
		private readonly IUserRepository _userRepository;
		private ValidatableObject<string> _workingMode = new ValidatableObject<string>();
		private string _description = string.Empty;
		#endregion
		#endregion

		#region .ctor
		public EditProfileBusinessmanViewModel(IAuthService authService,
											   IMvxNavigationService navigationService,
											   IGeoHelperService geoHelperService,
											   IProfileService profileService,
											   IUserRepository userRepository,
											   IPermissionsService permissionsService)
			: base(authService, geoHelperService, permissionsService)
		{
			_navigationService = navigationService;
			_profileService = profileService;
			_userRepository = userRepository;

			AddValidations();
		}
		#endregion

		#region Properties
		public ValidatableObject<string> Contact
		{
			get => _contact;
			set => SetProperty(ref _contact, value);
		}

		public MvxCommand EditCommand
		{
			get
			{
				_editCommand = _editCommand ?? new MvxCommand(EditCommandExecute);
				return _editCommand;
			}
		}

		public ValidatableObject<string> WorkingMode
		{
			get => _workingMode;
			set => SetProperty(ref _workingMode, value);
		}
		#endregion

		#region Private
		private void AddValidations()
		{
			WorkingMode.Validations.Add(new IsNotNullOrEmptyRule { ValidationMessage = "Укажите режим работы." });
			Contact.Validations.Add(new IsNotNullOrEmptyRule { ValidationMessage = "Укажите контактное лицо." });
			Address.Validations.Add(new IsNotNullOrEmptyRule { ValidationMessage = "Укажите адрес." });
			Address.Validations.Add(new MinLengthRule(6) { ValidationMessage = "Адрес не может быть меньше 6 символов." });
			PhoneNumber.Validations.Add(new IsNotNullOrEmptyRule { ValidationMessage = "Укажите номер телефона." });
			PhoneNumber.Validations.Add(new IsValidPhoneNumberRule { ValidationMessage = "Не корректный номер телефона." });
		}

		private async void EditCommandExecute()
		{
			if (!WorkingMode.Validate() | !Contact.Validate() | !Address.Validate() | !PhoneNumber.Validate())
			{
				return;
			}

			if (SelectedCountry == null)
			{
				Device.BeginInvokeOnMainThread(() =>
				{
					Application.Current.MainPage.DisplayAlert("Внимание", "Выберите страну.", "Ок");
				});
				return;
			}

			if (SelectedCity == null)
			{
				Device.BeginInvokeOnMainThread(() =>
				{
					Application.Current.MainPage.DisplayAlert("Внимание", "Выберите город.", "Ок");
				});
				return;
			}

			try
			{
				var arg = new EditBusinessmanDto
				{
					Uuid = Parameter.Guid,
					Country = SelectedCountry.LocalizedNames.Ru,
					City = SelectedCity.LocalizedNames.Ru,
					Address = Address.Value,
					WorkTime = WorkingMode.Value,
					Contact = Contact.Value,
					Phone = PhoneNumber.Value,
					Description = Description,
					Password = Parameter.Password
				};

				var user = await _profileService.Edit(arg, ImageBytes, ImageName);

				if (user?.AccessToken != null && !string.IsNullOrEmpty(user.AccessToken.Body))
				{
					_userRepository.Add(user);
					await _navigationService.Navigate<SuccessRegisterBusinessmanPopupViewModel>();
					await _navigationService.Navigate<MainBusinessmanViewModel>();
					return;
				}
			}
			catch (Exception e)
			{
				Debug.WriteLine(e);
			}

			if (_profileService.ErrorDetails != null && _profileService.ErrorDetails.Count > 0)
			{
				var key = _profileService.ErrorDetails.First().Key;
				if (key.Equals("phone"))
				{
					Device.BeginInvokeOnMainThread(() =>
					{
						Application.Current.MainPage.DisplayAlert("Ошибка", "Пользователь с таким номером уже существует.", "Ок");
					});
					return;
				}

			}

			if (!string.IsNullOrEmpty(_profileService.Error))
			{
				Device.BeginInvokeOnMainThread(() =>
				{
					Application.Current.MainPage.DisplayAlert("Ошибка", _profileService.Error, "Ок");
				});
			}
		}

		public string Description
		{
			get => _description;
			set => SetProperty(ref _description, value);
		}
		#endregion
	}
}
