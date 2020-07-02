using bonus.app.Core.Models.UserModels;
using bonus.app.Core.Services;
using bonus.app.Core.Validations;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace bonus.app.Core.ViewModels.Businessman.Managers
{
	public class EditManagerViewModel : MvxViewModel<User>
	{
		private User _user;

		public User User
		{
			get => _user;
			private set => SetProperty(ref _user, value);
		}
		public ValidatableObject<string> PhoneNumber
		{
			get => _phoneNumber;
			set => SetProperty(ref _phoneNumber, value);
		}

		private ValidatableObject<string> _name = new ValidatableObject<string>();
		private readonly IManagerService _managerService;
		private readonly IMvxNavigationService _navigationService;
		private ValidatableObject<string> _phoneNumber = new ValidatableObject<string>();

		public EditManagerViewModel(IMvxNavigationService navigationService, IManagerService managerService)
		{
			_navigationService = navigationService;
			_managerService = managerService;
		}

		public ValidatableObject<string> Name
		{
			get => _name;
			set => SetProperty(ref _name, value);
		}
		public override void Prepare(User parameter)
		{
			User = parameter;
		}
	}
}
