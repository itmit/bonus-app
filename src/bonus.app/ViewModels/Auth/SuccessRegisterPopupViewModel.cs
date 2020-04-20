using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace bonus.app.Core.ViewModels.Auth
{
	public class SuccessRegisterPopupViewModel : MvxViewModel<object, bool>
	{
		private readonly IMvxNavigationService _navigationService;

		public SuccessRegisterPopupViewModel(IMvxNavigationService navigationService) => _navigationService = navigationService;

		public override void Prepare(object parameter)
		{}

		private MvxCommand _openEditPageCommand;

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
	}
}
