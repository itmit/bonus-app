using System;
using System.Linq;
using System.Threading.Tasks;
using bonus.app.Core.Models;
using bonus.app.Core.Models.UserModels;
using bonus.app.Core.Services;
using bonus.app.Core.Validations;
using bonus.app.Core.ViewModels.Businessman.Profile;
using MvvmCross.Navigation;
using Xamarin.Forms;

namespace bonus.app.Core.ViewModels.Auth
{
	public class BusinessmanRegistrationViewModel : BaseRegistrationViewModel
	{
		#region Data
		#region Fields
		private readonly IAuthService _authService;
		private readonly IMvxNavigationService _navigationService;
		#endregion
		#endregion

		#region .ctor
		public BusinessmanRegistrationViewModel(IMvxNavigationService navigationService, IAuthService authService)
		{
			_navigationService = navigationService;
			_authService = authService;

			Name.Validations.Add(new IsNotNullOrEmptyRule
			{
				ValidationMessage = "Укажите торговое название или имя мастера."
			});
			Name.Validations.Add(new MinLengthRule(2)
			{
				ValidationMessage = "Торговое название или имя мастера не может быть меньше 2 символов."
			});
		}
		#endregion

		#region Overrided
		protected override async Task<bool> RegistrationCommandExecute()
		{
			try
			{
				var user = await _authService.Register(new User
													   {
														   Email = Email.Value,
														   Login = Login.Value,
														   Name = Name.Value,
														   Role = UserRole.Businessman
													   },
													   Password.Value,
													   ConfirmPassword.Value);
				if (user != null)
				{
					await Application.Current.MainPage.Navigation.PopToRootAsync();
					await _navigationService.Navigate<EditProfileBusinessmanViewModel, EditProfileViewModelArguments>(
						new EditProfileViewModelArguments(user.Uuid, false, Password.Value));
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
