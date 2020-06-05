using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using bonus.app.Core.Models;
using bonus.app.Core.Services;
using bonus.app.Core.ViewModels.Businessman.Services;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using Xamarin.Forms;

namespace bonus.app.Core.ViewModels
{
	public class PicServiceTypeViewModel : MvxViewModel, IServiceParentViewModel
	{
		private ServiceViewModel _selectedService;
		private MvxCommand _showOrHideTypesServicesCommand;
		private int _shapeRotation;
		private bool _isVisibleServices;
		private MvxObservableCollection<ServiceTypeViewModel> _services;
		private readonly IServicesService _servicesServices;
		private readonly IAuthService _authService;
		private readonly Mapper _mapper;

		public PicServiceTypeViewModel(IServicesService servicesServices, IAuthService authService)
		{
			_servicesServices = servicesServices;
			_authService = authService;
			_mapper = new Mapper(new MapperConfiguration(cfg =>
			{
				cfg.CreateMap<ServiceType, ServiceTypeViewModel>()
				   .ForMember(vm => vm.Services, m => m.MapFrom(model => model.Services));

				cfg.CreateMap<ServiceTypeItem, ServiceViewModel>()
				   .ForMember(vm => vm.ParentViewModel, m => m.MapFrom(model => this));
			}));
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


		public MvxObservableCollection<ServiceTypeViewModel> Services
		{
			get => _services;
			private set => SetProperty(ref _services, value);
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


		public override async Task Initialize()
		{
			await base.Initialize();
			var types = await _servicesServices.GetMyServices();
			var userServiceType = types.SingleOrDefault(t => t.Name.Equals(_authService.User.Uuid.ToString()));
			if (userServiceType != null)
			{
				userServiceType.Name = "Ваши услуги";
			}

			var typesVm = _mapper.Map<ServiceTypeViewModel[]>(types);
			Services = new MvxObservableCollection<ServiceTypeViewModel>(typesVm);
		}

		public Task<ServiceTypeItem> CreateServiceTypeItem(string name) => throw new NotImplementedException();

		public Task<bool> EditServiceTypeItem(Guid uuid, string name) => throw new NotImplementedException();

		public Task<bool> RemoveServiceTypeItem(Guid uuid) => throw new NotImplementedException();
	}
}
