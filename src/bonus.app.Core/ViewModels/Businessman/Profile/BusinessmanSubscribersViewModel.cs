using System;
using System.Linq;
using System.Threading.Tasks;
using bonus.app.Core.Models;
using bonus.app.Core.Models.UserModels;
using bonus.app.Core.Services;
using bonus.app.Core.Services.Interfaces;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace bonus.app.Core.ViewModels.Businessman.Profile
{
	public class BusinessmanSubscribersViewModel : MvxViewModel
	{
		#region Data
		#region Fields
		private bool _isRefreshing;
		private readonly IMvxNavigationService _navigationService;
		private readonly IProfileService _profileService;
		private MvxCommand _refreshCommand;
		private MvxCommand _searchCommand;
		private string _searchQuery;
		private Subscription _selectedSubscription;
		private readonly ISubscribeService _subscribeService;
		private MvxObservableCollection<Subscription> _subscriptions;
		#endregion
		#endregion

		#region .ctor
		public BusinessmanSubscribersViewModel(IMvxNavigationService navigationService, ISubscribeService subscribeService, IProfileService profileService)
		{
			_navigationService = navigationService;
			_subscribeService = subscribeService;
			_profileService = profileService;
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
									  await Initialize();
									  IsRefreshing = false;
								  });
				return _refreshCommand;
			}
		}

		public MvxCommand SearchCommand
		{
			get
			{
				_searchCommand = _searchCommand ??
								 new MvxCommand(async () =>
								 {
									 var subscriptions = await _subscribeService.GetSubscriptions();
									 if (string.IsNullOrWhiteSpace(SearchQuery))
									 {
										 Subscriptions = new MvxObservableCollection<Subscription>(subscriptions);
										 return;
									 }

									 Subscriptions = new MvxObservableCollection<Subscription>(subscriptions.Where(sub => sub.Login.Contains(SearchQuery)));
								 });
				return _searchCommand;
			}
		}

		public string SearchQuery
		{
			get => _searchQuery;
			set => SetProperty(ref _searchQuery, value);
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
				Task.Run(async () =>
				{
					await _navigationService.Navigate<ClientProfileViewModel, User>(await _profileService.User(value.Uuid));
				});
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
				SearchCommand.Execute();
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}
		#endregion
	}
}
