using System.Collections.Generic;
using System.Threading.Tasks;
using bonus.app.Core.Models;
using bonus.app.Core.Models.Statistic;

namespace bonus.app.Core.Services
{
	public interface IStatisticService
	{
		Task<List<Line>> GetProfileViewsStatistic();

		Task<List<Line>> GetStocksViewsStatistic();

		Task<List<Line>> GetSalesStatisticsByType(IEnumerable<ServiceType> types);


	}
}
