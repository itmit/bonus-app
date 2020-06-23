using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using bonus.app.Core.Models;
using bonus.app.Core.Models.ServiceModels;
using bonus.app.Core.Models.Statistic;

namespace bonus.app.Core.Services
{
	public interface IStatisticService
	{
		Task<List<Line>> GetProfileViewsStatistic(DateTime dateFrom, DateTime dateTo);

		Task<List<Line>> GetStocksViewsStatistic(DateTime dateFrom, DateTime dateTo);

		Task<List<Line>> GetSalesStatisticsByType(IEnumerable<Service> types, DateTime dateFrom, DateTime dateTo);

		Task<List<PiecePieChart>> GetGeographyStatistics(DateTime dateFrom, DateTime dateTo, GeographyStatisticsType statisticsType);

		Task<List<GenderColumn>> GetAgeStatistics(DateTime dateFrom, DateTime dateTo);
	}

	public enum GeographyStatisticsType
	{
		Country,
		City
	}
}
