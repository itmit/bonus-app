using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using bonus.app.Core.Models;
using bonus.app.Core.Repositories;
using bonus.app.Core.Services;
using bonus.app.Core.ViewModels.Profile;
using MvvmCross.Navigation;
using Xamarin.Forms;

namespace bonus.app.Core.ViewModels.Auth
{
	public class BusinessmanRegistrationViewModel : BaseRegistrationViewModel
	{
		private readonly IMvxNavigationService _navigationService;

		private readonly IAuthService _authService;

		public BusinessmanRegistrationViewModel(IMvxNavigationService navigationService, IAuthService authService)
		{
			_navigationService = navigationService;
			_authService = authService;
		}

		protected override async Task<bool> RegistrationCommandExecute()
		{
			try
			{
				var user = await _authService.Register(new User
				{
					Email = Email,
					Login = Login,
					Name = Name,
					Role = UserRole.Businessman
				}, Password, ConfirmPassword);

				if (user == null)
				{
					var dictionary = new Dictionary<string, string>();
					foreach (var detail in _authService.ErrorDetails)
					{
						dictionary[detail.Key] = string.Join("&#10;", detail.Value);
					}
					Errors = dictionary;
					if (!string.IsNullOrEmpty(_authService.Error))
					{
						Device.BeginInvokeOnMainThread(() =>
						{
							Application.Current.MainPage.DisplayAlert("Ошибка", _authService.Error, "Ок");
						});
					}

					return false;
				}
				await _navigationService.Navigate<EditProfileBusinessmanViewModel>();
				return true;
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}

			return false;
		}
	}
}
