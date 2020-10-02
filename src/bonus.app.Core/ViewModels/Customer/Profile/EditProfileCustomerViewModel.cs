using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using bonus.app.Core.Dtos;
using bonus.app.Core.Dtos.CustomerDtos;
using bonus.app.Core.Services;
using bonus.app.Core.Services.Interfaces;
using bonus.app.Core.Validations;
using bonus.app.Core.ViewModels.Auth;
using Microsoft.AppCenter.Crashes;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using Xamarin.Essentials;
using Xamarin.Forms;
using XF.Material.Forms.UI.Dialogs;

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

		public override async Task Initialize()
		{
			await base.Initialize();

			if (Parameters.IsActiveUser && User != null)
			{
				Car = User.Car;
				Birthday.Value = User.Birthday;
			}
		}

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
			PhoneNumber.Validations.Add(new IsNotNullOrEmptyRule
			{
				ValidationMessage = "Укажите номер телефона."
			});
			PhoneNumber.Validations.Add(new IsValidPhoneNumberRule
			{
				ValidationMessage = "Не корректный номер телефона."
			});
		}

		private bool Validate()
		{
			if (!Birthday.Validate() | !PhoneNumber.Validate())
			{
				return false;
			}

			if (CountryAndCityViewModel.SelectedCountry == null)
			{
				MaterialDialog.Instance.AlertAsync("Выберите страну", "Внимание", "Ок");
				return false;
			}

			if (CountryAndCityViewModel.SelectedCity != null)
			{
				return true;
			}

			MaterialDialog.Instance.AlertAsync("Выберите город", "Внимание", "Ок");
			return false;

		}

		private async void EditCommandExecute()
		{
			if (!Validate() || Birthday.Value == null)
			{
				return;
			}

			if (IsBusy)
			{
				return;
			}
			IsBusy = true;

			var loadingDialog = await MaterialDialog.Instance.LoadingDialogAsync(message: "Сохранение данных...");

			var arg = new EditCustomerDto
			{
				Uuid = Parameters.Guid,
				Country = CountryAndCityViewModel.SelectedCountry.LocalizedNames.Ru,
				City = CountryAndCityViewModel.SelectedCity.LocalizedNames.Ru,
				Phone = PhoneNumber.Value,
				Birthday = Birthday.Value.Value,
				Car = Car,
				Password = Parameters.Password
			};

			if (User != null && Parameters.IsActiveUser)
			{
				var p = Regex.Replace(PhoneNumber.Value, "[@,\\ \\(\\)\\-]", string.Empty);
				if (User.Phone.Equals(p))
				{
					arg.Phone = string.Empty;
				}
			}

			if (IsFemale)
			{
				arg.Sex = "female";
			}
			else if (IsMale)
			{
				arg.Sex = "male";
			}

			try
			{
				var user = await _customerProfileService.Edit(arg, ImageSource);

				if (user != null)
				{
					if (Parameters.IsActiveUser)
					{
						await loadingDialog.DismissAsync();
						await MaterialDialog.Instance.AlertAsync("Изменения сохранены успешно", "Внимание", "Ок");
						await _navigationService.Close(this, user);
						return;
					}

					await AuthService.Login(new AuthDto
					{
						Login = user.Email,
						Password = Parameters.Password
					});

					if (AuthService.UserIsAuthorized)
					{
						await loadingDialog.DismissAsync();
						await _navigationService.Navigate<SuccessRegisterCustomerPopupViewModel>();
						await _navigationService.Navigate<MainCustomerViewModel>();
						return;
					}
				}
			}
			catch (Exception e)
			{
				Crashes.TrackError(e, new Dictionary<string, string>());
				Console.WriteLine(e);
			}

			IsBusy = false;
			await loadingDialog.DismissAsync();
			if (_customerProfileService.ErrorDetails != null && _customerProfileService.ErrorDetails.Count > 0)
			{
				var key = _customerProfileService.ErrorDetails.First()
												 .Key;
				if (key.Equals("phone"))
				{
					await MaterialDialog.Instance.AlertAsync("Пользователь с таким номером уже существует", "Внимание", "Ок");
					return;
				}
			}

			if (!string.IsNullOrEmpty(_customerProfileService.Error))
			{
				await MaterialDialog.Instance.AlertAsync(_customerProfileService.Error, "Внимание", "Ок");
			}
		}
		#endregion
	}
}
