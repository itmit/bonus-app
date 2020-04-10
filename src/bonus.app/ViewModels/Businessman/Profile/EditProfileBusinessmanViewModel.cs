using System;
using System.Collections.Generic;
using System.Diagnostics;
using bonus.app.Core.Dtos.BusinessmanDtos;
using bonus.app.Core.Models;
using bonus.app.Core.Repositories;
using bonus.app.Core.Services;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using Xamarin.Forms;

namespace bonus.app.Core.ViewModels.Businessman.Profile
{
	public class EditProfileBusinessmanViewModel : BaseEditProfileViewModel
	{
		#region Data
		#region Fields
		private string _contact;
		private MvxCommand _editCommand;
		private readonly IMvxNavigationService _navigationService;
		private readonly IProfileService _profileService;
		private readonly IUserRepository _userRepository;
		private string _workingMode;
		private string _description;
		#endregion
		#endregion

		#region .ctor
		public EditProfileBusinessmanViewModel(IAuthService authService,
											   IMvxNavigationService navigationService,
											   IGeoHelperService geoHelperService,
											   IProfileService profileService,
											   IUserRepository userRepository,
											   IPermissionsService permissionsService)
			: base(authService, geoHelperService, permissionsService)
		{
			_navigationService = navigationService;
			_profileService = profileService;
			_userRepository = userRepository;
		}
		#endregion

		#region Properties
		public string Contact
		{
			get => _contact;
			set
			{
				SetProperty(ref _contact, value);
				if (string.IsNullOrEmpty(value?.Trim()))
				{
					Errors[nameof(Contact)
							   .ToLower()] = "Контактное лицо не может быть пустым.";
				}
				else
				{
					Errors[nameof(Contact)
							   .ToLower()] = null;
				}

				RaisePropertyChanged(() => Errors);
			}
		}

		public MvxCommand EditCommand
		{
			get
			{
				_editCommand = _editCommand ?? new MvxCommand(EditCommandExecute);
				return _editCommand;
			}
		}

		public string WorkingMode
		{
			get => _workingMode;
			set => SetProperty(ref _workingMode, value);
		}
		#endregion

		#region Private
		private async void EditCommandExecute()
		{
			if (SelectedCountry == null)
			{
				Device.BeginInvokeOnMainThread(() =>
				{
					Application.Current.MainPage.DisplayAlert("Внимание", "Выберите страну.", "Ок");
				});
				return;
			}

			if (SelectedCity == null)
			{
				Device.BeginInvokeOnMainThread(() =>
				{
					Application.Current.MainPage.DisplayAlert("Внимание", "Выберите город.", "Ок");
				});
				return;
			}

			var isValid = true;
			if (string.IsNullOrEmpty(Address?.Trim()))
			{
				Errors[nameof(Address)
						   .ToLower()] = "Адрес не может быть пустым.";
				isValid = false;
			}
			else
			{
				Errors[nameof(Address)
						   .ToLower()] = null;
			}
			if (string.IsNullOrEmpty(Contact?.Trim()))
			{
				Errors[nameof(Contact)
						   .ToLower()] = "Контактное лицо не может быть пустым.";
			}
			else
			{
				Errors[nameof(Contact)
						   .ToLower()] = null;
			}


			await RaisePropertyChanged(() => Errors);
			if (!isValid)
			{
				return;
			}

			var arg = new EditBusinessmanDto
			{
				Uuid = Parameter.Guid,
				Country = SelectedCountry.LocalizedNames.Ru,
				City = SelectedCity.LocalizedNames.Ru,
				Address = Address,
				WorkTime = WorkingMode,
				Contact = Contact,
				Phone = PhoneNumber,
				Description = Description,
				Password = Parameter.Password
			};
			User user = null;
			try
			{
				user = await _profileService.Edit(arg, ImageBytes, ImageName);
				_userRepository.Add(user);
			}
			catch (Exception e)
			{
				Debug.WriteLine(e);
			}

			if (user?.AccessToken != null && !string.IsNullOrEmpty(user.AccessToken.Body))
			{
				await _navigationService.Navigate<MainBusinessmanViewModel>();
				return;
			}

			var dictionary = new Dictionary<string, string>();
			foreach (var detail in _profileService.ErrorDetails)
			{
				dictionary[detail.Key] = string.Join("&#10;", detail.Value);
			}

			Errors = dictionary;
			if (!string.IsNullOrEmpty(_profileService.Error))
			{
				Device.BeginInvokeOnMainThread(() =>
				{
					Application.Current.MainPage.DisplayAlert("Ошибка", _profileService.Error, "Ок");
				});
			}
		}

		public string Description
		{
			get => _description;
			set
			{
				SetProperty(ref _description, value);
				if (string.IsNullOrEmpty(value?.Trim()))
				{
					Errors[nameof(Description)
							   .ToLower()] = "Описание не может быть пустым.";
				}
				else
				{
					Errors[nameof(Description)
							   .ToLower()] = null;
				}

				RaisePropertyChanged(() => Errors);
			}
		}
		#endregion
	}
}
