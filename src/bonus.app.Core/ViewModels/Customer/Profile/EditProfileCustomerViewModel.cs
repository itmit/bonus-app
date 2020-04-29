using System;
using System.Linq;
using bonus.app.Core.Dtos.CustomerDtos;
using bonus.app.Core.Services;
using bonus.app.Core.Validations;
using bonus.app.Core.ViewModels.Auth;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using Xamarin.Forms;

namespace bonus.app.Core.ViewModels.Customer.Profile
{
	public class EditProfileCustomerViewModel : BaseEditProfileViewModel
	{
		#region Data
		#region Fields
		private ValidatableObject<DateTime?> _birthday = new ValidatableObject<DateTime?>();
		private string _car = string.Empty;
		private readonly IProfileService _customerProfileService;

		private MvxCommand _editCommand;
		private bool _isFemale;
		private bool _isMale;
		private readonly IMvxNavigationService _navigationService;
		#endregion
		#endregion

		#region .ctor
		public EditProfileCustomerViewModel(IAuthService authService,
											IMvxNavigationService navigationService,
											IGeoHelperService geoHelperService,
											IProfileService customerProfileService,
											IPermissionsService permissionsService)
			: base(authService, geoHelperService, permissionsService)
		{
			_navigationService = navigationService;
			_customerProfileService = customerProfileService;
			IsFemale = true;

			AddValidations();
		}
		#endregion

		#region Properties
		public ValidatableObject<DateTime?> Birthday
		{
			get => _birthday;
			set => SetProperty(ref _birthday, value);
		}

		public string Car
		{
			get => _car;
			set => SetProperty(ref _car, value);
		}

		public MvxCommand EditCommand
		{
			get
			{
				_editCommand = _editCommand ?? new MvxCommand(EditCommandExecute);
				return _editCommand;
			}
		}

		public bool IsFemale
		{
			get => _isFemale;
			set
			{
				_isMale = !value;
				RaisePropertyChanged(() => IsMale);
				SetProperty(ref _isFemale, value);
			}
		}

		public bool IsMale
		{
			get => _isMale;
			set
			{
				_isFemale = !value;
				RaisePropertyChanged(() => IsFemale);
				SetProperty(ref _isMale, value);
			}
		}
		#endregion

		#region Public
		public void OpenAuthorization()
		{
			_navigationService.Navigate<AuthorizationViewModel>();
		}
		#endregion

		#region Private
		private void AddValidations()
		{
			Birthday.Validations.Add(new IsValidDateRule(new DateTime(1900, 1, 1), new DateTime(DateTime.Now.Year - 6, 1, 1))
			{
				ValidationMessage = "Не корректная дата."
			});
			Address.Validations.Add(new IsNotNullOrEmptyRule
			{
				ValidationMessage = "укажите адрес."
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
			if (!Birthday.Validate() | !Address.Validate() | !PhoneNumber.Validate())
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

			if (Birthday.Value == null)
			{
				return;
			}

			try
			{
				var arg = new EditCustomerDto
				{
					Uuid = Parameters.Guid,
					Country = CountryAndCityViewModel.SelectedCountry.LocalizedNames.Ru,
					City = CountryAndCityViewModel.SelectedCity.LocalizedNames.Ru,
					Phone = PhoneNumber.Value,
					Birthday = Birthday.Value.Value,
					Car = Car,
					Address = Address.Value,
					Password = Parameters.Password
				};
				if (IsFemale)
				{
					arg.Sex = "female";
				}
				else if (IsMale)
				{
					arg.Sex = "male";
				}

				var user = await _customerProfileService.Edit(arg, ImageSource);

				if (user?.AccessToken != null && !string.IsNullOrEmpty(user.AccessToken.Body))
				{
					await _navigationService.Navigate<SuccessRegisterCustomerPopupViewModel>();
					await _navigationService.Navigate<MainCustomerViewModel>();
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}

			if (_customerProfileService.ErrorDetails != null && _customerProfileService.ErrorDetails.Count > 0)
			{
				var key = _customerProfileService.ErrorDetails.First()
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

			if (!string.IsNullOrEmpty(_customerProfileService.Error))
			{
				Device.BeginInvokeOnMainThread(() =>
				{
					Application.Current.MainPage.DisplayAlert("Ошибка", _customerProfileService.Error, "Ок");
				});
			}
		}
		#endregion
	}
}
