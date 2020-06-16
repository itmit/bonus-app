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
	public class CustomerServicesViewModel : MvxNavigationViewModel, IServiceParentViewModel
	{
		#region Data
		#region Fields
		private bool _isVisibleServices;
		private readonly Mapper _mapper;
		private ServiceViewModel _selectedService;

		private MvxObservableCollection<Service> _services;
		private readonly IServicesService _servicesService;
		private MvxObservableCollection<ServiceTypeViewModel> _servicesTypes;
		private int _shapeRotation;
		private MvxCommand _showOrHideTypesServicesCommand;
		#endregion
		#endregion

		#region .ctor
		public CustomerServicesViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IServicesService servicesService, IGeoHelperService geoHelperService)
			: base(logProvider, navigationService)
		{
			_servicesService = servicesService;
			PicCountryAndCityViewModel = new PicCountryAndCityViewModel(geoHelperService);
			_mapper = new Mapper(new MapperConfiguration(cfg =>
			{
				cfg.CreateMap<ServiceType, ServiceTypeViewModel>()
				   .ForMember(vm => vm.Services, m => m.MapFrom(model => model.Services));

				cfg.CreateMap<ServiceTypeItem, ServiceViewModel>()
				   .ForMember(vm => vm.ParentViewModel, m => m.MapFrom(model => this));
			}));
		}
		#endregion

		#region Properties
		public PicCountryAndCityViewModel PicCountryAndCityViewModel
		{
			get;
		}

		public bool IsVisibleServices
		{
			get => _isVisibleServices;
			set => SetProperty(ref _isVisibleServices, value);
		}

		public MvxObservableCollection<Service> Services
		{
			get => _services;
			private set => SetProperty(ref _services, value);
		}

		public MvxObservableCollection<ServiceTypeViewModel> ServiceTypes
		{
			get => _servicesTypes;
			private set => SetProperty(ref _servicesTypes, value);
		}

		public int ShapeRotation
		{
			get => _shapeRotation;
			set => SetProperty(ref _shapeRotation, value);
		}

		public MvxCommand ShowOrHideTypesServicesCommand
		{
			get
			{
				_showOrHideTypesServicesCommand = _showOrHideTypesServicesCommand ??
												  new MvxCommand(() =>
												  {
													  IsVisibleServices = !IsVisibleServices;
													  ShapeRotation = IsVisibleServices ? 180 : 0;
												  });
				return _showOrHideTypesServicesCommand;
			}
		}
		#endregion

		#region IServiceParentViewModel members
		public ServiceViewModel SelectedService
		{
			get => _selectedService;
			set
			{
				if (_selectedService != null)
				{
					_selectedService.Color = Color.Transparent;
				}

				value.Color = Color.FromHex("#BB8D91");
				SetProperty(ref _selectedService, value);
			}
		}
		#endregion

		#region Overrided
		public override async Task Initialize()
		{
			await PicCountryAndCityViewModel.Initialize();
			await base.Initialize();

			try
			{
				var typesVm = _mapper.Map<ServiceTypeViewModel[]>(await _servicesService.GetMyServices());
				ServiceTypes = new MvxObservableCollection<ServiceTypeViewModel>(typesVm);

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
