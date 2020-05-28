using System;
using System.Threading.Tasks;
using bonus.app.Core.Models;
using bonus.app.Core.Services;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace bonus.app.Core.ViewModels.Businessman.Profile
{
	public class BusinessmanSubscribersViewModel : MvxViewModel
	{
		private ISubscribeService _subscribeService;
		private IMvxNavigationService _navigationService;
		private MvxObservableCollection<Subscription> _subscriptions;
		private bool _isRefreshing;
		private MvxCommand _refreshCommand;

		#region .ctor
		public BusinessmanSubscribersViewModel(IMvxNavigationService navigationService, ISubscribeService subscribeService)
		{
			_navigationService = navigationService;
			_subscribeService = subscribeService;
		}

		public MvxObservableCollection<Subscription> Subscriptions
		{
			get => _subscriptions;
			private set => SetProperty(ref _subscriptions, value);
		}
		#endregion

		public bool IsRefreshing
		{
			get => _isRefreshing;
			set => SetProperty(ref _isRefreshing, value);
		}

		public MvxCommand RefreshCommand
		{
			get
			{
				_refreshCommand = _refreshCommand ?? new MvxCommand(async () =>
				{
					IsRefreshing = true;
					Subscriptions = new MvxObservableCollection<Subscription>(await _subscribeService.GetSubscriptions());
					IsRefreshing = false;
				});
				return _refreshCommand;
			}
		}

		public override async Task Initialize()
		{
			await base.Initialize();
			try
			{
				Subscriptions = new MvxObservableCollection<Subscription>(await _subscribeService.GetSubscriptions());

			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}
	}
}
