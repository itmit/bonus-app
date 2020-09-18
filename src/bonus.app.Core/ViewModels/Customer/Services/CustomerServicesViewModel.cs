using System;
using System.Threading.Tasks;
using AutoMapper;
using bonus.app.Core.Models;
using bonus.app.Core.Models.ServiceModels;
using bonus.app.Core.Services;
using bonus.app.Core.ViewModels.Businessman.Services;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Xamarin.Forms;

namespace bonus.app.Core.ViewModels.Customer.Services
{
	public class CustomerServicesViewModel : MvxNavigationViewModel
	{
		#region Data
		#region Fields
		private MvxObservableCollection<Service> _services;
		private readonly IServicesService _servicesService;
		private Service _selectedServiceItem;
		private MvxCommand _applyFiltersCommand;
		private MvxCommand _refreshCommand;
		private bool _isRefreshing;
		#endregion
		#endregion

		#region .ctor
		public CustomerServicesViewModel(IMvxLogProvider logProvider,
										 IMvxNavigationService navigationService,
										 IServicesService servicesService,
										 IGeoHelperService geoHelperService,
										 IAuthService authService)
			: base(logProvider, navigationService)
		{
			_servicesService = servicesService;
			PicCountryAndCityViewModel = new PicCountryAndCityViewModel(geoHelperService, this);
			MyServicesViewModel = new MyServicesViewModel(servicesService, authService)
			{
				CanAddService = false
			};
		}
		#endregion

		#region Properties
		public PicCountryAndCityViewModel PicCountryAndCityViewModel
		{
			get;
		}

		public MyServicesViewModel MyServicesViewModel
		{
			get;
		}

		public MvxObservableCollection<Service> Services
		{
			get => _services;
			private set => SetProperty(ref _services, value);
		}
		public Service SelectedServiceItem
		{
			get => _selectedServiceItem;
			set
			{
				if (value == null)
				{
					return;
				}

				SetProperty(ref _selectedServiceItem, value);
				NavigationService.Navigate<BusinessmanProfileViewModel, BusinessmanProfileViewModelArgs>(new BusinessmanProfileViewModelArgs(value.Client.Uuid, null, value.Id));
			}
		}
		#endregion


		public MvxCommand ApplyFiltersCommand
		{
			get
			{
				_applyFiltersCommand = _applyFiltersCommand ??
									   new MvxCommand(async () =>
									   {
										   try
										   {
											   var sers = await _servicesService.GetAllServices(PicCountryAndCityViewModel.SelectedCountry,
																								PicCountryAndCityViewModel.SelectedCity,
																								MyServicesViewModel.SelectedService?.Id);
											   Services = new MvxObservableCollection<Service>(sers);
										   }
										   catch (Exception e)
										   {
											   Console.WriteLine(e);
										   }
									   });
				return _applyFiltersCommand;
			}
		}

		public bool IsRefreshing
		{
			get => _isRefreshing;
			set => SetProperty(ref _isRefreshing, value);
		}

		public MvxCommand RefreshCommand
		{
			get
			{
				_refreshCommand = _refreshCommand ??
								  new MvxCommand(async () =>
								  {
									  IsRefreshing = true; 
									  try
									  {
										  var sers = await _servicesService.GetAllServices(PicCountryAndCityViewModel.SelectedCountry,
																						   PicCountryAndCityViewModel.SelectedCity,
																						   MyServicesViewModel.SelectedService?.Id);
										  Services = new MvxObservableCollection<Service>(sers);
									  }
									  catch (Exception e)
									  {
										  Console.WriteLine(e);
									  }
									  IsRefreshing = false;
								  });
				return _refreshCommand;
			}
		}

		#region Overrided
		public override async Task Initialize()
		{
			await PicCountryAndCityViewModel.Initialize();
			await MyServicesViewModel.Initialize();
			await base.Initialize();

			try
			{
				Services = new MvxObservableCollection<Service>(await _servicesService.GetAllServices());
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}
		#endregion
	}
}
