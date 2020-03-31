using bonus.app.Core.Models;
using MvvmCross.ViewModels;

namespace bonus.app.Core.ViewModels.Businessman.Shares
{
	public class BusinessmanSharesDetailViewModel : MvxViewModel<Share>
	{
		private Share _share;

		public Share Share
		{
			get => _share;
			private set => SetProperty(ref _share, value);
		}

		public override void Prepare(Share parameter)
		{
			Share = parameter;
		}
	}
}
