using System;
using System.Threading.Tasks;
using bonus.app.Core.Services;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace bonus.app.Core.ViewModels.Customer.BonusAccrual
{
	public class CustomerBonusAccrualViewModel : MvxViewModel
	{
		#region Data
		#region Fields
		private readonly IAuthService _authService;
		private IMvxNavigationService _navigationService;
		private Guid _userUuid;
		#endregion
		#endregion

		#region .ctor
		public CustomerBonusAccrualViewModel(IAuthService authService, IMvxNavigationService navigationService)
		{
			_authService = authService;
			_navigationService = navigationService;
		}
		#endregion

		#region Properties
		public Guid UserUuid
		{
			get => _userUuid;
			set => SetProperty(ref _userUuid, value);
		}
		#endregion

		#region Overrided
		public override Task Initialize()
		{
			UserUuid = _authService.User.Uuid;
			return base.Initialize();
		}
		#endregion
	}
}
