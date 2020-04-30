using System;
using System.Linq;
using System.Threading.Tasks;
using bonus.app.Core.Dtos.BusinessmanDtos;
using bonus.app.Core.Services;
using bonus.app.Core.Validations;
using bonus.app.Core.ViewModels.Auth;
using bonus.app.Core.ViewModels.Businessman.Services;
using MvvmCross;
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
		private ValidatableObject<string> _email = new ValidatableObject<string>();
		private ValidatableObject<string> _name = new ValidatableObject<string>();
		private ValidatableObject<string> _vkLink = new ValidatableObject<string>();
		private ValidatableObject<string> _instagrammLink = new ValidatableObject<string>();
		private ValidatableObject<string> _facebookLink = new ValidatableObject<string>();
		private ValidatableObject<string> _classmatesLink = new ValidatableObject<string>();
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

		public ValidatableObject<string> VkLink
		{
			get => _vkLink;
			set => SetProperty(ref _vkLink, value);
		}

		public ValidatableObject<string> InstagramLink
		{
			get => _instagrammLink;
			set => SetProperty(ref _instagrammLink, value);
		}

		public ValidatableObject<string> FacebookLink
		{
			get => _facebookLink;
			set => SetProperty(ref _facebookLink, value);
		}

		public ValidatableObject<string> ClassmatesLink
		{
			get => _classmatesLink;
			set => SetProperty(ref _classmatesLink, value);
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

		public CreateServiceViewModel CreateServiceViewModel
		{
			get;
			private set;
		}
		#endregion

		#region Overrided
		public override async Task Initialize()
		{
			await base.Initialize();

			if (Parameters.IsActiveUser && User != null)
			{
				await CreateServiceViewModel.Initialize();
				WorkingMode.Value = User.WorkTime;
				Contact.Value = User.Contact;
				Description = User.Description;
				Email.Value = User.Email;
				Name.Value = User.Name;
			}
		}
		#endregion

		public override void Prepare(EditProfileViewModelArguments parameter)
		{
			base.Prepare(parameter);
			if (Parameters.IsActiveUser)
			{
				CreateServiceViewModel = new CreateServiceViewModel(Mvx.IoCProvider.Resolve<IServicesService>(), AuthService);
			
				Email.Validations.Add(new IsNotNullOrEmptyRule
				{
					ValidationMessage = "Укажите Email адрес."
				});
				Email.Validations.Add(new IsValidEmailRule
				{
					ValidationMessage = "Не корректно введен Email."
				});
				Name.Validations.Add(new IsNotNullOrEmptyRule
				{
					ValidationMessage = "Укажите торговое название или имя мастера."
				});
				Name.Validations.Add(new MinLengthRule(2)
				{
					ValidationMessage = "Торговое название или имя мастера не может быть меньше 2 символов."
				});
				VkLink.Validations.Add(new IsValidUriRule
				{
					ValidationMessage = "Не корректная ссылка."
				});
				InstagramLink.Validations.Add(new IsValidUriRule
				{
					ValidationMessage = "Не корректная ссылка."
				});
				FacebookLink.Validations.Add(new IsValidUriRule
				{
					ValidationMessage = "Не корректная ссылка."
				});
				ClassmatesLink
					.Validations.Add(new IsValidUriRule
				{
					ValidationMessage = "Не корректная ссылка."
				});
			}
		}

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

		public ValidatableObject<string> Name
		{
			get => _name;
			set => SetProperty(ref _name, value);
		}

		public ValidatableObject<string> Email
		{
			get => _email;
			set => SetProperty(ref _email, value);
		}

		private async void EditCommandExecute()
		{
			if (!Validate())
			{
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
					Description = Description,
					Phone = PhoneNumber.Value,
					Password = Parameters.Password
				};

				if (User != null && Parameters.IsActiveUser)
				{
					arg.Phone = !PhoneNumber.Value.Equals(User.Phone) ? PhoneNumber.Value : string.Empty;

					if (!PhoneNumber.Value.Equals(User.Email))
					{
						arg.Email = Email.Value;
					}
				}

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

		private bool Validate()
		{
			if (!WorkingMode.Validate() | !Contact.Validate() | !Address.Validate() | !PhoneNumber.Validate())
			{
				return false;
			}

			if (CountryAndCityViewModel.SelectedCountry == null)
			{
				Device.BeginInvokeOnMainThread(() =>
				{
					Application.Current.MainPage.DisplayAlert("Внимание", "Выберите страну.", "Ок");
				});
				return false;
			}

			if (CountryAndCityViewModel.SelectedCity == null)
			{
				Device.BeginInvokeOnMainThread(() =>
				{
					Application.Current.MainPage.DisplayAlert("Внимание", "Выберите город.", "Ок");
				});
				return false;
			}

			if (Parameters.IsActiveUser)
			{
				if (!string.IsNullOrEmpty(VkLink.Value) & !VkLink.Validate())
				{
					return false;
				}
				if (!string.IsNullOrEmpty(InstagramLink.Value) & !InstagramLink.Validate())
				{
					return false;
				}
				if (!string.IsNullOrEmpty(FacebookLink.Value) & !FacebookLink.Validate())
				{
					return false;
				}
				if (!string.IsNullOrEmpty(ClassmatesLink.Value) & !ClassmatesLink.Validate())
				{
					return false;
				}
			}

			if (Parameters.IsActiveUser & !Email.Validate() & !Name.Validate())
			{
				return false;
			}

			return true;
		}
		#endregion
	}
}
