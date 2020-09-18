using System;
using System.Threading.Tasks;
using bonus.app.Core.Dtos.BusinessmanDtos;
using bonus.app.Core.Models;
using bonus.app.Core.Models.ServiceModels;
using bonus.app.Core.Services;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Xamarin.Forms;

namespace bonus.app.Core.ViewModels.Businessman.Services
{
	public class BusinessmanServicesViewModel : MvxNavigationViewModel
	{
		#region Data
		#region Fields
		private MvxCommand _addServiceCommand;
		private bool _isRefreshing;
		private MvxObservableCollection<Service> _myServices;
		private MvxCommand _refreshCommand;
		private readonly IServicesService _servicesServices;
		private MvxCommand _openEditCommand;
		#endregion
		#endregion

		#region .ctor
		public BusinessmanServicesViewModel(IMvxLogProvider logProvider
											, IMvxNavigationService navigationService
											, IServicesService servicesServices)
			: base(logProvider, navigationService)
		{
			_servicesServices = servicesServices;
			_servicesServices.MyServicesListChanged += ServicesServicesOnMyServicesListChanged;
		}

		private void ServicesServicesOnMyServicesListChanged(object sender, EventArgs e)
		{
			RefreshCommand.Execute();
		}
		#endregion

		#region Properties
		public MvxCommand AddServiceCommand
		{
			get
			{
				_addServiceCommand = _addServiceCommand ?? new MvxCommand(() =>
				{
					NavigationService.Navigate<CreateServiceStepOneViewModel>();
				});
				return _addServiceCommand;
			}
		}

		public bool HasServices => MyServices != null && MyServices.Count > 0;

		public bool IsRefreshing
		{
			get => _isRefreshing;
			set => SetProperty(ref _isRefreshing, value);
		}

		public MvxObservableCollection<Service> MyServices
		{
			get => _myServices;
			private set => SetProperty(ref _myServices, value);
		}
		public MvxCommand OpenEditCommand
		{
			get
			{
				_openEditCommand = _openEditCommand ??
								   new MvxCommand(() =>
								   {
									   NavigationService.Navigate<EditBusinessmanServicesViewModel>();
								   });
				return _openEditCommand;
			}
		}

		public MvxCommand RefreshCommand
		{
			get
			{
				_refreshCommand = _refreshCommand ??
								  new MvxCommand(async () =>
								  {
									  IsRefreshing = true;
									  await Initialize();
									  IsRefreshing = false;
								  });
				return _refreshCommand;
			}
		}
		#endregion

		#region Overrided
		public override async Task Initialize()
		{
			await base.Initialize();

			try
			{
				MyServices = new MvxObservableCollection<Service>(await _servicesServices.GetBusinessmenService());
				await RaisePropertyChanged(() => HasServices);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}
		#endregion
	}
}
