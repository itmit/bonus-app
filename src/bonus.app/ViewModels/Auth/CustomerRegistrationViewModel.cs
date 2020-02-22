using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using bonus.app.Core.Models;
using bonus.app.Core.Repositories;
using bonus.app.Core.Services;
using bonus.app.Core.ViewModels.Customer.Profile;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Xamarin.Forms;

namespace bonus.app.Core.ViewModels.Auth
{
	public class CustomerRegistrationViewModel : BaseRegistrationViewModel
	{
		private readonly IMvxNavigationService _navigationService;
		private IMvxCommand _openAuthVkOrFc;
		private readonly IAuthService _authService;
		private MvxCommand _openEditPageCommand;
		private User _user;

		public CustomerRegistrationViewModel(IMvxNavigationService navigationService, IAuthService authService)
		{
			_authService = authService;
			_navigationService = navigationService;
		}


		public IMvxCommand OpenAuthVkOrFc
		{
			get
			{
				_openAuthVkOrFc = _openAuthVkOrFc ?? new MvxCommand(() =>
				{
					_navigationService.Navigate<AuthVkFcViewModel>();
				});
				return _openAuthVkOrFc;
			}
		}

		public MvxCommand OpenEditPageCommand
		{
			get
			{
				_openEditPageCommand = _openEditPageCommand ?? new MvxCommand(() =>
				{
					_navigationService.Navigate<EditProfileCustomerViewModel, EditProfileViewModelArguments>(new EditProfileViewModelArguments(_user.Guid, Password));
				});
				return _openEditPageCommand;
			}
		}

		protected override async Task<bool> RegistrationCommandExecute()
		{
			try
			{
				_user = await _authService.Register(new User
				{
					Email = Email,
					Login = Login,
					Name = Name,
					Role = UserRole.Customer
				}, Password, ConfirmPassword);

				if (_user == null)
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
