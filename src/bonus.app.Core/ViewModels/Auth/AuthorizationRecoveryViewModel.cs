using System.Collections.Generic;
using bonus.app.Core.Services;
using bonus.app.Core.Services.Interfaces;
using bonus.app.Core.Validations;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using XF.Material.Forms.UI.Dialogs;

namespace bonus.app.Core.ViewModels.Auth
{
	public class AuthorizationRecoveryViewModel : MvxViewModel
	{
		#region Data
		#region Fields
		private readonly IMvxNavigationService _navigationService;
		private MvxCommand _sendCodeCommand;
		private readonly IAuthService _authService;
		private ValidatableObject<string> _email = new ValidatableObject<string>();
		private MvxCommand _createAccountCommand;
		#endregion
		#endregion

		#region .ctor
		public AuthorizationRecoveryViewModel(IMvxNavigationService navigationService, IAuthService authService)
		{
			_authService = authService;
			_navigationService = navigationService;
			Email.Validations.Add(new IsNotNullOrEmptyRule
			{
				ValidationMessage = "Укажите Email адрес."
			});
			Email.Validations.Add(new IsValidEmailRule
			{
				ValidationMessage = "Не корректно введен Email."
			});
		}
		#endregion


		public ValidatableObject<string> Email
		{
			get => _email;
			set => SetProperty(ref _email, value);
		}

		/// <summary>
		/// Возвращает команду для создания аккаунта.
		/// </summary>
		public IMvxCommand CreateAccountCommand
		{
			get
			{
				_createAccountCommand = _createAccountCommand ??
										new MvxCommand(() =>
										{
											_navigationService.Navigate<BusinessmanAndCustomerViewModel>();
										});
				return _createAccountCommand;
			}
		}

		/// <summary>
		/// Показывает ошибку при авторизации.
		/// </summary>
		private async void ShowErrors()
		{
			if (_authService.ErrorDetails == null)
			{
				await MaterialDialog.Instance.AlertAsync("Ошибка сервера", "Внимание", "Ок");
				return;
			}

			var dictionary = new Dictionary<string, string>();
			foreach (var detail in _authService.ErrorDetails)
			{
				dictionary[detail.Key] = string.Join("&#10;", detail.Value);
			}

			if (!dictionary.ContainsKey("email"))
			{
				await MaterialDialog.Instance.AlertAsync(_authService.Error, "Внимание", "Ок");
				return;
			}

			var error = dictionary["email"];

			if (string.IsNullOrEmpty(error))
			{
				return;
			}

			if (error.Equals("The selected email is invalid."))
			{
				await MaterialDialog.Instance.AlertAsync("Пользователь не найден.", "Внимание", "Ок");
				return;
			}

			if (error.Equals("The email must be a valid email address."))
			{
				await MaterialDialog.Instance.AlertAsync("Не корректно введен Email.",  "Внимание", "Ок");
			}
		}

		public MvxCommand SendCodeCommand
		{
			get
			{
				_sendCodeCommand = _sendCodeCommand ?? new MvxCommand(async () =>
				{
					if (!Email.Validate())
					{
						return;
					}

					if (await _authService.SendRecoveryCode(Email.Value))
					{
						await _navigationService.Navigate<RecoveryDetailViewModel, RecoveryDetailViewModelArgs>(new RecoveryDetailViewModelArgs(Email.Value, this));
					}
					else
					{
						ShowErrors();
					}
				});
				return _sendCodeCommand;
			}
		}
	}
}
