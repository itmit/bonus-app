using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using bonus.app.Core.Dtos;
using bonus.app.Core.Models;
using bonus.app.Core.Repositories;
using bonus.app.Core.Services;
using bonus.app.Core.ViewModels.Businessman;
using bonus.app.Core.ViewModels.Businessman.Profile;
using bonus.app.Core.ViewModels.Customer;
using bonus.app.Core.ViewModels.Customer.Profile;
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
		private readonly IAuthService _authService;
		private MvxCommand _createAccountCommand;
		/// <summary>
		/// Ошибки авторизации.
		/// </summary>
		private Dictionary<string, string> _errors;
		private ICommand _forgotPasswordCommand;

		/// <summary>
		/// Логин пользователя.
		/// </summary>
		private string _login;

		/// <summary>
		/// Команда для авторизации.
		/// </summary>
		private ICommand _loginCommand;
		private IMvxCommand _openAuthVkFcPage;

		/// <summary>
		/// Пароль пользователя.
		/// </summary>
		private string _password;
		#endregion
		#endregion

		#region .ctor
		/// <summary>
		/// Инициализирует новый экземпляр <see cref="AuthorizationViewModel" />.
		/// </summary>
		/// <param name="logProvider">Провайдер логов.</param>
		/// <param name="navigationService">Сервис для навигации.</param>
		/// <param name="authService">Сервис для авторизации.</param>
		public AuthorizationViewModel(IMvxLogProvider logProvider,
									  IMvxNavigationService navigationService,
									  IAuthService authService)
			: base(logProvider, navigationService)
		{
			_authService = authService;
		}
		#endregion

		#region Properties
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
											NavigationService.Navigate<BusinessmanAndCustomerViewModel>();
										});
				return _createAccountCommand;
			}
		}

		/// <summary>
		/// Возвращает ошибки авторизации.
		/// </summary>
		public Dictionary<string, string> Errors
		{
			get => _errors;
			private set => SetProperty(ref _errors, value);
		}

		/// <summary>
		/// Возвращает команду для перехода на восстановление пароля.
		/// </summary>
		public ICommand ForgotPasswordCommand
		{
			get
			{
				_forgotPasswordCommand = _forgotPasswordCommand ??
										 new MvxCommand(() =>
										 {
											 NavigationService.Navigate<AuthorizationRecoveryViewModel>();
										 });
				return _forgotPasswordCommand;
			}
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

		/// <summary>
		/// Возвращает команду для перехода на авторизацию через Vk или Facebook.
		/// </summary>
		public IMvxCommand OpenAuthVkFcPage
		{
			get
			{
				_openAuthVkFcPage = _openAuthVkFcPage ??
									new MvxCommand(() =>
									{
										NavigationService.Navigate<AuthVkFcViewModel>();
									});
				return _openAuthVkFcPage;
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
		/// Выполняет вход в систему.
		/// </summary>
		private async void LoginExecute()
		{
			var login = Login?.Trim();
			var password = Password?.Trim();
			if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
			{
				await Application.Current.MainPage.DisplayAlert("Внимание", "E-mail и пароль должны быть заполнены", "Ок");
				return;
			}

			User user = null;
			try
			{
				user = await _authService.Login(new AuthDto
				{
					Login = login,
					Password = password
				});
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}

			if (user == null)
			{
				ShowErrors();
				return;
			}

			if (string.IsNullOrEmpty(user.AccessToken.Body) && user.Uuid != Guid.Empty)
			{
				if (user.Role == UserRole.Businessman)
				{
					await NavigationService.Navigate<EditProfileBusinessmanViewModel, EditProfileViewModelArguments>(new EditProfileViewModelArguments(user.Uuid, false, password));
				}
				else if (user.Role == UserRole.Customer)
				{
					await NavigationService.Navigate<EditProfileCustomerViewModel, EditProfileViewModelArguments>(new EditProfileViewModelArguments(user.Uuid, false, password));
				}

				return;
			}

			if (user.Role == UserRole.Businessman)
			{
				await NavigationService.Navigate<MainBusinessmanViewModel>();
			}
			else if (user.Role == UserRole.Customer)
			{
				await NavigationService.Navigate<MainCustomerViewModel>();
			}
		}

		private void ShowErrors()
		{
			if (_authService.ErrorDetails == null)
			{
				Application.Current.MainPage.DisplayAlert("Внимание", "Ошибка сервера", "Ок");
				return;
			}

			var dictionary = new Dictionary<string, string>();
			foreach (var detail in _authService.ErrorDetails)
			{
				dictionary[detail.Key] = string.Join("&#10;", detail.Value);
			}

			Errors = dictionary;

			if (!string.IsNullOrEmpty(_authService.Error))
			{
				Application.Current.MainPage.DisplayAlert("Внимание", _authService.Error, "Ок");
			}
		}
		#endregion
	}
}
