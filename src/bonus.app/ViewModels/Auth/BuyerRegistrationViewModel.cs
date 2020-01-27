using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace bonus.app.Core.ViewModels.Auth
{
	public class BuyerRegistrationViewModel : BaseRegistrationViewModel
	{
		private IMvxNavigationService _navigationService;
		private IMvxCommand _openAuthVkOrFc;

		public BuyerRegistrationViewModel(IMvxNavigationService navigationService)
			=> _navigationService = navigationService;


		public IMvxCommand OpenAuthVkOrFc
		{
			get
			{
				_openAuthVkOrFc = _openAuthVkOrFc ?? new MvxCommand(() =>
				{
					_navigationService.Navigate<AuthVkFcViewModel>();
				});
				return _openAuthVkOrFc;
			}
		}

		protected override void RegistrationCommandExecute()
		{
			
		}
	}
}
