using System;
using System.Linq;
using System.Threading.Tasks;
using bonus.app.Core.Repositories;
using bonus.app.Core.Services;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace bonus.app.Core.ViewModels.Customer.BonusAccrual
{
	public class CustomerBonusAccrualViewModel : MvxViewModel
	{
		private IMvxNavigationService _navigationService;
		private Guid _userUuid;
		private readonly IAuthService _authService;

		public CustomerBonusAccrualViewModel(IAuthService authService, IMvxNavigationService navigationService)
		{
			_authService = authService;
			_navigationService = navigationService;
		}

		public Guid UserUuid
		{
			get => _userUuid;
			set => SetProperty(ref _userUuid, value);
		}

		public override Task Initialize()
		{
			UserUuid = _authService.User.Uuid;
			return base.Initialize();
		}
	}
}
