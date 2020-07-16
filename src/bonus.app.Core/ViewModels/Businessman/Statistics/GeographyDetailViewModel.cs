using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bonus.app.Core.Models.Statistic;
using bonus.app.Core.Services;
using Microcharts;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using SkiaSharp;
using SkiaSharp.Views.Forms;

namespace bonus.app.Core.ViewModels.Businessman.Statistics
{
	public class GeographyDetailViewModel : MvxViewModel<GeographyStatisticsType>
	{
		private readonly IStatisticService _statisticService;
		private GeographyStatisticsType _statisticsType;
		private MvxObservableCollection<PiecePieChart> _result;
		private DonutChart _donutChart;
		private MvxCommand _refreshCommand;
		private bool _isRefreshing;
		private DateTime? _dateTo;
		private DateTime? _dateFrom;
		private MvxCommand _createChartCommand;

		public MvxObservableCollection<PiecePieChart> Result
		{
			get => _result;
			private set => SetProperty(ref _result, value);
		}

		public GeographyDetailViewModel(IStatisticService statisticService) => _statisticService = statisticService;

		public MvxCommand RefreshCommand { get
		{
			_refreshCommand = _refreshCommand ?? new MvxCommand(async () =>
			{
				IsRefreshing = true;
				await Initialize();
				IsRefreshing = false;
			});
			return _refreshCommand;
		} }

		public bool IsRefreshing
		{
			get => _isRefreshing;
			private set => SetProperty(ref _isRefreshing, value);
		}

		public DateTime? DateFrom
		{
			get => _dateFrom;
			set => SetProperty(ref _dateFrom, value);
		}

		public DateTime? DateTo
		{
			get => _dateTo;
			set => SetProperty(ref _dateTo, value);
		}


		public MvxCommand CreateChartCommand
		{
			get
			{
				_createChartCommand = _createChartCommand ?? new MvxCommand(async () => {

					if (DateTo == null)
					{
						DateTo = DateTime.Now + new TimeSpan(1, 0, 0, 0);
					}

					if (DateFrom == null)
					{
						DateFrom = DateTo.Value - new TimeSpan(30, 0, 0, 0);
					}

					try
					{
						Result = new MvxObservableCollection<PiecePieChart>(await _statisticService.GetGeographyStatistics(DateFrom.Value, DateTo.Value, StatisticsType));
						for (var i = 0; i < Result.Count; i++)
						{
							Result[i]
								.Color = Colors.Values[i];
						}
						var entries = Result.Select(piecePieChart => new Entry(piecePieChart.Percent)
						{
							Color = piecePieChart.Color.ToSKColor()
						});
						DonutChart = new DonutChart
						{
							MaxValue = 100f,
							MinValue = 0f,
							Entries = entries,
							HoleRadius = 0.3f,
							BackgroundColor = SKColor.Empty
						};
					}
					catch (Exception e)
					{
						Console.WriteLine(e);
					}
				});
				return _createChartCommand;
			}
		}

		public override async Task Initialize() 
		{
			await base.Initialize();

			CreateChartCommand.Execute();
		}

		public DonutChart DonutChart
		{
			get => _donutChart;
			private set => SetProperty(ref _donutChart, value);
		}

		public override void Prepare(GeographyStatisticsType parameter)
		{
			StatisticsType = parameter;
		}

		public GeographyStatisticsType StatisticsType
		{
			get => _statisticsType;
			private set => SetProperty(ref _statisticsType, value);
		}
	}
}
