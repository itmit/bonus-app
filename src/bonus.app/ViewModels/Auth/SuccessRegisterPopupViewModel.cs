using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace bonus.app.Core.ViewModels.Auth
{
	public class SuccessRegisterPopupViewModel : MvxViewModel<object, bool>
	{
		#region Data
		#region Fields
		private readonly IMvxNavigationService _navigationService;

		private MvxCommand _openEditPageCommand;
		#endregion
		#endregion

		#region .ctor
		public SuccessRegisterPopupViewModel(IMvxNavigationService navigationService) => _navigationService = navigationService;
		#endregion

		#region Properties
		public MvxCommand OpenEditPageCommand
		{
			get
			{
				_openEditPageCommand = _openEditPageCommand ??
									   new MvxCommand(() =>
									   {
										   _navigationService.Close(this, true);
									   });
				return _openEditPageCommand;
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
