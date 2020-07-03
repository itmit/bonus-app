using System;
using bonus.app.Core.Models.UserModels;
using bonus.app.Core.Services;
using bonus.app.Core.Validations;
using MvvmCross.Commands;
using MvvmCross.Forms.Presenters;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Xamarin.Forms;

namespace bonus.app.Core.ViewModels.Businessman.Managers
{
	public class CreateManagerViewModel : MvxViewModelResult<bool>
	{
		public CreateManagerViewModel(IMvxNavigationService navigationService, IManagerService managerService, IMvxFormsViewPresenter platformPresenter)
		{
			_navigationService = navigationService;
			_managerService = managerService;
			_platformPresenter = platformPresenter;
			AddValidations();
		}
		
		private void AddValidations()
		{
			Email.Validations.Add(new IsNotNullOrEmptyRule
			{
				ValidationMessage = "Укажите Email адрес."
			});
			Email.Validations.Add(new IsValidEmailRule
			{
				ValidationMessage = "Не корректно введен Email."
			});
			Password.Validations.Add(new IsNotNullOrEmptyRule
			{
				ValidationMessage = "Укажите пароль."
			});
			Password.Validations.Add(new MinLengthRule(6)
			{
				ValidationMessage = "Пароль не может быть меньше 6 символов."
			});
			ConfirmPassword.Validations.Add(new IsNotNullOrEmptyRule
			{
				ValidationMessage = "Подтвердите пароль."
			});
			ConfirmPassword.Validations.Add(new IsValidConfirmPassword(() => Password.Value)
			{
				ValidationMessage = "Пароли не совпадают."
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

		public ValidatableObject<string> PhoneNumber
		{
			get => _phoneNumber;
			set => SetProperty(ref _phoneNumber, value);
		}

		private ValidatableObject<string> _confirmPassword = new ValidatableObject<string>();
		private ValidatableObject<string> _email = new ValidatableObject<string>();
		private ValidatableObject<string> _name = new ValidatableObject<string>();
		private ValidatableObject<string> _password = new ValidatableObject<string>();
		private MvxCommand _createManagerCommand;
		private readonly IManagerService _managerService;
		private readonly IMvxNavigationService _navigationService;
		private ValidatableObject<string> _phoneNumber = new ValidatableObject<string>();

		public ValidatableObject<string> ConfirmPassword
		{
			get => _confirmPassword;
			set => SetProperty(ref _confirmPassword, value);
		}

		public ValidatableObject<string> Email
		{
			get => _email;
			set => SetProperty(ref _email, value);
		}

		public ValidatableObject<string> Name
		{
			get => _name;
			set => SetProperty(ref _name, value);
		}

		public ValidatableObject<string> Password
		{
			get => _password;
			set => SetProperty(ref _password, value);
		}

		public MvxCommand CreateManagerCommand
		{
			get
			{
				_createManagerCommand = _createManagerCommand ?? new MvxCommand(CreateManagerCommandExecute);
				return _createManagerCommand;
			}
		}

		private readonly IMvxFormsViewPresenter _platformPresenter;

		private Application _formsApplication;
		private Application FormsApplication => _formsApplication ?? (_formsApplication = _platformPresenter.FormsApplication);

		private bool IsValidFields => Name.Validate() & Email.Validate() & Password.Validate() & ConfirmPassword.Validate() & PhoneNumber.Validate();

		private async void CreateManagerCommandExecute()
		{
			if (!IsValidFields)
			{
				return;
			}

			try
			{
				var uuid = await _managerService.StoreManager(new User
				{
					Email = Email.Value,
					Name = Name.Value,
					Role = UserRole.Manager,
					Phone = PhoneNumber.Value
				}, Password.Value, ConfirmPassword.Value);

				if (uuid.Equals(Guid.Empty))
				{

					if (_managerService.LastError.Equals("The email has already been taken."))
					{
						await FormsApplication.MainPage.DisplayAlert("Внимание", "Пользователь с такой почтой уже зарегистрирован", "Ок");
					}
					else
					{
						await FormsApplication.MainPage.DisplayAlert("Внимание", _managerService.LastError, "Ок");
					}
					return;
				}
				await _navigationService.Close(this, true);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}
	}
}
