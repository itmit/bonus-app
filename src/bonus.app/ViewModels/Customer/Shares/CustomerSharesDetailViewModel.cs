using bonus.app.Core.Models;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace bonus.app.Core.ViewModels.Customer.Shares
{
	public class CustomerSharesDetailViewModel : MvxViewModel<Stock>
	{
		private Stock _stock;

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
