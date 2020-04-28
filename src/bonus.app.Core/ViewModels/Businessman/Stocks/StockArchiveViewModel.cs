using System.Threading.Tasks;
using bonus.app.Core.Services;
using MvvmCross.ViewModels;

namespace bonus.app.Core.ViewModels.Businessman.Stocks
{
	public class StockArchiveViewModel : MvxViewModel
	{
		#region Data
		#region Fields
		private readonly IStockService _stockService;
		#endregion
		#endregion

		#region .ctor
		public StockArchiveViewModel(IStockService stockService) => _stockService = stockService;
		#endregion

		#region Overrided
		public override async Task Initialize()
		{
			await base.Initialize();
		}
		#endregion
	}
}
