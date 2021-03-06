﻿using System;
using System.Threading.Tasks;
using bonus.app.Core.Models.Statistic;
using bonus.app.Core.Services;
using bonus.app.Core.Services.Interfaces;
using Microcharts;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace bonus.app.Core.ViewModels.Businessman.Statistics
{
	public class ViewsProfileViewModel : MvxViewModel
	{
		private readonly IStatisticService _statisticService;
		private MvxCommand _refreshCommand;
		private bool _isRefreshing;
		private DateTime? _dateFrom;
		private DateTime? _dateTo;
		private MvxObservableCollection<Line> _lines = new MvxObservableCollection<Line>();
		private MvxCommand _createChartCommand;

		public ViewsProfileViewModel(IStatisticService statisticService) => _statisticService = statisticService;

		public MvxObservableCollection<Line> Lines
		{
			get => _lines;
			private set => SetProperty(ref _lines, value);
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
						Lines = new MvxObservableCollection<Line>(await _statisticService.ProfileViewsStatistic(DateFrom.Value, DateTo.Value))
						{
							[0] =
							{
								Color = Colors.Values[0]
							},
							[1] =
							{
								Color = Colors.Values[2]
							}
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
