using System;
using System.Collections.Generic;
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
		private string _contactPerson;
		private MvxCommand _editCommand;
		private readonly IMvxNavigationService _navigationService;
		private readonly IProfileService _profileService;
		private readonly IUserRepository _userRepository;
		private string _workingMode;
		#endregion
		#endregion

		#region .ctor
		public EditProfileBusinessmanViewModel(IAuthService authService,
											   IMvxNavigationService navigationService,
											   IGeoHelperService geoHelperService,
											   IProfileService profileService,
											   IUserRepository userRepository)
			: base(authService, geoHelperService)
		{
			_navigationService = navigationService;
			_profileService = profileService;
			_userRepository = userRepository;
		}
		#endregion

		#region Properties
		public string ContactPerson
		{
			get => _contactPerson;
			set => SetProperty(ref _contactPerson, value);
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
			var arg = new EditBusinessmanDto
			{
				Uuid = Parameter.Guid,
				Country = SelectedCountry.LocalizedNames.Ru,
				City = SelectedCity.LocalizedNames.Ru,
				Address = Address,
				WorkTime = WorkingMode,
				Contact = ContactPerson,
				Phone = PhoneNumber,
				Description = "-",
				Password = Parameter.Password
			};
			User user = null;
			try
			{
				user = await _profileService.Edit(arg, null);
				_userRepository.Add(user);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
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
		#endregion
	}
}
