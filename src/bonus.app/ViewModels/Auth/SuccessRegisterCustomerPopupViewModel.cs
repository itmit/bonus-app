using System;
using bonus.app.Core.Services;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace bonus.app.Core.ViewModels.Auth
{
	public class SuccessRegisterCustomerPopupViewModel : MvxViewModel
	{
		private MvxCommand _closePopupCommand;
		private readonly IMvxNavigationService _navigationService;

		public SuccessRegisterCustomerPopupViewModel(IMvxNavigationService navigationService, IAuthService authService)
		{
			_navigationService = navigationService;

			UserUuid = authService.User.Uuid;
		}

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
	}
}
