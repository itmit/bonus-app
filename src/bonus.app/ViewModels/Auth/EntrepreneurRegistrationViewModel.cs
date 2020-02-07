using System;
using bonus.app.Core.Models;
using bonus.app.Core.Repositories;
using bonus.app.Core.ViewModels.Profile;
using MvvmCross.Navigation;

namespace bonus.app.Core.ViewModels.Auth
{
	public class EntrepreneurRegistrationViewModel : BaseRegistrationViewModel
	{
		private readonly IMvxNavigationService _navigationService;
		private readonly IUserRepository _userRepository;

		public EntrepreneurRegistrationViewModel(IMvxNavigationService navigationService, IUserRepository userRepository)
		{
			_navigationService = navigationService;
			_userRepository = userRepository;
		}

		protected override void RegistrationCommandExecute()
		{
			_navigationService.Navigate<EditProfileEntrepreneurViewModel>();
		}
	}
}
