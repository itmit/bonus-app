using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bonus.app.Core.Models;
using bonus.app.Core.Models.ServiceModels;
using bonus.app.Core.Models.Statistic;
using bonus.app.Core.Services;
using bonus.app.Core.Services.Interfaces;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace bonus.app.Core.ViewModels.Businessman.Statistics
{
	public class TransitionsProfileViewModel : MvxViewModel
	{
		private readonly IStatisticService _statisticService;
		private readonly IServicesService _servicesService;
		private MvxObservableCollection<Service> _selectedItems = new MvxObservableCollection<Service>();
		private MvxObservableCollection<Line> _lines = new MvxObservableCollection<Line>();
		private DateTime? _dateFrom;
		private DateTime? _dateTo;
		private MvxObservableCollection<Service> _services;
		private readonly IStockService _stockService;
		private MvxObservableCollection<Stock> _stocks;
		private bool _isRefreshing;
		private MvxCommand _refreshCommand;
		private bool _isVisibleServices;
		private MvxObservableCollection<Stock> _selectedStocks = new MvxObservableCollection<Stock>();
		private MvxCommand _createChartCommand;

		#region .ctor
		public TransitionsProfileViewModel(IStatisticService statisticService, IServicesService servicesService, IStockService stockService)
		{
			_statisticService = statisticService;
			_servicesService = servicesService;
			_stockService = stockService;
		}
		#endregion

		public override async Task Initialize()
		{
			await base.Initialize();
			
			try
			{
				Services = new MvxObservableCollection<Service>(await _servicesService.GetBusinessmenService());
				Stocks = new MvxObservableCollection<Stock>(await _stockService.MyStocks());
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
			CreateChartCommand.Execute();
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
						Lines = new MvxObservableCollection<Line>(await _statisticService.TransitionsProfileStatistics(SelectedStocks, SelectedItems, DateFrom.Value, DateTo.Value));
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


		public bool IsVisibleServices
		{
			get => _isVisibleServices;
			set => SetProperty(ref _isVisibleServices, value);
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


		public MvxObservableCollection<Service> SelectedItems
		{
			get => _selectedItems;
			set => SetProperty(ref _selectedItems, value);
		}

		public MvxObservableCollection<Line> Lines
		{
			get => _lines;
			set => SetProperty(ref _lines, value);
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

		public MvxObservableCollection<Service> Services
		{
			get => _services;
			private set => SetProperty(ref _services, value);
		}

		public MvxObservableCollection<Stock> Stocks
		{
			get => _stocks;
			private set => SetProperty(ref _stocks, value);
		}

		public MvxObservableCollection<Stock> SelectedStocks
		{
			get => _selectedStocks;
			set => SetProperty(ref _selectedStocks, value);
		}
	}
}
