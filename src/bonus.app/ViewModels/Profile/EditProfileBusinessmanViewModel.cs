using bonus.app.Core.Repositories;
using bonus.app.Core.Services;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace bonus.app.Core.ViewModels.Profile
{
	public class EditProfileBusinessmanViewModel : BaseEditProfileViewModel
	{
		public EditProfileBusinessmanViewModel(IUserRepository userRepository, IMvxNavigationService navigationService, IGeoHelperService geoHelperService)
			: base(userRepository, navigationService, geoHelperService)
		{
		}

		public override void Prepare(EditProfileViewModelArguments parameter)
		{
			
		}
	}
}
