using System;
using bonus.app.Core.Services;
using bonus.app.Core.Validations;
using MvvmCross.Commands;
using MvvmCross.Forms.Presenters;
using MvvmCross.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace bonus.app.Core.ViewModels.Auth
{
	public class RecoveryDetailViewModel : MvxViewModel<string>
	{
		private IAuthService _authService;
		private MvxCommand _sendCodeCommand;
		private string _email;
		/// <summary>
		/// Обрабатывает логику для общей навигации, связанной с формами.
		/// </summary>
		private readonly IMvxFormsViewPresenter _platformPresenter;

		/// <summary>
		/// Текущее приложение xamarin forms.
		/// </summary>
		private Application _formsApplication;
		private ValidatableObject<string> _confirmPassword = new ValidatableObject<string>();
		private ValidatableObject<string> _password = new ValidatableObject<string>();
		private MvxCommand _recoverCommand;
		private ValidatableObject<string> _code = new ValidatableObject<string>();

		/// <summary>
		/// Возвращает текущее приложение xamarin forms.
		/// </summary>
		private Application FormsApplication => _formsApplication ?? (_formsApplication = _platformPresenter.FormsApplication);

		public RecoveryDetailViewModel(IAuthService authService, IMvxFormsViewPresenter platformPresenter)
		{
			_authService = authService;
			_platformPresenter = platformPresenter;
			AddValidations();
		}

		protected bool IsValidFields => Password.Validate() & ConfirmPassword.Validate() & Code.Validate();


		public MvxCommand SendCodeCommand
		{
			get
			{
				_sendCodeCommand = _sendCodeCommand ?? new MvxCommand(async () =>
				{
					if (await _authService.SendRecoveryCode(_email))
					{
						await FormsApplication.MainPage.DisplayAlert("Внимание", "Код для восстановления пароля отправлен на указанный адрес, повторно", "Ок");
					}
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
						res = await _authService.Recovery(_email, Code.Value, Password.Value, ConfirmPassword.Value);
					}
					catch (Exception e)
					{
						Console.WriteLine(e);
					}

					if (!res)
					{
						return;
					}

					await FormsApplication.MainPage.Navigation.PopToRootAsync();

					Device.BeginInvokeOnMainThread(() =>
					{
						Application.Current.MainPage.DisplayAlert("Внимание", "Пароль сброшен", "Ок");
					});
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

		public override void Prepare(string parameter)
		{
			_email = parameter;
		}
	}
}
