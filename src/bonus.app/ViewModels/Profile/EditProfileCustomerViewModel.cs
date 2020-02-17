using System;
using System.Linq;
using System.Threading.Tasks;
using bonus.app.Core.Dto.GeoHelper;
using bonus.app.Core.Dtos.BusinessmanDtos;
using bonus.app.Core.Dtos.CustomerDtos;
using bonus.app.Core.Models;
using bonus.app.Core.Repositories;
using bonus.app.Core.Services;
using bonus.app.Core.ViewModels.Auth;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace bonus.app.Core.ViewModels.Profile
{
	public class EditProfileCustomerViewModel : BaseEditProfileViewModel
	{
		private readonly IMvxNavigationService _navigationService;
		private readonly IProfileService _customerProfileService;

		public void OpenAuthorization()
		{
			_navigationService.Navigate<AuthorizationViewModel>();
		}

		public EditProfileCustomerViewModel(IUserRepository userRepository, IMvxNavigationService navigationService, IGeoHelperService geoHelperService, IProfileService customerProfileService)
			: base(userRepository, navigationService, geoHelperService)
		{
			_navigationService = navigationService;
			_customerProfileService = customerProfileService;
			IsFemale = true;
		}

		private MvxCommand _editCommand;
		private bool _isMale;
		private bool _isFemale;
		private DateTime _birthday;
		private string _car = "";
		private EditProfileViewModelArguments _parameter;
		private readonly IUserRepository _userRepository;
		public MvxCommand EditCommand
		{
			get
			{
				_editCommand = _editCommand ?? new MvxCommand(EditCommandExecute);
				return _editCommand;
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

		private async void EditCommandExecute()
		{
			var arg = new EditCustomerDto
			{
				Uuid = _parameter.Guid,
				Country = SelectedCountry.LocalizedNames.Ru,
				City = SelectedCity.LocalizedNames.Ru,
				Phone = PhoneNumber,
				Birthday = Birthday.ToString("yyyy-MM-dd"),
				Car = Car,
				Address = Address,
				Password = _parameter.Password
			};
			if (IsFemale)
			{
				arg.Sex = "female";
			}
			else if (IsMale)
			{
				arg.Sex = "male";
			}

			try
			{
				var user = await _customerProfileService.Edit(arg, null);
				_userRepository.Add(user);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}

		public override void Prepare(EditProfileViewModelArguments parameter)
		{
			_parameter = parameter;
		}
	}
}
