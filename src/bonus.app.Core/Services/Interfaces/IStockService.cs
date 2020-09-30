using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using bonus.app.Core.Models;
using bonus.app.Core.Services.Implementations;

namespace bonus.app.Core.Services.Interfaces
{
	public interface IStockService
	{
		#region Delegates and events
		event EventHandler CreatedStockEventHandler;

		event StockService.EditedStockEventHandler EditedStock;
		#endregion

		#region Overridable
		Task<bool> AddToFavorite(Guid stockUuid);

		Task<bool> RemoveFromFavorite(Guid stockUuid);

		Task<bool> CreateStock(Stock stock, byte[] imageBytes);

		Task<bool> EditStock(Stock stock, byte[] imageBytes);

		Task<IEnumerable<Stock>> All();

		Task<IEnumerable<Stock>> ArchiveStock(Guid? serviceUuid, string city);

		Task<Stock> Detail(Guid uuid);

		Task<List<Stock>> FavoriteStocks();

		Task<IEnumerable<Stock>> MyArchiveStock(Guid? serviceUuid, string city);

		Task<IEnumerable<Stock>> MyArchiveStock();

		Task<Stock> StockForEdit(Guid uuid);

		Task<IEnumerable<Stock>> MyStocks();
		#endregion
	}
}
