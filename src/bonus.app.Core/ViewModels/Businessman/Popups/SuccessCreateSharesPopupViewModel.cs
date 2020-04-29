using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace bonus.app.Core.ViewModels.Businessman.Popups
{
	public class SuccessCreateSharesPopupViewModel : MvxViewModel<object, bool>
	{
		#region Data
		#region Fields
		private MvxCommand _closeModalCommand;
		private readonly IMvxNavigationService _navigationService;
		#endregion
		#endregion

		#region .ctor
		public SuccessCreateSharesPopupViewModel(IMvxNavigationService navigationService) => _navigationService = navigationService;
		#endregion

		#region Properties
		public MvxCommand CloseModalCommand
		{
			get
			{
				_closeModalCommand = _closeModalCommand ??
									 new MvxCommand(() =>
									 {
										 _navigationService.Close(this, true);
									 });
				return _closeModalCommand;
			}
		}
		#endregion

		#region Overrided
		public override void Prepare(object parameter)
		{
		}
		#endregion
	}
}
