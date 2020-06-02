using System;
using System.Threading.Tasks;
using AutoMapper;
using bonus.app.Core.Models;
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
		private readonly IServicesService _servicesService;

		public PicCountryAndCityViewModel PicCountryAndCityViewModel { get; }

		private MvxObservableCollection<Service> _services;
		private Mapper _mapper;
		private ServiceViewModel _selectedService;
		private MvxCommand _showOrHideTypesServicesCommand;
		private bool _isVisibleServices;
		private int _shapeRotation;
		private MvxObservableCollection<ServiceTypeViewModel> _servicesTypes;

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

		public MvxObservableCollection<Service> Services
		{
			get => _services;
			private set => SetProperty(ref _services, value);
		}
		#endregion

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

		public bool IsVisibleServices
		{
			get => _isVisibleServices;
			set => SetProperty(ref _isVisibleServices, value);
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
		public MvxObservableCollection<ServiceTypeViewModel> ServiceTypes
		{
			get => _servicesTypes;
			private set => SetProperty(ref _servicesTypes, value);
		}

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
	}
}
