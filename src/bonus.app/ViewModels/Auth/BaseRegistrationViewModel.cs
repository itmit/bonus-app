using bonus.app.Core.Dto;
using MvvmCross.Commands;
using MvvmCross.ViewModels;

namespace bonus.app.Core.ViewModels.Auth
{
	public abstract class BaseRegistrationViewModel : MvxViewModel
	{
		private string _login;
		private string _password;
		private string _email;
		private string _masterName;
		private string _confirmPassword;
		private string _pinCode;
		private RegisterErrorDto _errors = new RegisterErrorDto();
		private IMvxCommand _registrationCommand;

		public RegisterErrorDto Errors
		{
			get => _errors;
			set => SetProperty(ref _errors, value);
		}
		public IMvxCommand RegistrationCommand
		{
			get
			{
				_registrationCommand = _registrationCommand ?? new MvxCommand(() =>
				{
					CheckValidFields();
					RegistrationCommandExecute();
				});
				return _registrationCommand;
			}
		}

		protected void CheckValidFields()
		{
			var needRaiseErrorsPropertyChanged = false;
			var login = Login?.Trim();
			if (string.IsNullOrEmpty(login))
			{
				Errors.Login = "Поле логин не может быть пустым.";
				needRaiseErrorsPropertyChanged = true;
			}

			var masterName = MasterName?.Trim();
			if (string.IsNullOrEmpty(masterName))
			{
				Errors.MasterName = "Поле торговое название или имя мастера не может быть пустым.";
				needRaiseErrorsPropertyChanged = true;
			}

			var email = Email?.Trim();
			if (string.IsNullOrEmpty(email))
			{
				Errors.Email = "Поле email не может быть пустым.";
				needRaiseErrorsPropertyChanged = true;
			}
			if (IsValidEmail(email))
			{
				Errors.Email = "Не правильный Email.";
				needRaiseErrorsPropertyChanged = true;
			}

			var password = Password?.Trim();
			if (string.IsNullOrEmpty(password))
			{
				Errors.Password = "Поле пароль не может быть пустым.";
				needRaiseErrorsPropertyChanged = true;
			}

			var confirmPassword = ConfirmPassword?.Trim();
			if (string.IsNullOrEmpty(confirmPassword))
			{
				Errors.Password = "Поле пароль не может быть пустым.";
				needRaiseErrorsPropertyChanged = true;
			}

			if (needRaiseErrorsPropertyChanged)
			{
				RaisePropertyChanged(nameof(Errors));
			}
		}

		private bool IsValidEmail(string email)
		{
			try
			{
				var address = new System.Net.Mail.MailAddress(email);
				return address.Address == email;
			}
			catch
			{
				return false;
			}
		}

		protected abstract void RegistrationCommandExecute();

		public string Login
		{
			get => _login;
			set => SetProperty(ref _login, value);
		}

		public string Password
		{
			get => _password;
			set => SetProperty(ref _password, value);
		}

		public string Email
		{
			get => _email;
			set => SetProperty(ref _email, value);
		}

		public string MasterName
		{
			get => _masterName;
			set => SetProperty(ref _masterName, value);
		}

		public string ConfirmPassword
		{
			get => _confirmPassword;
			set => SetProperty(ref _confirmPassword, value);
		}

		public string PinCode
		{
			get => _pinCode;
			set => SetProperty(ref _pinCode, value);
		}

	}
}
