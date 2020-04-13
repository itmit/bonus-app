using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using bonus.app.Core.Models;

namespace bonus.app.Core.Services
{
	public interface IStockService
	{
		Task<IEnumerable<Stock>> GetMyStock(Guid? serviceUuid, string city);

		Task<IEnumerable<Stock>> GetArchiveStock(Guid? serviceUuid, string city);

		Task<bool> EditStock(Stock stock, byte[] imageBytes);

		Task<IEnumerable<Stock>> GetMyStock();

		Task<Stock> GetStockForEdit(Guid uuid);

		Task<IEnumerable<Stock>> GetArchiveStock();

		Task<bool> CreateStock(Stock stock, byte[] imageBytes);

		Task<IEnumerable<Stock>> GetAll();

		event EventHandler CreatedStockEventHandler;


		event StockService.EditedStockEventHandler EditedStock;
	}
}
