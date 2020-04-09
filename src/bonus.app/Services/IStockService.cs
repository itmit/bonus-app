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
		Task<IEnumerable<Stock>> GetMyStock();

		Task<IEnumerable<Stock>> GetArchiveStock();

		Task<bool> CreateStock(Stock stock, byte[] imageBytes);

		Task<IEnumerable<Stock>> GetAll();

		event EventHandler CreatedShareEventHandler;
	}
}
