using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using bonus.app.Core.Dtos;
using bonus.app.Core.Dtos.CustomerDtos;
using bonus.app.Core.Models;
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
		private IFacebookService _facebookService;
		private IVkService _vkService;
		private MvxCommand _facebookLoginCommand;
		private IProfileService _profileService;
		#endregion
		#endregion

		#region .ctor
		/// <summary>
		/// Инициализирует новый экземпляр <see cref="AuthorizationViewModel" />.
		/// </summary>
		/// <param name="logProvider">Провайдер логов.</param>
		/// <param name="navigationService">Сервис для навигации.</param>
		/// <param name="authService">Сервис для авторизации.</param>
		public AuthorizationViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IAuthService authService, IVkService vkService, IFacebookService facebookService, IProfileService profileService)
			: base(logProvider, navigationService)
		{
			_authService = authService;
			_vkService = vkService;
			_facebookService = facebookService;
			_profileService = profileService;
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
		public IMvxCommand VkLoginCommand
		{
			get
			{
				_openAuthVkFcPage = _openAuthVkFcPage ??
									new MvxCommand(async () =>
									{
										var result = await _vkService.Login();
										await AuthorizationAnExternalService(result, ExternalAuthService.Vk);
									});
				return _openAuthVkFcPage;
			}
		}

		private async Task AuthorizationAnExternalService(LoginResult result, ExternalAuthService serviceType)
		{
			string serviceName;
			switch (serviceType)
			{
				case ExternalAuthService.Vk:
					serviceName = "Vk";
					break;
				case ExternalAuthService.Facebook:
					serviceName = "Facebook";
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(serviceType), serviceType, null);
			}

			switch (result.LoginState)
			{
				case LoginState.Canceled:
					// Обработать
					Device.BeginInvokeOnMainThread(() =>
					{
						Application.Current.MainPage.DisplayAlert("Ошибка", $"Авторизация через {serviceName} отменена.", "Ок");
					});
					break;
				case LoginState.Success:

					User user = null;
					try
					{
						user = await _authService.AuthorizationAnExternalService(result.Email, result.Token, serviceType);
					}
					catch (Exception e)
					{
						Console.WriteLine(e);
					}

					var isActive = true;
					if (user == null)
					{
						var role = await Application.Current.MainPage.DisplayAlert("Внимание",
																				   "Выберите тип аккаунта.",
																				   "Предприниматель",
																				   "Покупатель") ? UserRole.Businessman : UserRole.Customer;

						user = await _authService.Register(new User
														   {
															   Email = result.Email,
															   Name = $"{result.FirstName} {result.LastName}",
															   Login = result.UserId,
															   Role = role
						},
														   result.UserId,
														   result.UserId);
						if (user == null)
						{
							Device.BeginInvokeOnMainThread(() =>
							{
								Application.Current.MainPage.DisplayAlert("Ошибка",
																		  $"Авторизация через {serviceName} пошла успешно, но не удалось зарегистрировать данного пользователя в системе.",
																		  "Ок");
							});
							return;
						}

						isActive = false;
					}

					if (string.IsNullOrEmpty(user.AccessToken?.Body) && user.Uuid != Guid.Empty)
					{
						if (isActive)
						{
							Device.BeginInvokeOnMainThread(() =>
							{
								Application.Current.MainPage.DisplayAlert("Внимание", $"Авторизация через {serviceName} невозможна, пока Вы не заполните статистическую информацию.", "Ок");
							});
						}
						else
						{
							switch (user.Role)
							{
								case UserRole.Businessman:
									await NavigationService.Navigate<EditProfileBusinessmanViewModel, EditProfileViewModelArguments>(
										new EditProfileViewModelArguments(user.Uuid, false, result.UserId));
									break;
								case UserRole.Customer:
									await NavigationService.Navigate<EditProfileCustomerViewModel, EditProfileViewModelArguments>(
										new EditProfileViewModelArguments(user.Uuid, false, result.UserId));
									break;
								default:
									throw new ArgumentOutOfRangeException();
							}
						}
						return;
					}

					switch (user.Role)
					{
						case UserRole.Businessman:
							await NavigationService.Navigate<MainBusinessmanViewModel>();
							break;
						case UserRole.Customer:
							await NavigationService.Navigate<MainCustomerViewModel>();
							break;
						default:
							throw new ArgumentOutOfRangeException();
					}

					break;
				case LoginState.Failed:
					Device.BeginInvokeOnMainThread(() =>
					{
						Application.Current.MainPage.DisplayAlert("Ошибка", "Не удалось авторизоваться.", "Ок");
					});
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		public MvxCommand FacebookLoginCommand {
			get
			{
				_facebookLoginCommand = _facebookLoginCommand ??
										new MvxCommand(async () =>
										{
											var result = await _facebookService.Login();
											await AuthorizationAnExternalService(result, ExternalAuthService.Facebook);
										});
				return _facebookLoginCommand;
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
				switch (user.Role)
				{
					case UserRole.Businessman:
						await NavigationService.Navigate<EditProfileBusinessmanViewModel, EditProfileViewModelArguments>(new EditProfileViewModelArguments(user.Uuid, false, password));
						break;
					case UserRole.Customer:
						await NavigationService.Navigate<EditProfileCustomerViewModel, EditProfileViewModelArguments>(new EditProfileViewModelArguments(user.Uuid, false, password));
						break;
					default:
						throw new ArgumentOutOfRangeException();
				}

				return;
			}

			switch (user.Role)
			{
				case UserRole.Businessman:
					await NavigationService.Navigate<MainBusinessmanViewModel>();
					break;
				case UserRole.Customer:
					await NavigationService.Navigate<MainCustomerViewModel>();
					break;
				default:
					throw new ArgumentOutOfRangeException();
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
