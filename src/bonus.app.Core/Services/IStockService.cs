using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using bonus.app.Core.Models;

namespace bonus.app.Core.Services
{
	public interface IStockService
	{
		#region Delegates and events
		event EventHandler CreatedStockEventHandler;

		event StockService.EditedStockEventHandler EditedStock;
		#endregion

		#region Overridable
		Task<bool> AddToFavorite(Guid stockUuid);

		Task<bool> CreateStock(Stock stock, byte[] imageBytes);

		Task<bool> EditStock(Stock stock, byte[] imageBytes);

		Task<IEnumerable<Stock>> GetAll();

		Task<IEnumerable<Stock>> GetArchiveStock(Guid? serviceUuid, string city);

		Task<Stock> GetDetail(Guid uuid);

		Task<List<Stock>> GetFavoriteStocks();

		Task<IEnumerable<Stock>> GetMyArchiveStock(Guid? serviceUuid, string city);

		Task<IEnumerable<Stock>> GetMyArchiveStock();

		Task<Stock> GetStockForEdit(Guid uuid);

		Task<IEnumerable<Stock>> GetMyStocks();
		#endregion
	}
}
