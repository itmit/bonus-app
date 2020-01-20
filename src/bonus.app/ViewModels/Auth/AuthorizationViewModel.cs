using System;
using System.Net.Mail;
using System.Windows.Input;
using bonus.app.Core.Dto;
using bonus.app.Core.Models;
using bonus.app.Core.Repositories;
using bonus.app.Core.Services;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Xamarin.Forms;

namespace bonus.app.Core.ViewModels.Auth
{
	/// <summary>
	/// Представляет модель для страницы авторизации.
	/// </summary>
	public class AuthorizationViewModel : MvxNavigationViewModel
	{
		#region Data
		#region Fields
		/// <summary>
		/// Ошибки авторизации.
		/// </summary>
		private AuthErrorDto _errors = new AuthErrorDto();
		
		/// <summary>
		/// Логин пользователя.
		/// </summary>
		private string _login;

		/// <summary>
		/// Команда для авторизации.
		/// </summary>
		private ICommand _loginCommand;
		
		/// <summary>
		/// Пароль пользователя.
		/// </summary>
		private string _password;
		private ICommand _forgotPasswordCommand;
		private MvxCommand _createAccountCommand;
		#endregion
		#endregion

		#region .ctor
		/// <summary>
		/// Инициализирует новый экземпляр <see cref="AuthorizationViewModel" />.
		/// </summary>
		/// <param name="logProvider">Провайдер логов.</param>
		/// <param name="navigationService">Сервис для навигации.</param>
		public AuthorizationViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService)
			: base(logProvider, navigationService)
		{
		}
		#endregion

		#region Properties
		/// <summary>
		/// Возвращает ошибки авторизации.
		/// </summary>
		public AuthErrorDto Errors
		{
			get => _errors;
			private set => SetProperty(ref _errors, value);
		}

		/// <summary>
		/// Возвращает или устанавливает логин пользователя.
		/// </summary>
		public string Login
		{
			get => _login;
			set => SetProperty(ref _login, value);
		}

		/// <summary>
		/// Возвращает команду для авторизации.
		/// </summary>
		public ICommand LoginCommand
		{
			get
			{
				_loginCommand = _loginCommand ?? new MvxCommand(LoginExecute);
				return _loginCommand;
			}
		}

		public ICommand CreateAccountCommand
		{
			get
			{
				_createAccountCommand = _createAccountCommand ?? new MvxCommand(() =>
				{
					NavigationService.Navigate<EntrepreneurAndBuyerViewModel>();
				});
				return _createAccountCommand;
			}
		}

		/// <summary>
		/// Возвращает команду для перехода на восстановление пароля.
		/// </summary>
		public ICommand ForgotPasswordCommand
		{
			get
			{
				_forgotPasswordCommand = _forgotPasswordCommand ?? new MvxCommand(() =>
				{
					NavigationService.Navigate<AuthorizationRecoveryViewModel>();
				});
				return _forgotPasswordCommand;
			}
		}

		/// <summary>
		/// Возвращает или устанавливает пароль пользователя.
		/// </summary>
		public string Password
		{
			get => _password;
			set => SetProperty(ref _password, value);
		}
		#endregion

		#region Private
		/// <summary>
		/// Проверяет валидность почты.
		/// </summary>
		/// <param name="email">Адрес электронной почты.</param>
		/// <returns>Возвращает <c>true</c> если адрес является валидным.</returns>
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

		/// <summary>
		/// Выполняет вход в систему.
		/// </summary>
		private async void LoginExecute()
		{
			var email = Login?.Trim();
			var password = Password?.Trim();
			if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
			{
				await Application.Current.MainPage.DisplayAlert("Внимание", "E-mail и пароль должны быть заполнены", "Ок");
				return;
			}

			if (!IsValidEmail(email))
			{
				await Application.Current.MainPage.DisplayAlert("Внимание", "E-mail некорректен", "Ок");
				return;
			}

			var authService = new AuthService();
			User user = null;
			try
			{
				user = await authService.Login(new AuthDto
				{
					Email = email,
					Password = password
				});
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}

			if (user == null)
			{
				if (authService.ServerAuthorizationError == null)
				{
					await Application.Current.MainPage.DisplayAlert("Внимание", "Ошибка сервера", "Ок");
					return;
				}

				Errors = authService.ServerAuthorizationError.Errors;

				if (!string.IsNullOrEmpty(authService.ServerAuthorizationError.Message))
				{
					await Application.Current.MainPage.DisplayAlert("Внимание", authService.ServerAuthorizationError.Message, "Ок");
				}

				return;
			}

			var repository = new UserRepository();
			repository.Add(user);

			if (user.Role == UserRole.Buyer)
			{
				//Application.Current.MainPage = new MainPage();
			}

			if (user.Role == UserRole.Entrepreneur)
			{
				//Application.Current.MainPage = new MainBusinessmanTabbedPage();
			}
		}
		#endregion
	}
}
