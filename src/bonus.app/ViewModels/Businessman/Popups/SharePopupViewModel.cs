using bonus.app.Core.Models;
using bonus.app.Core.Services;
using MvvmCross.ViewModels;

namespace bonus.app.Core.ViewModels.Businessman.Popups
{
	public class SharePopupViewModel : MvxViewModel<Stock>
	{
		private Stock _stock;
		private User _user;

		public SharePopupViewModel(IAuthService authService)
		{
			User = authService.User;
		}

		public User User
		{
			get => _user;
			private set => SetProperty(ref _user, value);
		}

		public override void Prepare(Stock parameter)
		{
			Stock = parameter;
		}

		public Stock Stock
		{
			get => _stock;
			private set => SetProperty(ref _stock, value);
		}
	}
}
