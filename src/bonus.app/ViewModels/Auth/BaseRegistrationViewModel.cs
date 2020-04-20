using System.Collections.Generic;
using System.Net.Mail;
using System.Threading.Tasks;
using bonus.app.Core.Validations;
using MvvmCross.Commands;
using MvvmCross.ViewModels;

namespace bonus.app.Core.ViewModels.Auth
{
	public abstract class BaseRegistrationViewModel : MvxViewModel
	{
		public BaseRegistrationViewModel()
		{
			AddValidations();
		}

		#region Data
		#region Fields
		private ValidatableObject<string> _confirmPassword = new ValidatableObject<string>();
		private ValidatableObject<string> _email = new ValidatableObject<string>();
		private ValidatableObject<string> _login = new ValidatableObject<string>();
		private ValidatableObject<string> _name = new ValidatableObject<string>();
		private ValidatableObject<string> _password = new ValidatableObject<string>();
		private string _pinCode;
		private IMvxCommand _registrationCommand;
		#endregion
		#endregion

		#region Properties
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

		public ValidatableObject<string> Login
		{
			get => _login;
			set
			{
				SetProperty(ref _login, value);
			}
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

		public string PinCode
		{
			get => _pinCode;
			set => SetProperty(ref _pinCode, value);
		}

		public IMvxCommand RegistrationCommand
		{
			get
			{
				_registrationCommand = _registrationCommand ?? new MvxCommand(Execute);
				return _registrationCommand;
			}
		}
		#endregion

		#region Protected
		protected bool CheckValidFields() => Login.Validate() & Name.Validate() & Email.Validate() & Password.Validate() & ConfirmPassword.Validate() & Name.Validate();
		#endregion

		#region Overridable
		protected abstract Task<bool> RegistrationCommandExecute();
		#endregion

		#region Private
		private async void Execute()
		{
			if (CheckValidFields())
			{
				await RegistrationCommandExecute();
			}
		}

		private void AddValidations()
		{
			Email.Validations.Add(new IsNotNullOrEmptyRule { ValidationMessage = "Укажите Email адрес." });
			Email.Validations.Add(new IsValidEmailRule { ValidationMessage = "Не корректно введен Email." });
			Login.Validations.Add(new IsNotNullOrEmptyRule { ValidationMessage = "Укажите логин." });
			Login.Validations.Add(new MinLengthRule(2) { ValidationMessage = "Логин не может быть меньше 2 символов." });
			Password.Validations.Add(new IsNotNullOrEmptyRule { ValidationMessage = "Укажите пароль." });
			Password.Validations.Add(new MinLengthRule(6) { ValidationMessage = "Пароль не может быть меньше 6 символов." });
			ConfirmPassword.Validations.Add(new IsNotNullOrEmptyRule { ValidationMessage = "Подтвердите пароль." });
			ConfirmPassword.Validations.Add(new IsValidConfirmPassword(() => Password.Value) { ValidationMessage = "Пароли не совпадают." });
		}

		#endregion
	}
}
