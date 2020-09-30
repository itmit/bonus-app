using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bonus.app.Core.Dtos;
using bonus.app.Core.Models;
using bonus.app.Core.Models.ServiceModels;
using bonus.app.Core.Models.Statistic;
using bonus.app.Core.Services.Interfaces;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace bonus.app.Core.Services.Implementations
{
	public class StatisticService : BaseService, IStatisticService
	{
		#region Data
		#region Consts
		private const string AgeStatisticsUri = "http://bonus.itmit-studio.ru/api/statistics/getAgeStatistics?date_from={0}&date_to={1}";
		private const string GeographyStatisticsUri = "http://bonus.itmit-studio.ru/api/statistics/getGeographyStatistics?date_from={0}&date_to={1}&type={2}";
		private const string ProfileViewsStatisticUri = "http://bonus.itmit-studio.ru/api/statistics/getProfileViewsStatistics?date_from={0}&date_to={1}&profile_uuid={2}";
		private const string SalesStatisticsUri = "http://bonus.itmit-studio.ru/api/statistics/getSalesStatistics?date_from={0}&date_to={1}&service_type_ids={2}";
		private const string StockViewsStatisticsUri = "http://bonus.itmit-studio.ru/api/statistics/getStockViewsStatistics?date_from={0}&date_to={1}";
		private const string TransitionsProfileStatisticsUri =
			"http://bonus.itmit-studio.ru/api/statistics/getTransitionsProfileStatistics?date_from={0}&date_to={1}&service_type_ids={3}&stock_ids={2}";
		#endregion
		#endregion

		#region .ctor
		public StatisticService(IAuthService authService)
			: base(authService)
		{
		}
		#endregion

		#region IStatisticService members
		public async Task<List<GenderColumn>> AgeStatistics(DateTime dateFrom, DateTime dateTo)
		{
			var json = await HttpClient.GetStringAsync(string.Format(AgeStatisticsUri, dateFrom.ToString("yyyy-MM-dd"), dateTo.ToString("yyyy-MM-dd")));

			Debug.WriteLine(json);

			if (string.IsNullOrEmpty(json))
			{
				return new List<GenderColumn>();
			}

			var data = JsonConvert.DeserializeObject<ResponseDto<List<GenderColumn>>>(json);

			if (data == null)
			{
				return new List<GenderColumn>();
			}

			return data.Data ?? new List<GenderColumn>();
		}

		public async Task<List<PiecePieChart>> GeographyStatistics(DateTime dateFrom, DateTime dateTo, GeographyStatisticsType statisticsType)
		{
			var json = await HttpClient.GetStringAsync(string.Format(GeographyStatisticsUri,
																	 dateFrom.ToString("yyyy-MM-dd"),
																	 dateTo.ToString("yyyy-MM-dd"),
																	 statisticsType.ToString()
																				   .ToLower()));

			Debug.WriteLine(json);

			if (string.IsNullOrEmpty(json))
			{
				return new List<PiecePieChart>();
			}

			var data = JsonConvert.DeserializeObject<ResponseDto<List<PiecePieChart>>>(json);

			if (data == null)
			{
				return new List<PiecePieChart>();
			}

			return data.Data ?? new List<PiecePieChart>();
		}

		public async Task<List<Line>> ProfileViewsStatistic(DateTime dateFrom, DateTime dateTo)
		{
			var json = await HttpClient.GetStringAsync(string.Format(ProfileViewsStatisticUri,
																	 dateFrom.ToString("yyyy-MM-dd"),
																	 dateTo.ToString("yyyy-MM-dd"),
																	 AuthService.User.Uuid));

			Debug.WriteLine(json);

			if (string.IsNullOrEmpty(json))
			{
				return new List<Line>();
			}

			var data = JsonConvert.DeserializeObject<ResponseDto<List<Line>>>(json);

			if (data == null)
			{
				return new List<Line>();
			}

			return data.Data ?? new List<Line>();
		}

		public async Task<List<Line>> SalesStatisticsByType(IEnumerable<Service> types, DateTime dateFrom, DateTime dateTo)
		{
			var servs = new StringBuilder();
			servs.Append('[');

			foreach (var service in types)
			{
				servs.Append(service.Id);
				servs.Append(',');
			}

			servs.Remove(servs.Length - 1, 1);
			servs.Append(']');
			var url = string.Format(SalesStatisticsUri, dateFrom.ToString("yyyy-MM-dd"), dateTo.ToString("yyyy-MM-dd"), servs);
			Debug.WriteLine(url);
			var json = await HttpClient.GetStringAsync(url);

			Debug.WriteLine(json);

			if (string.IsNullOrEmpty(json))
			{
				return new List<Line>();
			}

			var data = JsonConvert.DeserializeObject<ResponseDto<List<Line>>>(json);

			if (data == null)
			{
				return new List<Line>();
			}

			for (var i = 0; i < data.Data.Count; i++)
			{
				if (i > 15)
				{
					data.Data[i]
						.Color = Color.Gray;
					continue;
				}

				data.Data[i]
					.Color = Colors.Values[i];
			}

			return data.Data ?? new List<Line>();
		}

		public async Task<List<Line>> StocksViewsStatistic(DateTime dateFrom, DateTime dateTo)
		{
			var json = await HttpClient.GetStringAsync(string.Format(StockViewsStatisticsUri, dateFrom.ToString("yyyy-MM-dd"), dateTo.ToString("yyyy-MM-dd")));
			Debug.WriteLine(json);

			if (string.IsNullOrEmpty(json))
			{
				return new List<Line>();
			}

			var data = JsonConvert.DeserializeObject<ResponseDto<List<Line>>>(json);

			if (data == null)
			{
				return new List<Line>();
			}

			return data.Data ?? new List<Line>();
		}

		public async Task<List<Line>> TransitionsProfileStatistics(IEnumerable<Stock> stocks, IEnumerable<Service> services, DateTime dateFrom, DateTime dateTo)
		{
			var serviceIds = new StringBuilder();
			var servicesList = services.ToList();
			if (servicesList.Any())
			{
				serviceIds.Append('[');
				foreach (var service in servicesList)
				{
					serviceIds.Append(service.Id);
					serviceIds.Append(',');
				}

				serviceIds.Remove(serviceIds.Length - 1, 1);
				serviceIds.Append(']');
			}

			var stockIds = new StringBuilder();
			var stockList = stocks.ToList();
			if (stockList.Any())
			{
				stockIds.Append('[');

				foreach (var service in stockList)
				{
					stockIds.Append(service.Id);
					stockIds.Append(',');
				}

				stockIds.Remove(stockIds.Length - 1, 1);
				stockIds.Append(']');
			}

			var json = await HttpClient.GetStringAsync(string.Format(TransitionsProfileStatisticsUri,
																	 dateFrom.ToString("yyyy-MM-dd"),
																	 dateTo.ToString("yyyy-MM-dd"),
																	 stockIds,
																	 serviceIds));

			Debug.WriteLine(json);

			if (string.IsNullOrEmpty(json))
			{
				return new List<Line>();
			}

			var data = JsonConvert.DeserializeObject<ResponseDto<List<Line>>>(json);

			if (data == null)
			{
				return new List<Line>();
			}

			if (data.Data == null)
			{
				return new List<Line>();
			}

			for (var i = 0; i < data.Data.Count; i++)
			{
				if (i > 15)
				{
					data.Data[i]
						.Color = Color.Gray;
					continue;
				}

				data.Data[i]
					.Color = Colors.Values[i];
			}

			return data.Data;
		}
		#endregion
	}
}
