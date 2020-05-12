using System;
using System.Threading.Tasks;
using bonus.app.Core.Services;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace bonus.app.Core.ViewModels.Customer.BonusAccrual
{
	public class CustomerBonusAccrualViewModel : MvxNavigationViewModel
	{
		#region Data
		#region Fields
		private readonly IAuthService _authService;
		private Guid _userUuid;
		private MvxCommand _openMyBonusesCommand;
		#endregion
		#endregion

		#region .ctor
		public CustomerBonusAccrualViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IAuthService authService)
			: base(logProvider, navigationService)
		{
			_authService = authService;
		}
		#endregion

		#region Properties
		public Guid UserUuid
		{
			get => _userUuid;
			set => SetProperty(ref _userUuid, value);
		}

		public MvxCommand OpenMyBonusesCommand
		{
			get
			{
				_openMyBonusesCommand = _openMyBonusesCommand ?? new MvxCommand(() =>
				{
					NavigationService.Navigate<MyBonusViewModel>();
				});
				return _openMyBonusesCommand;
			}
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
