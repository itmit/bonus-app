using System;
using System.Linq;
using System.Threading.Tasks;
using bonus.app.Core.Dto.GeoHelper;
using bonus.app.Core.Models;
using bonus.app.Core.Repositories;
using bonus.app.Core.Services;
using bonus.app.Core.ViewModels.Auth;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace bonus.app.Core.ViewModels.Profile
{
	public class EditProfileBauerViewModel : BaseEditProfileViewModel
	{
		private readonly IMvxNavigationService _navigationService;

		public void OpenAuthorization()
		{
			_navigationService.Navigate<AuthorizationViewModel>();
		}

		public EditProfileBauerViewModel(IUserRepository userRepository, IMvxNavigationService navigationService, IGeoHelperService geoHelperService)
			: base(userRepository, navigationService, geoHelperService)
		{
			_navigationService = navigationService;
		}
	}
}
