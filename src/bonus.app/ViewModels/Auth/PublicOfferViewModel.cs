using System.ComponentModel;
using System.Windows.Input;
using bonus.app.Core.Models;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace bonus.app.Core.ViewModels.Auth
{
	public class PublicOfferViewModel : MvxViewModel<UserRole>
	{
		private IMvxCommand _openRegistrationCommand;
		private UserRole _userRole;
		private IMvxNavigationService _navigationService;
		private bool _isCheckedPrivatePolicy;
		private bool _isCheckedPublicOffer;
		private bool _canRegister;

		public PublicOfferViewModel(IMvxNavigationService navigationService)
		{
			_navigationService = navigationService;
		}

		public bool IsCheckedPrivatePolicy
		{
			get => _isCheckedPrivatePolicy;
			set
			{
				SetProperty(ref _isCheckedPrivatePolicy, value);
				CanRegister = value && IsCheckedPrivatePolicy;
			}
		}

		public bool CanRegister
		{
			get => _canRegister;
			private set => SetProperty(ref _canRegister, value);
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
				_openRegistrationCommand = _openRegistrationCommand ?? new MvxCommand(() =>
				{
					switch (_userRole)
					{
						case UserRole.Buyer:
							_navigationService.Navigate<BuyerRegistrationViewModel>();
							break;
						case UserRole.Entrepreneur:
							_navigationService.Navigate<EntrepreneurRegistrationViewModel>();
							break;
					}
				}, CanExecuteOpenRegistrationCommand);
				return _openRegistrationCommand;
			}
		}

		private bool CanExecuteOpenRegistrationCommand()
		{
			return IsCheckedPrivatePolicy && IsCheckedPublicOffer;
		}

		public override void Prepare(UserRole parameter)
		{
			_userRole = parameter;
		}
	}
}
