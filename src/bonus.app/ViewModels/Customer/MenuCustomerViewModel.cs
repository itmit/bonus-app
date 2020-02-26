using System.Linq;
using bonus.app.Core.Repositories;
using bonus.app.Core.ViewModels.Auth;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace bonus.app.Core.ViewModels.Customer
{
	public class MenuCustomerViewModel : MvxViewModel
	{
		private MvxCommand _logOutCommand;
		private readonly IUserRepository _userRepository;
		private readonly IMvxNavigationService _navigationService;

		public MenuCustomerViewModel(IMvxNavigationService navigationService, IUserRepository userRepository)
		{
			_navigationService = navigationService;
			_userRepository = userRepository;
		}

		public MvxCommand LogOutCommand
		{
			get
			{
				_logOutCommand = _logOutCommand ?? new MvxCommand(LogOutCommandExecute);
				return _logOutCommand;
			}
		}

		private async void LogOutCommandExecute()
		{
			var user = _userRepository.GetAll()
									  .Single();
			
			_userRepository.Remove(user);
			await _navigationService.Navigate<AuthorizationViewModel>();
		}
	}
}
