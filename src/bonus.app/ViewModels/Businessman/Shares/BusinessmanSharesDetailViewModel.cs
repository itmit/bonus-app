using bonus.app.Core.Models;
using bonus.app.Core.Services;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Xamarin.Forms;

namespace bonus.app.Core.ViewModels.Businessman.Shares
{
	public class BusinessmanSharesDetailViewModel : MvxViewModel<Share>
	{
		private Share _share;
		private User _user;
		private Color _shareColor;
		private IMvxNavigationService _navigationService;
		private MvxCommand _openCreateSharePageCommand;
		private MvxCommand _openCreateShareArchivePageCommand;

		public User User
		{
			get => _user;
			private set => SetProperty(ref _user, value);
		}

		public Color ShareColor
		{
			get => _shareColor;
			private set => SetProperty(ref _shareColor, value);
		}

		public BusinessmanSharesDetailViewModel(IAuthService authService, IMvxNavigationService navigationService)
		{
			User = authService.User;
			_navigationService = navigationService;
		}


		public MvxCommand OpenCreateSharePageCommand
		{
			get
			{
				_openCreateSharePageCommand = _openCreateSharePageCommand ??
											  new MvxCommand(() =>
											  {
												  _navigationService.Navigate<CreateShareViewModel>();
											  });
				return _openCreateSharePageCommand;
			}
		}

		public MvxCommand OpenCreateShareArchivePageCommand
		{
			get
			{
				_openCreateShareArchivePageCommand = _openCreateShareArchivePageCommand ??
													 new MvxCommand(() =>
													 {
														 _navigationService.Navigate<ShareArchiveViewModel>();
													 });
				return _openCreateShareArchivePageCommand;
			}
		}

		public Share Share
		{
			get => _share;
			private set => SetProperty(ref _share, value);
		}

		public override void Prepare(Share parameter)
		{
			Share = parameter;
			if (Share.Status.Equals("Завершена"))
			{
				ShareColor = Color.FromHex("#807D746D");
			}
			else if (Share.Status.Equals("Отклонена"))
			{
				ShareColor = Color.FromHex("#80BB8D91");
			}
		}
	}
}
