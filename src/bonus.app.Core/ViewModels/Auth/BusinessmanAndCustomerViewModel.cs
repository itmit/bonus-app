using bonus.app.Core.Models;
using bonus.app.Core.Models.UserModels;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace bonus.app.Core.ViewModels.Auth
{
	/// <summary>
	/// Представляет модель для страницы выбора типа пользователя, при первом запуске приложения.
	/// </summary>
	public class BusinessmanAndCustomerViewModel : MvxNavigationViewModel
	{
		#region Data
		#region Fields
		/// <summary>
		/// Команда для перехода на авторизацию.
		/// </summary>
		private MvxCommand _openAuthorizationPageCommand;

		/// <summary>
		/// Команда для перехода на регистрацию покупателя.
		/// </summary>
		private MvxCommand _openBuyerRegistrationCommand;

		/// <summary>
		/// Команда для перехода на регистрацию предпринимателя.
		/// </summary>
		private MvxCommand _openPurchaserRegistrationCommand;
		private bool _isBusy;
		#endregion
		#endregion

		#region .ctor
		/// <summary>
		/// Инициализирует <see cref="BusinessmanAndCustomerViewModel" /> с параметрами.
		/// </summary>
		/// <param name="logProvider">Поставщик логов.</param>
		/// <param name="navigationService">Сервис навигации.</param>
		public BusinessmanAndCustomerViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService)
			: base(logProvider, navigationService)
		{
		}
		#endregion

		#region Properties
		public bool IsBusy
		{
			get => _isBusy;
			set => SetProperty(ref _isBusy, value);
		}

		/// <summary>
		/// Возвращает команду для перехода на авторизацию.
		/// </summary>
		public MvxCommand OpenAuthorizationPageCommand
		{
			get
			{
				_openAuthorizationPageCommand = _openAuthorizationPageCommand ??
												new MvxCommand(async () =>
												{
													if (IsBusy)
													{
														return;
													}
													IsBusy = true;
													await NavigationService.Navigate<AuthorizationViewModel>();
													IsBusy = false;
												});
				return _openAuthorizationPageCommand;
			}
		}

		/// <summary>
		/// Возвращает команду для перехода на регистрацию покупателя.
		/// </summary>
		public MvxCommand OpenBuyerRegistrationCommand
		{
			get
			{
				_openBuyerRegistrationCommand = _openBuyerRegistrationCommand ??
												new MvxCommand(async () =>
												{
													if (IsBusy)
													{
														return;
													}
													IsBusy = true;
													await NavigationService.Navigate<PublicOfferViewModel, UserRole>(UserRole.Customer);
													IsBusy = false;
												});
				return _openBuyerRegistrationCommand;
			}
		}

		/// <summary>
		/// Возвращает команду для перехода на регистрацию предпринимателя.
		/// </summary>
		public MvxCommand OpenEntrepreneurRegistrationCommand
		{
			get
			{
				_openPurchaserRegistrationCommand = _openPurchaserRegistrationCommand ??
													new MvxCommand(async () =>
													{
														if (IsBusy)
														{
															return;
														}
														IsBusy = true;
														await NavigationService.Navigate<PublicOfferViewModel, UserRole>(UserRole.Businessman);
														IsBusy = false;
													});
				return _openPurchaserRegistrationCommand;
			}
		}
		#endregion
	}
}
