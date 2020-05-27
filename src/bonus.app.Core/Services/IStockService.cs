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
		Task<bool> CreateStock(Stock stock, byte[] imageBytes);

		Task<bool> EditStock(Stock stock, byte[] imageBytes);

		Task<IEnumerable<Stock>> GetAll();

		Task<IEnumerable<Stock>> GetArchiveStock(Guid? serviceUuid, string city);

		Task<IEnumerable<Stock>> GetArchiveStock();

		Task<IEnumerable<Stock>> GetMyStock(Guid? serviceUuid, string city);

		Task<IEnumerable<Stock>> GetMyStock();

		Task<Stock> GetStockForEdit(Guid uuid);

		Task<bool> AddToFavorite(Guid stockUuid);

		Task<List<Stock>> GetFavoriteStocks();
		#endregion
	}
}
