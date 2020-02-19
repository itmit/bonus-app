using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace bonus.app.Core.ViewModels
{
	public class BusinessmanBonusAccrualViewModel : MvxNavigationViewModel
	{
		#region Fields
		private IMvxCommand _furetherCommand;
		#endregion

		#region Ctor
		public BusinessmanBonusAccrualViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService)
			: base(logProvider, navigationService)
		{
		}
		#endregion


		#region Prop
		public IMvxCommand FurtherCommand
		{
			get
			{
				_furetherCommand = _furetherCommand ?? new MvxCommand(() =>
				{
					NavigationService.Navigate<BusinessmanBonusAccrualDetailsViewModel>();
				});
				return _furetherCommand;
			}
		}
		#endregion

		
	}
}
