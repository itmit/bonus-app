using System;
using bonus.app.Core.Dtos.CustomerDtos;
using bonus.app.Core.Models;
using bonus.app.Core.Repositories;
using bonus.app.Core.Services;
using bonus.app.Core.ViewModels.Auth;
using MvvmCross.Commands;
using MvvmCross.Navigation;

namespace bonus.app.Core.ViewModels.Customer.Profile
{
	public class EditProfileCustomerViewModel : BaseEditProfileViewModel
	{
		#region Data
		#region Fields
		private DateTime _birthday;
		private string _car = "";
		private readonly IProfileService _customerProfileService;

		private MvxCommand _editCommand;
		private bool _isFemale;
		private bool _isMale;
		private readonly IMvxNavigationService _navigationService;
		private readonly IUserRepository _userRepository;
		#endregion
		#endregion

		#region .ctor
		public EditProfileCustomerViewModel(IAuthService authService,
											IMvxNavigationService navigationService,
											IGeoHelperService geoHelperService,
											IProfileService customerProfileService,
											IUserRepository userRepository)
			: base(authService, geoHelperService)
		{
			_navigationService = navigationService;
			_customerProfileService = customerProfileService;
			_userRepository = userRepository;
			IsFemale = true;
		}
		#endregion

		#region Properties
		public DateTime Birthday
		{
			get => _birthday;
			set => SetProperty(ref _birthday, value);
		}

		public string Car
		{
			get => _car;
			set => SetProperty(ref _car, value);
		}

		public MvxCommand EditCommand
		{
			get
			{
				_editCommand = _editCommand ?? new MvxCommand(EditCommandExecute);
				return _editCommand;
			}
		}

		public bool IsFemale
		{
			get => _isFemale;
			set
			{
				_isMale = !value;
				RaisePropertyChanged(() => IsMale);
				SetProperty(ref _isFemale, value);
			}
		}

		public bool IsMale
		{
			get => _isMale;
			set
			{
				_isFemale = !value;
				RaisePropertyChanged(() => IsFemale);
				SetProperty(ref _isMale, value);
			}
		}
		#endregion

		#region Public
		public void OpenAuthorization()
		{
			_navigationService.Navigate<AuthorizationViewModel>();
		}
		#endregion

		#region Private
		private async void EditCommandExecute()
		{
			var arg = new EditCustomerDto
			{
				Uuid = Parameter.Guid,
				Country = SelectedCountry.LocalizedNames.Ru,
				City = SelectedCity.LocalizedNames.Ru,
				Phone = PhoneNumber,
				Birthday = Birthday.ToString("yyyy-MM-dd"),
				Car = Car,
				Address = Address,
				Password = Parameter.Password
			};
			if (IsFemale)
			{
				arg.Sex = "female";
			}
			else if (IsMale)
			{
				arg.Sex = "male";
			}

			User user = null;
			try
			{
				user = await _customerProfileService.Edit(arg, null);
				_userRepository.Add(user);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}

			if (user?.AccessToken != null && !string.IsNullOrEmpty(user.AccessToken.Body))
			{
				await _navigationService.Navigate<MainCustomerViewModel>();
			}
		}
		#endregion
	}
}
