using System;
using System.Threading.Tasks;
using bonus.app.Core.Models;
using bonus.app.Core.Services;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace bonus.app.Core.ViewModels.Customer.Profile
{
	public class CustomerSubscribersViewModel : MvxViewModel
	{
		#region Data
		#region Fields
		private bool _isRefreshing;
		private readonly IMvxNavigationService _navigationService;
		private MvxCommand _refreshCommand;
		private Subscription _selectedSubscription;
		private readonly ISubscribeService _subscribeService;
		private MvxObservableCollection<Subscription> _subscriptions;
		#endregion
		#endregion

		#region .ctor
		public CustomerSubscribersViewModel(IMvxNavigationService navigationService, ISubscribeService subscribeService)
		{
			_navigationService = navigationService;
			_subscribeService = subscribeService;
		}
		#endregion

		#region Properties
		public bool IsRefreshing
		{
			get => _isRefreshing;
			set => SetProperty(ref _isRefreshing, value);
		}

		public MvxCommand RefreshCommand
		{
			get
			{
				_refreshCommand = _refreshCommand ??
								  new MvxCommand(async () =>
								  {
									  IsRefreshing = true;
									  Subscriptions = new MvxObservableCollection<Subscription>(await _subscribeService.GetSubscriptions());
									  IsRefreshing = false;
								  });
				return _refreshCommand;
			}
		}

		public Subscription SelectedSubscription
		{
			get => _selectedSubscription;
			set
			{
				if (value == null)
				{
					return;
				}

				SetProperty(ref _selectedSubscription, value);
				_navigationService.Navigate<BusinessmanProfileViewModel, Guid>(value.Uuid);
			}
		}

		public MvxObservableCollection<Subscription> Subscriptions
		{
			get => _subscriptions;
			private set => SetProperty(ref _subscriptions, value);
		}
		#endregion

		#region Overrided
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
		#endregion
	}
}
