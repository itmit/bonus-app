﻿using System.Linq;
using bonus.app.Core.Repositories;
using bonus.app.Core.Services;
using bonus.app.Core.ViewModels.Auth;
using bonus.app.Core.ViewModels.Businessman.News;
using bonus.app.Core.ViewModels.Businessman.Pay;
using bonus.app.Core.ViewModels.Businessman.Statistics;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace bonus.app.Core.ViewModels.Businessman
{
	public class MenuBusinessmanViewModel : MvxViewModel
	{
		private readonly IAuthService _authService;
		private MvxCommand _logOutCommand;
		private readonly IMvxNavigationService _navigationService;

		#region .ctor
		public MenuBusinessmanViewModel(IAuthService authService, IMvxNavigationService navigationService)
		{
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
					//_navigationService.Navigate<>();
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
					_navigationService.Navigate<ChatViewModel>();
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
					_navigationService.Navigate<StatisticsViewModel>();
				});
				return _openStatisticsCommand;
			}
		}

		public async void LogOutCommandExecute()
		{
			await _authService.LogOut(_authService.User);
			await _navigationService.Navigate<AuthorizationViewModel>();
		}
	}
}
