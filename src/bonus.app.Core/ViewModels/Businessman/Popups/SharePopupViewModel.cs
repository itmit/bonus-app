using bonus.app.Core.Models;
using bonus.app.Core.Models.UserModels;
using bonus.app.Core.Services;
using MvvmCross.ViewModels;

namespace bonus.app.Core.ViewModels.Businessman.Popups
{
	public class SharePopupViewModel : MvxViewModel<Stock>
	{
		#region Data
		#region Fields
		private Stock _stock;
		private User _user;
		#endregion
		#endregion

		#region .ctor
		public SharePopupViewModel(IAuthService authService) => User = authService.User;
		#endregion

		#region Properties
		public Stock Stock
		{
			get => _stock;
			private set => SetProperty(ref _stock, value);
		}

		public User User
		{
			get => _user;
			private set => SetProperty(ref _user, value);
		}
		#endregion

		#region Overrided
		public override void Prepare(Stock parameter)
		{
			Stock = parameter;
		}
		#endregion
	}
}
