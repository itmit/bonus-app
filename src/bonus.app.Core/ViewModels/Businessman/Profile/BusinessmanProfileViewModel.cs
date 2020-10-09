using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bonus.app.Core.Models;
using bonus.app.Core.Models.ServiceModels;
using bonus.app.Core.Models.UserModels;
using bonus.app.Core.Services;
using bonus.app.Core.Services.Interfaces;
using bonus.app.Core.ViewModels.Businessman.Managers;
using bonus.app.Core.ViewModels.Chats;
using MvvmCross.Binding.Extensions;
using MvvmCross.Commands;
using MvvmCross.Forms.Presenters;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Xamarin.Essentials;
using Xamarin.Forms;
using XF.Material.Forms.Models;
using XF.Material.Forms.UI.Dialogs;

namespace bonus.app.Core.ViewModels.Businessman.Profile
{
	public class BusinessmanProfileViewModel : MvxNavigationViewModel
	{
		#region Data
		#region Fields
		private readonly IAuthService _authService;

		private bool _isRefreshing;
		private bool _isShowedDetails;
		private MvxCommand _openClassmatesCommand;
		private MvxCommand _openDialogsCommand;
		private MvxCommand _openEditProfilePageCommand;
		private MvxCommand _openFacebookCommand;
		private MvxCommand _openInstagramCommand;
		private MvxCommand _openSubscribersCommand;
		private MvxCommand _openVkCommand;
		private readonly IPermissionsService _permissionsService;

		private Command<MaterialMenuResult> _portfolioActionCommand;
		private MvxObservableCollection<PortfolioImage> _portfolioImages;
		private readonly IProfileService _profileService;
		private MvxCommand _refreshCommand;
		private PortfolioImage _selectedPortfolioImage;
		private bool _isDeletePortfolioImageOn;
		private IEnumerable<Service> _services;
		private readonly IServicesService _servicesService;
		private MvxCommand _showBonusDetailsCommand;
		private User _user;
		private MvxCommand _openManagersCommand;
		private bool _hasServiceInfo;
		private MvxNotifyTask _loadProfileTask;
		private string _photoSource;
		#endregion
		#endregion

		#region .ctor
		public BusinessmanProfileViewModel(IMvxLogProvider logProvider
										   , IMvxNavigationService navigationService
										   , IAuthService authService
										   , IServicesService servicesService
										   , IProfileService profileService
										   , IPermissionsService permissionsService)
			: base(logProvider, navigationService)
		{
			_servicesService = servicesService;
			_profileService = profileService;
			_permissionsService = permissionsService;
			_authService = authService;
			User = _authService.User;
			_profileService.PortfolioChanged += ProfileServiceOnPortfolioChanged;
			_profileService.UserUpdated += (sender, args) =>
			{
				LoadProfileTask = MvxNotifyTask.Create(LoadProfile);
			};
		}

		private async void ProfileServiceOnPortfolioChanged(object sender, EventArgs e)
		{
			PortfolioImages = new MvxObservableCollection<PortfolioImage>(await _profileService.Portfolio());
		}
		#endregion

		#region Properties
		public string PhotoSource
		{
			get => _photoSource;
			private set => SetProperty(ref _photoSource, value);
		}

		public bool IsRefreshing
		{
			get => _isRefreshing;
			set => SetProperty(ref _isRefreshing, value);
		}

