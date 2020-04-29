using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace bonus.app.Core.ViewModels.Auth
{
	public class SuccessRegisterBusinessmanPopupViewModel : MvxViewModel
	{
		#region Data
		#region Fields
		private MvxCommand _closePopupCommand;
		private readonly IMvxNavigationService _navigationService;
		#endregion
		#endregion

		#region .ctor
		public SuccessRegisterBusinessmanPopupViewModel(IMvxNavigationService navigationService) => _navigationService = navigationService;
		#endregion

		#region Properties
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
