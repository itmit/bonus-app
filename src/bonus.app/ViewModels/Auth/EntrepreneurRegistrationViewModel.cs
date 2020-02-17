using System;
using System.Threading.Tasks;
using bonus.app.Core.Models;
using bonus.app.Core.Repositories;
using bonus.app.Core.Services;
using bonus.app.Core.ViewModels.Profile;
using MvvmCross.Navigation;

namespace bonus.app.Core.ViewModels.Auth
{
	public class EntrepreneurRegistrationViewModel : BaseRegistrationViewModel
	{
		private readonly IMvxNavigationService _navigationService;

		public EntrepreneurRegistrationViewModel(IMvxNavigationService navigationService)
		{
			_navigationService = navigationService;
		}

		protected override Task<bool> RegistrationCommandExecute()
		{
			return Task.FromResult(true);
			// _navigationService.Navigate<EditProfileBusinessmanViewModel, EditProfileViewModelArguments>(new EditProfileViewModelArguments(user.Guid, password));
		}
	}
}
