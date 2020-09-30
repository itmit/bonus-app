using System;
using bonus.app.Core.Services;
using bonus.app.Core.Services.Interfaces;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace bonus.app.Core.ViewModels.Auth
{
	public class SuccessRegisterCustomerPopupViewModel : MvxViewModel
	{
		#region Data
		#region Fields
		private MvxCommand _closePopupCommand;
		private readonly IMvxNavigationService _navigationService;
		#endregion
		#endregion

		#region .ctor
		public SuccessRegisterCustomerPopupViewModel(IMvxNavigationService navigationService, IAuthService authService)
		{
			_navigationService = navigationService;

			UserUuid = authService.User.Uuid;
		}
		#endregion

		#region Properties
		public Guid UserUuid
		{
			get;
		}

		public MvxCommand ClosePopupCommand
		{
			get
			{
				_closePopupCommand = _closePopupCommand ??
									 new MvxCommand(() =>
									 {
										 _navigationService.Close(this);
									 });
				return _closePopupCommand;
			}
		}
		#endregion
	}
}
