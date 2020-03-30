using System.Collections.Generic;
using System.Net.Mail;
using System.Threading.Tasks;
using MvvmCross.Commands;
using MvvmCross.ViewModels;

namespace bonus.app.Core.ViewModels.Auth
{
	public abstract class BaseRegistrationViewModel : MvxViewModel
	{
		#region Delegates and events
		/// <summary>
		/// Представляет метод при успешной регистрации.
		/// </summary>
		public delegate void AfterRegisterEventHandler();

		/// <summary>
		/// Происходит после успешной регистрации.
		/// </summary>
		public event AfterRegisterEventHandler AfterRegister;
		#endregion

		#region Data
		#region Fields
		private string _confirmPassword;
		private string _email;
		private Dictionary<string, string> _errors = new Dictionary<string, string>();
		private string _login;
		private string _name;
		private string _password;
		private string _pinCode;
		private IMvxCommand _registrationCommand;
		#endregion
		#endregion

		#region Properties
		public string ConfirmPassword
		{
			get => _confirmPassword;
			set => SetProperty(ref _confirmPassword, value);
		}

		public string Email
		{
			get => _email;
			set => SetProperty(ref _email, value);
		}

		public Dictionary<string, string> Errors
		{
			get => _errors;
			protected set => SetProperty(ref _errors, value);
		}

		public string Login
		{
			get => _login;
			set
			{
				SetProperty(ref _login, value);
				if (string.IsNullOrEmpty(value))
				{
					Errors[nameof(Login)
							   .ToLower()] = "Поле логин не может быть пустым.";
				}
				else
				{
					Errors[nameof(Login)
							   .ToLower()] = null;
				}

				RaisePropertyChanged(() => Errors);
			}
		}

		public string Name
		{
			get => _name;
			set => SetProperty(ref _name, value);
		}

		public string Password
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
		protected bool CheckValidFields()
		{
			var needRaiseErrorsPropertyChanged = false;
			var login = Login?.Trim();
			if (string.IsNullOrEmpty(login))
			{
				Errors[nameof(Login)
						   .ToLower()] = "Поле логин не может быть пустым.";
				needRaiseErrorsPropertyChanged = true;
			}

			var name = Name?.Trim();
			if (string.IsNullOrEmpty(name))
			{
				Errors[nameof(Name)
						   .ToLower()] = "Поле торговое название или имя мастера не может быть пустым.";
				needRaiseErrorsPropertyChanged = true;
			}

			var email = Email?.Trim();
			if (string.IsNullOrEmpty(email))
			{
				Errors[nameof(Email)
						   .ToLower()] = "Поле email не может быть пустым.";
				needRaiseErrorsPropertyChanged = true;
			}

			if (!IsValidEmail(email))
			{
				Errors[nameof(Email)
						   .ToLower()] = "Поле email не может быть пустым.";
				needRaiseErrorsPropertyChanged = true;
			}

			var password = Password?.Trim();
			if (string.IsNullOrEmpty(password))
			{
				Errors[nameof(Password)
						   .ToLower()] = "Поле пароль не может быть пустым.";
				needRaiseErrorsPropertyChanged = true;
			}

			var confirmPassword = ConfirmPassword?.Trim();
			if (string.IsNullOrEmpty(confirmPassword))
			{
				Errors[nameof(Password)
						   .ToLower()] = "Поле пароль не может быть пустым.";
				needRaiseErrorsPropertyChanged = true;
			}

			if (needRaiseErrorsPropertyChanged)
			{
				RaisePropertyChanged(() => Errors);
				return false;
			}

			return true;
		}
		#endregion

		#region Overridable
		protected abstract Task<bool> RegistrationCommandExecute();
		#endregion

		#region Private
		private async void Execute()
		{
			if (CheckValidFields() && await RegistrationCommandExecute())
			{
				AfterRegister?.Invoke();
			}
		}

		private bool IsValidEmail(string email)
		{
			try
			{
				var address = new MailAddress(email);
				return address.Address == email;
			}
			catch
			{
				return false;
			}
		}
		#endregion
	}
}
