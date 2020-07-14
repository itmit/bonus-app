using System;
using System.Linq;
using System.Threading.Tasks;
using bonus.app.Core.Models;
using bonus.app.Core.Models.ServiceModels;
using bonus.app.Core.Models.UserModels;
using bonus.app.Core.Services;
using bonus.app.Core.ViewModels.Chats;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Xamarin.Essentials;

namespace bonus.app.Core.ViewModels.Customer
{
	public class BusinessmanProfileViewModel : MvxViewModel<BusinessmanProfileViewModelArgs>
	{
		#region Data
		#region Fields
		private bool _isShowedDetails;
		private bool _isSubscribe;
		private readonly IMvxNavigationService _navigationService;
		private MvxCommand _openChatCommand;
		private MvxCommand _openClassmatesCommand;
		private MvxCommand _openFacebookCommand;
		private MvxCommand _openInstagramCommand;
		private MvxCommand _openVkCommand;
		private string _photoSource;
		private MvxObservableCollection<PortfolioImage> _portfolioImages;
		private readonly IProfileService _profileService;
		private MvxObservableCollection<Service> _services;
		private readonly IServicesService _servicesService;
		private MvxCommand _showBonusDetailsCommand;
		private MvxCommand _subscribeCommand;
		private readonly ISubscribeService _subscribeService;
		private MvxCommand _unsubscribeCommand;
		private User _user;
		private BusinessmanProfileViewModelArgs _parameter;
		#endregion
		#endregion

		#region .ctor
		public BusinessmanProfileViewModel(IMvxNavigationService navigationService,
										   IProfileService profileService,
										   ISubscribeService subscribeService,
										   IServicesService servicesService)
		{
			_navigationService = navigationService;
			_profileService = profileService;
			_subscribeService = subscribeService;
			_servicesService = servicesService;
		}
		#endregion

		#region Properties
		public bool IsShowedDetails
		{
			get => _isShowedDetails;
			set => SetProperty(ref _isShowedDetails, value);
		}

		public bool IsSubscribe
		{
			get => _isSubscribe;
			private set => SetProperty(ref _isSubscribe, value);
		}

		public MvxCommand OpenChatCommand
		{
			get
			{
				_openChatCommand = _openChatCommand ??
								   new MvxCommand(() =>
								   {
									   _navigationService.Navigate<ChatViewModel, ChatViewModelArguments>(new ChatViewModelArguments(User, null));
								   });
				return _openChatCommand;
			}
		}

		public MvxCommand OpenClassmatesCommand
		{
			get
			{
				_openClassmatesCommand = _openClassmatesCommand ??
										 new MvxCommand(async () =>
										 {
											 await OpenBrowser(User.ClassmatesLink);
										 });
				return _openClassmatesCommand;
			}
		}

		public MvxCommand OpenFacebookCommand
		{
			get
			{
				_openFacebookCommand = _openFacebookCommand ??
									   new MvxCommand(async () =>
									   {
										   await OpenBrowser(User.FacebookLink);
									   });
				return _openFacebookCommand;
			}
		}

		public MvxCommand OpenInstagramCommand
		{
			get
			{
				_openInstagramCommand = _openInstagramCommand ??
										new MvxCommand(async () =>
										{
											await OpenBrowser(User.InstagramLink);
										});
				return _openInstagramCommand;
			}
		}

		public MvxCommand OpenVkCommand
		{
			get
			{
				_openVkCommand = _openVkCommand ??
								 new MvxCommand(async () =>
								 {
									 await OpenBrowser(User.VkLink);
								 });
				return _openVkCommand;
			}
		}

		public string PhotoSource
		{
			get => _photoSource;
			private set => SetProperty(ref _photoSource, value);
		}

		public MvxObservableCollection<PortfolioImage> PortfolioImages
		{
			get => _portfolioImages;
			private set => SetProperty(ref _portfolioImages, value);
		}

		public MvxObservableCollection<Service> Services
		{
			get => _services;
			private set => SetProperty(ref _services, value);
		}

		public MvxCommand ShowBonusDetailsCommand
		{
			get
			{
				_showBonusDetailsCommand = _showBonusDetailsCommand ??
										   new MvxCommand(() =>
										   {
											   IsShowedDetails = !IsShowedDetails;
										   });
				return _showBonusDetailsCommand;
			}
		}

		public MvxCommand SubscribeCommand
		{
			get
			{
				_subscribeCommand = _subscribeCommand ??
									new MvxCommand(async () =>
									{
										IsSubscribe = await _subscribeService.SubscribeToBusinessman(_parameter.Uuid);
									});
				return _subscribeCommand;
			}
		}

		public MvxCommand UnsubscribeCommand
		{
			get
			{
				_unsubscribeCommand = _unsubscribeCommand ??
									  new MvxCommand(async () =>
									  {
										  IsSubscribe = !await _subscribeService.UnsubscribeToBusinessman(_parameter.Uuid);
									  });
				return _unsubscribeCommand;
			}
		}

		public User User
		{
			get => _user;
			private set => SetProperty(ref _user, value);
		}
		#endregion

		#region Overrided
		public override async Task Initialize()
		{
			await base.Initialize();

			try
			{
				User = await _profileService.GetUser(_parameter.Uuid, _parameter.StockId, _parameter.ServiceId);
				PhotoSource = string.IsNullOrEmpty(User.PhotoSource) ? "about:blank" : User.PhotoSource;
				IsSubscribe = (await _subscribeService.GetSubscriptions()).Any(s => s.Uuid.Equals(_parameter.Uuid));
				Services = new MvxObservableCollection<Service>(await _servicesService.GetBusinessmenService(_parameter.Uuid));
				PortfolioImages = new MvxObservableCollection<PortfolioImage>(await _profileService.GetPortfolio(_parameter.Uuid));
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}

		public override void Prepare(BusinessmanProfileViewModelArgs parameter)
		{
			_parameter = parameter;
		}
		#endregion

		#region Private
		private async Task OpenBrowser(string link)
		{
			if (Uri.TryCreate(link, UriKind.Absolute, out var uriResult) && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps))
			{
				await Browser.OpenAsync(uriResult, BrowserLaunchMode.SystemPreferred);
			}
		}
		#endregion
	}

	public class BusinessmanProfileViewModelArgs
	{
		public BusinessmanProfileViewModelArgs(Guid uuid, int? stockId, int? serviceId)
		{
			Uuid = uuid;
			StockId = stockId;
			ServiceId = serviceId;
		}

		public BusinessmanProfileViewModelArgs(Guid uuid) => Uuid = uuid;

		public Guid Uuid
		{
			get;
		}

		public int? StockId
		{
			get;
		}
		public int? ServiceId
		{
			get;
		}
	}
}
