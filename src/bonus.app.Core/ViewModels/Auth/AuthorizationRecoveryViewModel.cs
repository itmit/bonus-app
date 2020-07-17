using bonus.app.Core.Services;
using bonus.app.Core.Validations;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

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
						await _navigationService.Navigate<RecoveryDetailViewModel, string>(Email.Value);
					}
				});
				return _sendCodeCommand;
			}
		}
	}
}
