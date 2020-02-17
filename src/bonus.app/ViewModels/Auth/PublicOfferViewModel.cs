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
		private readonly IMvxNavigationService _navigationService;
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
				CanRegister = value && IsCheckedPublicOffer;
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
						case UserRole.Customer:
							_navigationService.Navigate<CustomerRegistrationViewModel>();
							break;
						case UserRole.Businessman:
							_navigationService.Navigate<EntrepreneurRegistrationViewModel>();
							break;
					}
				}, () => CanRegister);
				return _openRegistrationCommand;
			}
		}

		public override void Prepare(UserRole parameter)
		{
			_userRole = parameter;
		}
	}
}
