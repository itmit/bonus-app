using System;
using System.Linq;
using System.Threading.Tasks;
using bonus.app.Core.Dtos.BusinessmanDtos;
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
		private string _description = string.Empty;
		private MvxCommand _editCommand;
		private readonly IMvxNavigationService _navigationService;
		private readonly IProfileService _profileService;
		private ValidatableObject<string> _workingMode = new ValidatableObject<string>();
		#endregion
		#endregion

		#region .ctor
		public EditProfileBusinessmanViewModel(IAuthService authService,
											   IMvxNavigationService navigationService,
											   IGeoHelperService geoHelperService,
											   IProfileService profileService,
											   IPermissionsService permissionsService)
			: base(authService, geoHelperService, permissionsService)
		{
			_navigationService = navigationService;
			_profileService = profileService;

			AddValidations();
		}
		#endregion

		#region Properties
		public ValidatableObject<string> Contact
		{
			get => _contact;
			set => SetProperty(ref _contact, value);
		}

		public string Description
		{
			get => _description;
			set => SetProperty(ref _description, value);
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

		#region Overrided
		public override async Task Initialize()
		{
			await base.Initialize();

			if (Parameters.IsActiveUser)
			{
				WorkingMode.Value = User?.WorkTime;
				Contact.Value = User?.Contact;
				Description = User?.Description;
			}
		}
		#endregion

		#region Private
		private void AddValidations()
		{
			WorkingMode.Validations.Add(new IsNotNullOrEmptyRule
			{
				ValidationMessage = "Укажите режим работы."
			});
			Contact.Validations.Add(new IsNotNullOrEmptyRule
			{
				ValidationMessage = "Укажите контактное лицо."
			});
			Address.Validations.Add(new IsNotNullOrEmptyRule
			{
				ValidationMessage = "Укажите адрес."
			});
			Address.Validations.Add(new MinLengthRule(6)
			{
				ValidationMessage = "Адрес не может быть меньше 6 символов."
			});
			PhoneNumber.Validations.Add(new IsNotNullOrEmptyRule
			{
				ValidationMessage = "Укажите номер телефона."
			});
			PhoneNumber.Validations.Add(new IsValidPhoneNumberRule
			{
				ValidationMessage = "Не корректный номер телефона."
			});
		}

		private async void EditCommandExecute()
		{
			if (!WorkingMode.Validate() | !Contact.Validate() | !Address.Validate() | !PhoneNumber.Validate())
			{
				return;
			}

			if (CountryAndCityViewModel.SelectedCountry == null)
			{
				Device.BeginInvokeOnMainThread(() =>
				{
					Application.Current.MainPage.DisplayAlert("Внимание", "Выберите страну.", "Ок");
				});
				return;
			}

			if (CountryAndCityViewModel.SelectedCity == null)
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
					Uuid = Parameters.Guid,
					Country = CountryAndCityViewModel.SelectedCountry.LocalizedNames.Ru,
					City = CountryAndCityViewModel.SelectedCity.LocalizedNames.Ru,
					Address = Address.Value,
					WorkTime = WorkingMode.Value,
					Contact = Contact.Value,
					Phone = PhoneNumber.Value,
					Description = Description,
					Password = Parameters.Password
				};

				var user = await _profileService.Edit(arg, ImageSource);

				if (Parameters.IsActiveUser)
				{
					await _navigationService.Close(this, user);
					return;
				}

				if (user?.AccessToken != null && !string.IsNullOrEmpty(user.AccessToken.Body))
				{
					await _navigationService.Navigate<SuccessRegisterBusinessmanPopupViewModel>();
					await _navigationService.Navigate<MainBusinessmanViewModel>();
					return;
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}

			if (_profileService.ErrorDetails != null && _profileService.ErrorDetails.Count > 0)
			{
				var key = _profileService.ErrorDetails.First()
										 .Key;
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
		#endregion
	}
}
