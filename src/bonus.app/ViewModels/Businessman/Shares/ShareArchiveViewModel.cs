using System.Data;
using System.Threading.Tasks;
using bonus.app.Core.Services;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace bonus.app.Core.ViewModels.Businessman.Shares
{
	public class ShareArchiveViewModel : MvxViewModel
	{
		private readonly IStockService _stockService;

		public ShareArchiveViewModel(IStockService stockService)
		{
			_stockService = stockService;
		}

		public override async Task Initialize()
		{
			await base.Initialize();
		}
	}
}
