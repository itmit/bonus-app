using bonus.app.Core.Models;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace bonus.app.Core.ViewModels.Businessman.Shares
{
	public class BusinessmanSharesViewModel : MvxNavigationViewModel
	{
		private MvxObservableCollection<Share> _shares;
		private Share _selectedShare;

		public BusinessmanSharesViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService)
			: base(logProvider, navigationService)
		{
		}

		public MvxObservableCollection<Share> Shares
		{
			get => _shares;
			set => SetProperty(ref _shares, value);
		}

		public Share SelectedShare
		{
			get => _selectedShare;
			set
			{
				SetProperty(ref _selectedShare, value);
				if (value != null)
				{
					NavigationService.Navigate<BusinessmanSharesDetailViewModel, Share>(value);
				}
			}
		}
	}
}
