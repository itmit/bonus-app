using System;
using System.Linq;
using System.Threading.Tasks;
using bonus.app.Core.Models;
using bonus.app.Core.Models.UserModels;
using bonus.app.Core.Services;
using bonus.app.Core.Validations;
using bonus.app.Core.ViewModels.Customer.Profile;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using Xamarin.Forms;

namespace bonus.app.Core.ViewModels.Auth
{
	public class CustomerRegistrationViewModel : BaseRegistrationViewModel
	{
		#region Data
		#region Fields
		private readonly IAuthService _authService;
		private readonly IMvxNavigationService _navigationService;
		private IMvxCommand _openAuthVkOrFc;
		private MvxCommand _openEditPageCommand;
		private User _user;
		#endregion
		#endregion

		#region .ctor
		public CustomerRegistrationViewModel(IMvxNavigationService navigationService, IAuthService authService)
		{
			_authService = authService;
			_navigationService = navigationService;

			Name.Validations.Add(new IsNotNullOrEmptyRule
			{
				ValidationMessage = "Укажите Ф.И.О."
			});
			Name.Validations.Add(new MinLengthRule(2)
			{
				ValidationMessage = "Ф.И.О. быть меньше 2 символов."
			});
		}
		#endregion

		#region Properties
		public IMvxCommand OpenAuthVkOrFc
		{
			get
			{
				_openAuthVkOrFc = _openAuthVkOrFc ??
								  new MvxCommand(() =>
								  {
									  _navigationService.Navigate<AuthVkFcViewModel>();
								  });
				return _openAuthVkOrFc;
			}
		}
		#endregion

		#region Overrided
		protected override async Task<bool> RegistrationCommandExecute()
		{
			try
			{
				_user = await _authService.Register(new User
													{
														Email = Email.Value,
														Login = Login.Value,
														Name = Name.Value,
														Role = UserRole.Customer
													},
													Password.Value,
													ConfirmPassword.Value);

				if (_user != null)
				{
					await Application.Current.MainPage.Navigation.PopToRootAsync();
					if (await _navigationService.Navigate<SuccessRegisterPopupViewModel, object, bool>(null))
					{
						await _navigationService.Navigate<EditProfileCustomerViewModel, EditProfileViewModelArguments>(
							new EditProfileViewModelArguments(_user.Uuid, false, Password.Value));
					}

					return true;
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}

			if (_authService.ErrorDetails != null && _authService.ErrorDetails.Count > 0)
			{
				var key = _authService.ErrorDetails.First()
									  .Key;
				if (key.Equals("email"))
				{
					Device.BeginInvokeOnMainThread(() =>
					{
						Application.Current.MainPage.DisplayAlert("Ошибка", "Пользователь с таким Email адресом уже существует.", "Ок");
					});
					return false;
				}
			}

			if (!string.IsNullOrEmpty(_authService.Error))
			{
				Device.BeginInvokeOnMainThread(() =>
				{
					Application.Current.MainPage.DisplayAlert("Ошибка", _authService.Error, "Ок");
				});
			}

			return false;
		}
		#endregion
	}
}
