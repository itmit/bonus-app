using System;
using bonus.app.Core.Dtos.BusinessmanDtos;
using bonus.app.Core.Models;
using bonus.app.Core.Repositories;
using bonus.app.Core.Services;
using MvvmCross.Commands;
using MvvmCross.Navigation;

namespace bonus.app.Core.ViewModels.Businessman.Profile
{
	public class EditProfileBusinessmanViewModel : BaseEditProfileViewModel
	{
		private MvxCommand _editCommand;
		private EditProfileViewModelArguments _parameter;
		private readonly IProfileService _profileService;
		private readonly IMvxNavigationService _navigationService;
		private readonly IUserRepository _userRepository;
		private string _workingMode;
		private string _contactPerson;

		public EditProfileBusinessmanViewModel(IUserRepository userRepository, IMvxNavigationService navigationService, IGeoHelperService geoHelperService, IProfileService profileService)
			: base(userRepository, navigationService, geoHelperService)
		{
			_userRepository = userRepository;
			_navigationService = navigationService;
			_profileService = profileService;
		}

		public override void Prepare(EditProfileViewModelArguments parameter)
		{
			_parameter = parameter;
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

		public string ContactPerson
		{
			get => _contactPerson;
			set => SetProperty(ref _contactPerson, value);
		}

		private async void EditCommandExecute()
		{
			var arg = new EditBusinessmanDto
			{
				Uuid = _parameter.Guid,
				Country = SelectedCountry.LocalizedNames.Ru,
				City = SelectedCity.LocalizedNames.Ru,
				Address = Address,
				WorkTime = WorkingMode,
				Contact = ContactPerson,
				Phone = PhoneNumber,
				Description = "-",
				Password = _parameter.Password
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
			}
		}
	}
}
