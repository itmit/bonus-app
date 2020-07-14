using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bonus.app.Core.Models.ServiceModels;
using bonus.app.Core.Models.Statistic;
using bonus.app.Core.Services;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace bonus.app.Core.ViewModels.Businessman.Statistics
{
	public class SalesTypesViewModel : MvxViewModel
	{
		private readonly IStatisticService _statisticService;
		private readonly IServicesService _servicesService;
		private MvxObservableCollection<Service> _services;
		private MvxCommand _showOrHideTypesServicesCommand;
		private bool _isVisibleServices;
		private MvxObservableCollection<Service> _selectedItems = new MvxObservableCollection<Service>();
		private MvxObservableCollection<Line> _result  = new MvxObservableCollection<Line>();
		private MvxCommand _refreshCommand;
		private bool _isRefreshing;
		private DateTime? _dateFrom;
		private DateTime? _dateTo;

		#region .ctor
		public SalesTypesViewModel(IServicesService servicesService, IStatisticService statisticService)
		{
			_servicesService = servicesService;
			_statisticService = statisticService;
		}

		public MvxCommand ShowOrHideTypesServicesCommand
		{
			get
			{
				_showOrHideTypesServicesCommand = _showOrHideTypesServicesCommand ?? new MvxCommand(() =>
				{
					IsVisibleServices = !IsVisibleServices;
				});
				return _showOrHideTypesServicesCommand;
			}
		}

		public MvxObservableCollection<Service> SelectedItems
		{
			get => _selectedItems;
			set => SetProperty(ref _selectedItems, value);
		}

		public MvxObservableCollection<Line> Result
		{
			get => _result;
			set => SetProperty(ref _result, value);
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

		public MvxObservableCollection<Service> Services
		{
			get => _services;
			private set => SetProperty(ref _services, value);
		}
		#endregion

		public override async Task Initialize()
		{
			await base.Initialize();

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
				Services = new MvxObservableCollection<Service>(await _servicesService.GetBusinessmenService());
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}

			if (SelectedItems.Any())
			{
				try
				{
					Result = new MvxObservableCollection<Line>(await _statisticService.GetSalesStatisticsByType(SelectedItems, DateFrom.Value, DateTo.Value));
				}
				catch (Exception e)
				{
					Console.WriteLine(e);
				}
			}
		}
	}
}
