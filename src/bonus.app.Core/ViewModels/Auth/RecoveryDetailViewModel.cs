using System;
using bonus.app.Core.Services;
using bonus.app.Core.Services.Interfaces;
using bonus.app.Core.Validations;
using MvvmCross.Commands;
using MvvmCross.Forms.Presenters;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;
using XF.Material.Forms.UI.Dialogs;

namespace bonus.app.Core.ViewModels.Auth
{
	public class RecoveryDetailViewModel : MvxViewModel<RecoveryDetailViewModelArgs>
	{
		private readonly IAuthService _authService;
		private MvxCommand _sendCodeCommand;
		private ValidatableObject<string> _confirmPassword = new ValidatableObject<string>();
		private ValidatableObject<string> _password = new ValidatableObject<string>();
		private MvxCommand _recoverCommand;
		private ValidatableObject<string> _code = new ValidatableObject<string>();
		private readonly IMvxNavigationService _navigationService;

		public RecoveryDetailViewModel(IAuthService authService, IMvxNavigationService navigationService)
		{
			_authService = authService;
			_navigationService = navigationService;
			AddValidations();
		}

		protected bool IsValidFields => Password.Validate() & ConfirmPassword.Validate() & Code.Validate();


		public MvxCommand SendCodeCommand
		{
			get
			{
				_sendCodeCommand = _sendCodeCommand ?? new MvxCommand(async () =>
				{
					await MaterialDialog.Instance.AlertAsync("Код для восстановления пароля отправлен на указанный адрес, повторно", "Внимание", "Ок");
				});
				return _sendCodeCommand;
			}
		}
		private void AddValidations()
		{
			Code.Validations.Add(new IsNotNullOrEmptyRule
			{
				ValidationMessage = "Укажите код для восстановления."
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
		}

		public MvxCommand RecoverCommand
		{
			get
			{
				_recoverCommand = _recoverCommand ?? new MvxCommand(async () =>
				{
					var res = false;

					if (!IsValidFields)
					{
						return;
					}

					try
					{
						res = await _authService.Recovery(Parameter.Email, Code.Value, Password.Value, ConfirmPassword.Value);
					}
					catch (Exception e)
					{
						Console.WriteLine(e);
					}

					if (!res)
					{
						return;
					}

					await _navigationService.Close(Parameter.ParentViewModel);
					await _navigationService.Close(this);
					await MaterialDialog.Instance.AlertAsync("Пароль сброшен", "Внимание", "Ок");
				});
				return _recoverCommand;
			}
		}

		public ValidatableObject<string> Code
		{
			get => _code;
			set => SetProperty(ref _code, value);
		}

		public ValidatableObject<string> Password
		{
			get => _password;
			set => SetProperty(ref _password, value);
		}


		public ValidatableObject<string> ConfirmPassword
		{
			get => _confirmPassword;
			set => SetProperty(ref _confirmPassword, value);
		}

		public override void Prepare(RecoveryDetailViewModelArgs parameter)
		{
			Parameter = parameter;
		}

		public RecoveryDetailViewModelArgs Parameter
		{
			get;
			private set;
		}
	}

	public class RecoveryDetailViewModelArgs
	{
		public string Email
		{
			get;
		}

		public IMvxViewModel ParentViewModel
		{
			get;
		}

		public RecoveryDetailViewModelArgs(string email, IMvxViewModel parentViewModel)
		{
			Email = email;
			ParentViewModel = parentViewModel;
		}
	}
}
