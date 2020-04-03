using bonus.app.Core.Models;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace bonus.app.Core.ViewModels.Customer.Shares
{
	public class CustomerSharesDetailViewModel : MvxViewModel<Share>
	{
		private Share _share;

		public override void Prepare(Share parameter)
		{
			Share = parameter;
		}

		public Share Share
		{
			get => _share;
			private set => SetProperty(ref _share, value);
		}

	}
}
