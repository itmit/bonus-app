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
		private int _pinCode;

		private IMvxCommand _registrationCommand;

		public IMvxCommand RegistrationCommand
		{
			get
			{
				_registrationCommand = _registrationCommand ?? new MvxCommand(RegistrationCommandExecute);
				return _registrationCommand;
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

		public int PinCode
		{
			get => _pinCode;
			set => SetProperty(ref _pinCode, value);
		}

	}
}
