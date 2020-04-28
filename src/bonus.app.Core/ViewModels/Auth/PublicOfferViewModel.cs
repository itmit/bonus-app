using bonus.app.Core.Models;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace bonus.app.Core.ViewModels.Auth
{
	public class PublicOfferViewModel : MvxViewModel<UserRole>
	{
		#region Data
		#region Fields
		private bool _canRegister;
		private bool _isCheckedPrivatePolicy;
		private bool _isCheckedPublicOffer;
		private readonly IMvxNavigationService _navigationService;
		private IMvxCommand _openRegistrationCommand;
		private UserRole _userRole;
		#endregion
		#endregion

		#region .ctor
		public PublicOfferViewModel(IMvxNavigationService navigationService) => _navigationService = navigationService;
		#endregion

		#region Properties
		public bool CanRegister
		{
			get => _canRegister;
			private set => SetProperty(ref _canRegister, value);
		}

		public bool IsCheckedPrivatePolicy
		{
			get => _isCheckedPrivatePolicy;
			set
			{
				SetProperty(ref _isCheckedPrivatePolicy, value);
				CanRegister = value && IsCheckedPublicOffer;
			}
		}

		public bool IsCheckedPublicOffer
		{
			get => _isCheckedPublicOffer;
			set
			{
				SetProperty(ref _isCheckedPublicOffer, value);
				CanRegister = value && IsCheckedPrivatePolicy;
			}
		}

		public IMvxCommand OpenRegistrationCommand
		{
			get
			{
				_openRegistrationCommand = _openRegistrationCommand ??
										   new MvxCommand(() =>
														  {
															  switch (_userRole)
															  {
																  case UserRole.Customer:
																	  _navigationService.Navigate<CustomerRegistrationViewModel>();
																	  break;
																  case UserRole.Businessman:
																	  _navigationService.Navigate<BusinessmanRegistrationViewModel>();
																	  break;
															  }
														  },
														  () => CanRegister);
				return _openRegistrationCommand;
			}
		}
		#endregion

		#region Overrided
		public override void Prepare(UserRole parameter)
		{
			_userRole = parameter;
		}
		#endregion
	}
}
