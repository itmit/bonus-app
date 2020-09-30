using System;
using System.Threading.Tasks;
using bonus.app.Core.Models.Statistic;
using bonus.app.Core.Services;
using bonus.app.Core.Services.Interfaces;
using Microcharts;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using SkiaSharp;
using SkiaSharp.Views.Forms;

namespace bonus.app.Core.ViewModels.Businessman.Statistics
{
	public class GenderAgeViewModel : MvxViewModel
	{
		private readonly IStatisticService _statisticService;
		private MvxCommand _refreshCommand;
		private bool _isRefreshing;
		private DateTime? _dateFrom;
		private DateTime? _dateTo;
		private MvxObservableCollection<BarChart> _columns = new MvxObservableCollection<BarChart>();
		private MvxCommand _createChartCommand;

		public GenderAgeViewModel(IStatisticService statisticService) => _statisticService = statisticService;

		public MvxObservableCollection<BarChart> Columns
		{
			get => _columns;
			private set => SetProperty(ref _columns, value);
		}

		public override async Task Initialize() 
		{
			await base.Initialize();
			CreateChartCommand.Execute();
		}

		public MvxCommand RefreshCommand
		{
			get
			{
				_refreshCommand = _refreshCommand ?? new MvxCommand(async () =>
				{
					IsRefreshing = true;
					await Initialize();
					IsRefreshing = false;
				});
				return _refreshCommand;
			}
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
						var result = await _statisticService.AgeStatistics(DateFrom.Value, DateTo.Value);
						Columns.Clear();
						foreach (var column in result)
						{
							var sum = column.Male + column.Female;
							Columns.Add(new BarChart
							{
								Entries = new[]
								{
									new Entry(sum > 0 ? 100 / sum * column.Male : 0)
									{
										Color = Colors.Values[0].ToSKColor()
									},
									new Entry(sum > 0 ? 100 / sum * column.Female : 0)
									{
										Color = Colors.Values[2].ToSKColor()
									}
								},
								BackgroundColor = SKColor.Empty,
								Margin = 0f,
								MaxValue = 100f,
								MinValue = 0f
							});
						}

						await RaisePropertyChanged(() => Columns);
					}
					catch (Exception e)
					{
						Console.WriteLine(e);
					}
				});
				return _createChartCommand;
			}
		}

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

	}
}
