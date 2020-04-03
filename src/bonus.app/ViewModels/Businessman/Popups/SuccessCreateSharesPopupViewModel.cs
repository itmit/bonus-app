using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace bonus.app.Core.ViewModels.Businessman.Popups
{
	public class SuccessCreateSharesPopupViewModel : MvxViewModel<object, bool>
	{
		private readonly IMvxNavigationService _navigationService;
		private MvxCommand _closeModalCommand;

		public SuccessCreateSharesPopupViewModel(IMvxNavigationService navigationService)
		{
			_navigationService = navigationService;
		}

		public MvxCommand CloseModalCommand
		{
			get
			{
				_closeModalCommand = _closeModalCommand ??
									 new MvxCommand(() =>
									 {
										 _navigationService.Close(this,true);
									 });
				return _closeModalCommand;
			}
		}

		public override void Prepare(object parameter)
		{ }
	}
}