		public bool IsShowedDetails
		{
			get => _isShowedDetails;
			set => SetProperty(ref _isShowedDetails, value);
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

		public MvxCommand OpenDialogsCommand
		{
			get
			{
				_openDialogsCommand = _openDialogsCommand ??
									  new MvxCommand(() =>
									  {
										  NavigationService.Navigate<DialogsViewModel>();
									  });
				return _openDialogsCommand;
			}
		}

		public MvxCommand OpenEditProfilePageCommand
		{
			get
			{
				_openEditProfilePageCommand = _openEditProfilePageCommand ??
											  new MvxCommand(async () =>
											  {
												  var user = await NavigationService.Navigate<EditProfileBusinessmanViewModel, EditProfileViewModelArguments, User>(
																 new EditProfileViewModelArguments(_authService.User.Uuid, true));
												  if (user != null)
												  {
													  User = user;
												  }
											  });
				return _openEditProfilePageCommand;
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

		public MvxCommand OpenSubscribersCommand
		{
			get
			{
				_openSubscribersCommand = _openSubscribersCommand ??
										  new MvxCommand(() =>
										  {
											  NavigationService.Navigate<BusinessmanSubscribersViewModel>();
										  });
				return _openSubscribersCommand;
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

		public Command<MaterialMenuResult> PortfolioActionCommand
		{
			get
			{
				_portfolioActionCommand = _portfolioActionCommand ??
										  new Command<MaterialMenuResult>(result =>
										  {
											  switch (result.Index)
											  {
												  case 0:
													  AddImageToPortfolio();
													  break;
												  case 1:
													  IsDeletePortfolioImageOn = true;
													  MaterialDialog.Instance.AlertAsync("Выберите картинку для ее удаления", "Внимание", "Ок");
													  break;
											  }
										  });
				return _portfolioActionCommand;
			}
		}

		public MvxObservableCollection<PortfolioImage> PortfolioImages
		{
			get => _portfolioImages;
			private set => SetProperty(ref _portfolioImages, value);
		}

		public MvxCommand RefreshCommand
		{
			get
			{
				_refreshCommand = _refreshCommand ??
								  new MvxCommand(() =>
								  {
									  IsRefreshing = true;
									  LoadProfileTask = MvxNotifyTask.Create(LoadProfile);
									  IsRefreshing = false;
								  });
				return _refreshCommand;
			}
		}

		public PortfolioImage SelectedPortfolioImage
		{
			get => _selectedPortfolioImage;
			set
			{
				if (value == null)
				{
					return;
				}
				SetProperty(ref _selectedPortfolioImage, value);
				if (IsDeletePortfolioImageOn)
				{
					RemovePortfolioImage(value);
					IsDeletePortfolioImageOn = false;
				}
				else
				{
					NavigationService.Navigate<PhotoViewModel, string>(value.ImageSource);
				}
			}
		}

		public bool IsDeletePortfolioImageOn
		{
			get => _isDeletePortfolioImageOn;
			set => SetProperty(ref _isDeletePortfolioImageOn, value);
		}

		public IEnumerable<Service> Services
		{
			get => _services;
			private set => SetProperty(ref _services, value);
		}

		public bool HasServiceInfo
		{
			get => _hasServiceInfo;
			set => SetProperty(ref _hasServiceInfo, value);
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

		public User User
		{
			get => _user;
			private set
			{
				SetProperty(ref _user, value);

				if (string.IsNullOrEmpty(value.PhotoSource))
				{
					PhotoSource = "about:blank";
				}
				else if (!value.PhotoSource.Equals(PhotoSource))
				{
					PhotoSource = value.PhotoSource;
				}
			}
		}
		#endregion

		#region Public
		public async void RemovePortfolioImage(PortfolioImage portfolioImage)
		{
			var confirm = await MaterialDialog.Instance.ConfirmAsync("Вы действительно хотите удалите картинку из портфолио?", "Подтвердите", "Да", "Нет");

			if (confirm == null || !confirm.Value)
			{
				return;
			}

			try
			{
				if (!await _profileService.RemoveImageFromPortfolio(portfolioImage.Uuid))
				{
					return;
				}

				PortfolioImages.Remove(portfolioImage);
				await RaisePropertyChanged(() => PortfolioImages);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}
		#endregion

		#region Overrided
		public override async Task Initialize()
		{
			LoadProfileTask = MvxNotifyTask.Create(LoadProfile);

			await base.Initialize();
		}
		#endregion

		public MvxCommand OpenManagersCommand
		{
			get
			{
				_openManagersCommand = _openManagersCommand ??
									   new MvxCommand(() =>
									   {
										   NavigationService.Navigate<ManagersViewModel>();
									   });
				return _openManagersCommand;
			}
		}

		public MvxNotifyTask LoadProfileTask
		{
			get => _loadProfileTask;
			private set => SetProperty(ref _loadProfileTask, value);
		}

		#region Private
		private async Task LoadProfile()
		{
			User = await _profileService.User();

			Services = await _servicesService.GetBusinessmenService();
			HasServiceInfo = Services != null && Services.Any();

			PortfolioImages = new MvxObservableCollection<PortfolioImage>(await _profileService.Portfolio());
		}

		private async void AddImageToPortfolio()
		{
			if (!await _permissionsService.RequestPermissionAsync<StoragePermission>(Permission.Storage, "Для загрузки аватара необходимо разрешение на использование хранилища."))
			{
				return;
			}

			if (!CrossMedia.Current.IsPickPhotoSupported)
			{
				return;
			}

			var image = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
			{
				PhotoSize = PhotoSize.Medium
			});

			if (image == null)
			{
				return;
			}

			PortfolioImages.Add(new PortfolioImage
			{
				ImageSource = image.Path
			});
			await RaisePropertyChanged(() => PortfolioImages);

			await _profileService.AddImageToPortfolio(image.Path);
		}

		private static async Task OpenBrowser(string link)
		{
			if (Uri.TryCreate(link, UriKind.Absolute, out var uriResult) && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps))
			{
				await Browser.OpenAsync(uriResult, BrowserLaunchMode.SystemPreferred);
			}
		}
		#endregion
	}
}
