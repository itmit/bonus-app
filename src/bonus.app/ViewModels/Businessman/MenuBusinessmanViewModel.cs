using System.Linq;
using bonus.app.Core.Repositories;
using bonus.app.Core.Services;
using bonus.app.Core.ViewModels.Auth;
using bonus.app.Core.ViewModels.Businessman.Pay;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace bonus.app.Core.ViewModels.Businessman
{
	public class MenuBusinessmanViewModel : MvxViewModel
	{
		private readonly IUserRepository _userRepository;
		private readonly IAuthService _authService;
		private MvxCommand _logOutCommand;
		private readonly IMvxNavigationService _navigationService;

		#region .ctor
		public MenuBusinessmanViewModel(IUserRepository userRepository, IAuthService authService, IMvxNavigationService navigationService)
		{
			_userRepository = userRepository;
			_authService = authService;
			_navigationService = navigationService;
		}
		#endregion

		public MvxCommand LogOutCommand
		{
			get
			{
				_logOutCommand = _logOutCommand ?? new MvxCommand(LogOutCommandExecute);
				return _logOutCommand;
			}
		}

		private MvxCommand _openProfileCommand;
		private MvxCommand _openPayCommand;
		private MvxCommand _openTariffCommand;
		private MvxCommand _openSupportCommand;
		private MvxCommand _openStatisticsCommand;

		public MvxCommand OpenProfileCommand
		{
			get
			{
				_openProfileCommand = _openProfileCommand ?? new MvxCommand(() =>
				{
					//_navigationService.Navigate();
				});
				return _openProfileCommand;
			}
		}

		public MvxCommand OpenPayCommand
		{
			get
			{
				_openPayCommand = _openPayCommand ?? new MvxCommand(() =>
				{
					_navigationService.Navigate<PaySubscribesViewModel>();
				});
				return _openPayCommand;
			}
		}

		public MvxCommand OpenTariffCommand
		{
			get
			{
				_openTariffCommand = _openTariffCommand ?? new MvxCommand(() =>
				{
					_navigationService.Navigate<TariffViewModel>();
				});
				return _openTariffCommand;
			}
		}

		public MvxCommand OpenSupportCommand
		{
			get
			{
				_openSupportCommand = _openSupportCommand ?? new MvxCommand(() =>
				{
					//_navigationService.Navigate();
				});
				return _openSupportCommand;
			}
		}

		public MvxCommand OpenStatisticsCommand
		{
			get
			{
				_openStatisticsCommand = _openStatisticsCommand ?? new MvxCommand(() =>
				{
					//_navigationService.Navigate();
				});
				return _openStatisticsCommand;
			}
		}

		public async void LogOutCommandExecute()
		{
			var user = _userRepository.GetAll()
									   .Single();
			
			_userRepository.Remove(user);
			await _navigationService.Navigate<AuthorizationViewModel>();
		}
	}
}
