using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using bonus.app.Core.Models;
using bonus.app.Core.Models.ServiceModels;
using bonus.app.Core.Models.Statistic;

namespace bonus.app.Core.Services.Interfaces
{
	public interface IStatisticService
	{
		Task<List<Line>> ProfileViewsStatistic(DateTime dateFrom, DateTime dateTo);

		Task<List<Line>> StocksViewsStatistic(DateTime dateFrom, DateTime dateTo);

		Task<List<Line>> TransitionsProfileStatistics(IEnumerable<Stock> stocks, IEnumerable<Service> services, DateTime dateFrom, DateTime dateTo);

		Task<List<Line>> SalesStatisticsByType(IEnumerable<Service> types, DateTime dateFrom, DateTime dateTo);

		Task<List<PiecePieChart>> GeographyStatistics(DateTime dateFrom, DateTime dateTo, GeographyStatisticsType statisticsType);

		Task<List<GenderColumn>> AgeStatistics(DateTime dateFrom, DateTime dateTo);
	}

	public enum GeographyStatisticsType
	{
		Country,
		City
	}
}
