using bonus.app.Core.Models;
using MvvmCross.ViewModels;

namespace bonus.app.Core.ViewModels.Customer.Shares
{
	public class CustomerSharesDetailViewModel : MvxViewModel<Stock>
	{
		#region Data
		#region Fields
		private Stock _stock;
		#endregion
		#endregion

		#region Properties
		public Stock Stock
		{
			get => _stock;
			private set => SetProperty(ref _stock, value);
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
