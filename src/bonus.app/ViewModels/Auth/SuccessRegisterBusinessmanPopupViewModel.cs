using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace bonus.app.Core.ViewModels.Auth
{
	public class SuccessRegisterBusinessmanPopupViewModel : MvxViewModel
	{
		private MvxCommand _closePopupCommand;
		private readonly IMvxNavigationService _navigationService;

		public SuccessRegisterBusinessmanPopupViewModel(IMvxNavigationService navigationService) => _navigationService = navigationService;

		public MvxCommand ClosePopupCommand
		{
			get
			{
				_closePopupCommand = _closePopupCommand ?? new MvxCommand(() =>
				{
					_navigationService.Close(this);
				});
				return _closePopupCommand;
			}
		}
	}
}
