using bonus.app.Core.Services;
using MvvmCross.ViewModels;

namespace bonus.app.Core.ViewModels.Businessman.Shares
{
	public class CreateShareViewModel : MvxViewModel
	{
		private readonly IShareService _shareService;

		public CreateShareViewModel(IShareService shareService)
		{
			_shareService = shareService;
		}
	}
}
