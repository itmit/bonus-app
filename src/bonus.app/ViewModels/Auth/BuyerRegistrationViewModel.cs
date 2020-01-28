using System;
using bonus.app.Core.Models;
using bonus.app.Core.Repositories;
using bonus.app.Core.ViewModels.Profile;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace bonus.app.Core.ViewModels.Auth
{
	public class BuyerRegistrationViewModel : BaseRegistrationViewModel
	{
		private readonly IMvxNavigationService _navigationService;
		private IMvxCommand _openAuthVkOrFc;
		private readonly IUserRepository _userRepository;

		public BuyerRegistrationViewModel(IMvxNavigationService navigationService, IUserRepository userRepository)
		{
			_userRepository = userRepository;
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

		protected override void RegistrationCommandExecute()
		{
			_userRepository.Add(new User
			{
				AccessToken = new AccessToken(),
				Guid = Guid.Empty,
				Login = Login,
				Role = UserRole.Buyer,
				Email = Email,
				MasterName = MasterName,
				PinCode = PinCode
			});

			_navigationService.Navigate<EditProfileBauerViewModel>();
		}
	}
}
