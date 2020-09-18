using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using bonus.app.Core.Dtos;
using bonus.app.Core.Models;
using bonus.app.Core.Models.UserModels;
using bonus.app.Core.Services;
using bonus.app.Core.ViewModels.Businessman;
using bonus.app.Core.ViewModels.Businessman.Profile;
using bonus.app.Core.ViewModels.Customer;
using bonus.app.Core.ViewModels.Customer.Profile;
using bonus.app.Core.ViewModels.Manager;
using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.Forms.Presenters;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Xamarin.Forms;
using XF.Material.Forms.UI.Dialogs;

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
		/// Сервис авторизации.
		/// </summary>
		private readonly IAuthService _authService;

		/// <summary>
		/// Команда создания аккаунта.
		/// </summary>
		private MvxCommand _createAccountCommand;

		/// <summary>
		/// Ошибки авторизации.
		/// </summary>
		private Dictionary<string, string> _errors;

		/// <summary>
		/// Команда восстановления пароля.
		/// </summary>
		private ICommand _forgotPasswordCommand;

		/// <summary>
		/// Логин пользователя.
		/// </summary>
		private string _login;

		/// <summary>
		/// Команда для авторизации.
		/// </summary>
		private ICommand _loginCommand;

		/// <summary>
		/// Команда входа через ВКонтакте.
		/// </summary>
		private IMvxCommand _vkLoginCommand;

		/// <summary>
		/// Пароль пользователя.
		/// </summary>
		private string _password;

		/// <summary>
		/// Сервис для входа через facebook.
		/// </summary>
		private readonly IFacebookService _facebookService;

		/// <summary>
		/// Сервис для входа через ВКонтакте.
		/// </summary>
		private readonly IVkService _vkService;

		/// <summary>
		/// Команда входа через facebook.
		/// </summary>
		private MvxCommand _facebookLoginCommand;
		#endregion
		#endregion

		#region .ctor
		/// <summary>
		/// Инициализирует новый экземпляр <see cref="AuthorizationViewModel" />.
		/// </summary>
		/// <param name="logProvider">Провайдер логов.</param>
		/// <param name="navigationService">Сервис для навигации.</param>
		/// <param name="authService">Сервис для авторизации.</param>
		public AuthorizationViewModel(IMvxLogProvider logProvider
									  , IMvxNavigationService navigationService
									  , IAuthService authService)
			: base(logProvider, navigationService)
		{
			_authService = authService;
			Mvx.IoCProvider.TryResolve(out _vkService);
			Mvx.IoCProvider.TryResolve(out _facebookService);
		}
		#endregion
		private bool _isBusy;

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
		/// Возвращает команду для перехода на авторизацию через Vk.
		/// </summary>
		public IMvxCommand VkLoginCommand
		{
			get
			{
				_vkLoginCommand = _vkLoginCommand ??
									new MvxCommand(async () =>
									{
										var result = await _vkService.Login();
										await AuthorizationAnExternalService(result, ExternalAuthService.Vk);
									});
				return _vkLoginCommand;
			}
		}

		/// <summary>
		/// Авторизует либо регистрирует пользователя в системе, после входа через ВК или Facebook.
		/// </summary>
		/// <param name="result">Результат входа.</param>
		/// <param name="serviceType">Тип внешнего сервиса для входа (ВК/Facebook)</param>
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
					await MaterialDialog.Instance.AlertAsync($"Авторизация через {serviceName} отменена", "Ошибка", "Ок");
					break;
				case LoginState.Success:

					User user = null;
					var dialog = await MaterialDialog.Instance.LoadingDialogAsync(message: "Авторизация...");
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
						await dialog.DismissAsync();
						var confirm = await MaterialDialog.Instance.ConfirmAsync("Выберите тип аккаунта", "Внимание", "Предприниматель", "Покупатель");
						if (confirm == null)
						{
							return;
						}
						var role = confirm.Value ? UserRole.Businessman : UserRole.Customer;

						dialog = await MaterialDialog.Instance.LoadingDialogAsync(message: "Создание аккаунта...");
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
							await dialog.DismissAsync();
							await MaterialDialog.Instance.AlertAsync($"Авторизация через {serviceName} пошла успешно, но не удалось создать аккаунт данного пользователя в системе",
																	 "Ошибка", "Ок");
							IsBusy = false;
							return;
						}

						isActive = false;
					}

					if (string.IsNullOrEmpty(user.AccessToken?.Body) && user.Uuid != Guid.Empty)
					{
						if (isActive)
						{
							await dialog.DismissAsync();
							await MaterialDialog.Instance.AlertAsync($"Авторизация через {serviceName} невозможна, пока Вы не заполните статистическую информацию",
																	 "Внимание", "Ок");
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
								case UserRole.Manager:
									break;
								default:
									throw new ArgumentOutOfRangeException();
							}
							await dialog.DismissAsync();
						}
						IsBusy = false;
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
						case UserRole.Manager:
							await NavigationService.Navigate<MainManagerViewModel>();
							break;
						default:
							throw new ArgumentOutOfRangeException();
					}

					await dialog.DismissAsync();
					break;
				case LoginState.Failed:
					await MaterialDialog.Instance.AlertAsync("Не удалось авторизоваться",
															 "Ошибка", "Ок");
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}

			IsBusy = false;
		}

		/// <summary>
		/// Возвращает команду для перехода на авторизацию через Facebook.
		/// </summary>
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
			if (IsBusy)
			{
				return;
			}
			IsBusy = true;

			var login = Login?.Trim();
			var password = Password?.Trim();
			if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
			{
				await MaterialDialog.Instance.AlertAsync("E-mail и пароль должны быть заполнены",
														 "Внимание", "Ок");
				IsBusy = false;
				return;
			}

			if (!CheckIsEmail(login) && !CheckIsPhoneNumber(login))
			{
				await MaterialDialog.Instance.AlertAsync("Введите Email или номер телефона в формате международном формате, начиная с плюса, без пробелов, скобок",
														 "Внимание", "Ок");
				IsBusy = false;
				return;
			}

			if (password.Length < 6)
			{
				await MaterialDialog.Instance.AlertAsync("Пароль должен состоять минимум из 6 символов",
														 "Внимание", "Ок");
				IsBusy = false;
				return;
			}

			User user = null;
			var dialog = await MaterialDialog.Instance.LoadingDialogAsync(message: "Авторизация...");
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
				IsBusy = false;
				await dialog.DismissAsync();
				return;
			}

			if (string.IsNullOrEmpty(user.AccessToken.Body) && user.Uuid != Guid.Empty)
			{
				switch (user.Role)
				{
					case UserRole.Businessman:
						await NavigationService.Navigate<EditProfileBusinessmanViewModel, EditProfileViewModelArguments>(
							new EditProfileViewModelArguments(user.Uuid, false, password));
						break;
					case UserRole.Customer:
						await NavigationService.Navigate<EditProfileCustomerViewModel, EditProfileViewModelArguments>(
							new EditProfileViewModelArguments(user.Uuid, false, password));
						break;
					case UserRole.Manager:
						break;
					default:
						throw new ArgumentOutOfRangeException();
				}

				await dialog.DismissAsync();
				IsBusy = false;
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
				case UserRole.Manager:
					await NavigationService.Navigate<MainManagerViewModel>();
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}

			await dialog.DismissAsync();
			IsBusy = false;
		}

		public bool IsBusy
		{
			get => _isBusy;
			set => SetProperty(ref _isBusy, value);
		}

		private static bool CheckIsEmail(string email)
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

		private bool CheckIsPhoneNumber(string number) => Regex.Match(number, @"^\+\d*").Success;

		/// <summary>
		/// Показывает ошибку при авторизации.
		/// </summary>
		private void ShowErrors()
		{
			if (_authService.ErrorDetails == null)
			{
				MaterialDialog.Instance.AlertAsync("Ошибка сервера", "Внимание", "Ок");
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
				MaterialDialog.Instance.AlertAsync(_authService.Error,
												   "Внимание", "Ок");
			}
		}
		#endregion
	}
}
