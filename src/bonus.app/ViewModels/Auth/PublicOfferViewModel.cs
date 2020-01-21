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
				OpenRegistrationCommand.RaiseCanExecuteChanged();
			}
		}

		public bool IsCheckedPublicOffer
		{
			get => _isCheckedPublicOffer;
			set
			{
				SetProperty(ref _isCheckedPublicOffer, value);
				OpenRegistrationCommand.RaiseCanExecuteChanged();
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
				}, () => _isCheckedPrivatePolicy && _isCheckedPublicOffer);
				return _openRegistrationCommand;
			}
		}

		public override void Prepare(UserRole parameter)
		{
			_userRole = parameter;
		}
	}
}
