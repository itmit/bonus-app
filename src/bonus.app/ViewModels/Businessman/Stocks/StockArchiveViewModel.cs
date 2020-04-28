using System.Threading.Tasks;
using bonus.app.Core.Services;
using MvvmCross.ViewModels;

namespace bonus.app.Core.ViewModels.Businessman.Stocks
{
	public class StockArchiveViewModel : MvxViewModel
	{
		private readonly IStockService _stockService;

		public StockArchiveViewModel(IStockService stockService)
		{
			_stockService = stockService;
		}

		public override async Task Initialize()
		{
			await base.Initialize();
		}
	}
}
