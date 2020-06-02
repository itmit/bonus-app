using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using bonus.app.Core.Models;
using bonus.app.Core.Services;
using bonus.app.Core.ViewModels.Chats;
using MvvmCross.Commands;
using MvvmCross.Forms.Presenters;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.Permissions.Abstractions;
using Xamarin.Essentials;
using Xamarin.Forms;
using XF.Material.Forms.Models;

namespace bonus.app.Core.ViewModels.Businessman.Profile
{
	public class BusinessmanProfileViewModel : MvxNavigationViewModel
	{
		#region Data
		#region Fields
		private readonly IAuthService _authService;
		private MvxCommand _openDialogsCommand;
		private MvxCommand _openEditProfilePageCommand;
		private MvxCommand _openSubscribersCommand;
		private IEnumerable<Service> _services;
		private readonly IServicesService _servicesService;
		private User _user;
		private readonly IProfileService _profileService;
		private MvxObservableCollection<PortfolioImage> _portfolioImages;
		private MvxCommand _openVkCommand;
		private MvxCommand _openClassmatesCommand;
		private MvxCommand _openFacebookCommand;
		private MvxCommand _openInstagramCommand;
		private MvxCommand _showBonusDetailsCommand;
		private bool _isShowedDetails;
		private Command<MaterialMenuResult> _portfolioActionCommand;
		private readonly IPermissionsService _permissionsService;
		private PortfolioImage _selectedPortfolioImage;
		private MvxCommand _refreshCommand;
		private bool _isRefreshing;
		private SelectionMode _selectionModePortfolio;
		#endregion
		#endregion

		#region .ctor
		public BusinessmanProfileViewModel(IMvxLogProvider logProvider,
										   IMvxNavigationService navigationService,
										   IAuthService authService,
										   IServicesService servicesService,
										   IProfileService profileService,
										   IPermissionsService permissionsService,
										   IMvxFormsViewPresenter platformPresenter)
			: base(logProvider, navigationService)
		{
			_servicesService = servicesService;
			_profileService = profileService;
			_permissionsService = permissionsService;
			_platformPresenter = platformPresenter;
			_authService = authService;
			User = _authService.User;
			PhotoSource = string.IsNullOrEmpty(User.PhotoSource) ? "about:blank" : User.PhotoSource;
		}

		public string PhotoSource
		{
			get;
		}
		#endregion

		#region Properties
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
								  new MvxCommand(async () =>
								  {
									  IsRefreshing = true;
									  await Initialize();
									  IsRefreshing = false;
								  });
				return _refreshCommand;
			}
		}

		public bool IsRefreshing
		{
			get => _isRefreshing;
			set => SetProperty(ref _isRefreshing, value);
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
													  SelectionModePortfolio = SelectionMode.Single;
													  FormsApplication.MainPage.DisplayAlert("Внимание", "Выберите картинку для ее удаления." ,"Ок");
													  break;
											  }
										  });
				return _portfolioActionCommand;
			}
		}

		private readonly IMvxFormsViewPresenter _platformPresenter;

		private Application _formsApplication;
		public Application FormsApplication
		{
			get => _formsApplication ?? (_formsApplication = _platformPresenter.FormsApplication);
			set => _formsApplication = value;
		}
		public SelectionMode SelectionModePortfolio
		{
			get => _selectionModePortfolio;
			set => SetProperty(ref _selectionModePortfolio, value);
		}

		public PortfolioImage SelectedPortfolioImage
		{
			get => _selectedPortfolioImage;
			set
			{
				SetProperty(ref _selectedPortfolioImage, value);
				RemovePortfolioImage(value);
				SelectionModePortfolio = SelectionMode.None;
			}
		}

		public async void RemovePortfolioImage(PortfolioImage portfolioImage)
		{
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

		private async void AddImageToPortfolio()
		{
			if (await _permissionsService.CheckPermission(Permission.Storage,
														 "Для загрузки аватара необходимо разрешение на использование хранилища."))
			{
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

				var portfolioImage = await _profileService.AddImageToPortfolio(image.Path);

				if (portfolioImage == null)
				{
					return;
				}

				PortfolioImages.Add(portfolioImage);
				await RaisePropertyChanged(() => PortfolioImages);
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
												  if (user == null)
												  {
													  return;
												  }

												  User = user;
											  });
				return _openEditProfilePageCommand;
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

		public IEnumerable<Service> Services
		{
			get => _services;
			private set => SetProperty(ref _services, value);
		}

		public User User
		{
			get => _user;
			private set => SetProperty(ref _user, value);
		}

		public MvxCommand OpenVkCommand
		{
			get
			{
				_openVkCommand = _openVkCommand ?? new MvxCommand(async () =>
				{
					await OpenBrowser(User.VkLink);
				});
				return _openVkCommand;
			}
		}

		public MvxCommand OpenInstagramCommand
		{
			get
			{
				_openInstagramCommand = _openInstagramCommand ?? new MvxCommand(async () =>
				{
					await OpenBrowser(User.InstagramLink);
				});
				return _openInstagramCommand;
			}
		}

		public MvxCommand ShowBonusDetailsCommand
		{
			get
			{
				_showBonusDetailsCommand = _showBonusDetailsCommand ?? new MvxCommand(() =>
				{
					IsShowedDetails = !IsShowedDetails;
				});
				return _showBonusDetailsCommand;
			}
		}

		public bool IsShowedDetails
		{
			get => _isShowedDetails;
			set => SetProperty(ref _isShowedDetails, value);
		}

		public MvxCommand OpenFacebookCommand
		{
			get
			{
				_openFacebookCommand = _openFacebookCommand ?? new MvxCommand(async () =>
				{
					await OpenBrowser(User.FacebookLink);
				});
				return _openFacebookCommand;
			}
		}
		public MvxCommand OpenClassmatesCommand
		{
			get
			{
				_openClassmatesCommand = _openClassmatesCommand ?? new MvxCommand(async () =>
				{
					await OpenBrowser(User.ClassmatesLink);
				});
				return _openClassmatesCommand;
			}
		}

		private async Task OpenBrowser(string link)
		{
			if (Uri.TryCreate(link, UriKind.Absolute, out var uriResult) && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps))
			{
				await Browser.OpenAsync(uriResult, BrowserLaunchMode.SystemPreferred);
			}
		}
		#endregion

		#region Overrided
		public override async Task Initialize()
		{
			await base.Initialize();

			try
			{
				Services = await _servicesService.GetBusinessmenService();
				PortfolioImages = new MvxObservableCollection<PortfolioImage>(await _profileService.GetPortfolio());
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}
		#endregion
	}
}
