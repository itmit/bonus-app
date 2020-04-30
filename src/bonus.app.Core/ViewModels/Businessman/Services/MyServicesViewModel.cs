using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using bonus.app.Core.Models;
using bonus.app.Core.Services;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using Xamarin.Forms;

namespace bonus.app.Core.ViewModels.Businessman.Services
{
	public class MyServicesViewModel : MvxViewModel, IServiceParentViewModel, ICreatedServiceParentViewModel
	{
		#region Data
		#region Fields
		private bool _isVisibleServices;
		private readonly Mapper _mapper;
		private MvxObservableCollection<CreatedServiceViewModel> _myServiceTypes = new MvxObservableCollection<CreatedServiceViewModel>();
		private ServiceViewModel _selectedService;
		private MvxObservableCollection<ServiceTypeViewModel> _services;
		private readonly IServicesService _servicesServices;
		private int _shapeRotation;
		private MvxCommand _showMyServiceTypesCommand;
		private MvxCommand _showOrHideTypesServicesCommand;
		#endregion
		#endregion

		#region .ctor
		public MyServicesViewModel(IServicesService servicesServices, IAuthService authService)
		{
			UserUuid = authService.User.Uuid;
			_servicesServices = servicesServices;
			_mapper = new Mapper(new MapperConfiguration(cfg =>
			{
				cfg.CreateMap<ServiceType, ServiceTypeViewModel>()
				   .ForMember(vm => vm.Services, m => m.MapFrom(model => model.Services));

				cfg.CreateMap<Service, ServiceViewModel>()
				   .ForMember(vm => vm.ParentViewModel, m => m.MapFrom(model => this));
			}));
		}
		#endregion

		#region Properties
		public ServiceType UserServiceType
		{
			get;
			private set;
		}

		public Guid UserUuid
		{
			get;
		}

		public MvxCommand AddServiceCommand
		{
			get
			{
				_showMyServiceTypesCommand = _showMyServiceTypesCommand ??
											 new MvxCommand(() =>
											 {
												 MyServiceTypes.Add(new CreatedServiceViewModel(_servicesServices)
												 {
													 ParentViewModel = this
												 });
												 RaisePropertyChanged(() => MyServiceTypes);
											 });
				return _showMyServiceTypesCommand;
			}
		}

		public bool IsVisibleServices
		{
			get => _isVisibleServices;
			set => SetProperty(ref _isVisibleServices, value);
		}

		public MvxObservableCollection<CreatedServiceViewModel> MyServiceTypes
		{
			get => _myServiceTypes;
			set => SetProperty(ref _myServiceTypes, value);
		}

		public MvxObservableCollection<ServiceTypeViewModel> Services
		{
			get => _services;
			private set => SetProperty(ref _services, value);
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

		#region Public
		public async Task<bool> ReloadServices()
		{
			try
			{
				var types = await _servicesServices.GetAll();
				UserServiceType = types.SingleOrDefault(t => t.Name.Equals(UserUuid.ToString()));
				if (UserServiceType != null)
				{
					UserServiceType.Name = "Ваши услуги";
				}

				var typesVm = _mapper.Map<ServiceTypeViewModel[]>(types);
				Services = new MvxObservableCollection<ServiceTypeViewModel>(typesVm);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				return false;
			}

			return true;
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
			await base.Initialize();
			await ReloadServices();
			if (UserServiceType != null)
			{
				foreach (var service in UserServiceType.Services)
				{
					var type = new CreatedServiceViewModel(_servicesServices)
					{
						IsCreated = true,
						ParentViewModel = this,
						Name =
						{
							Value = service.Name
						}
					};
					MyServiceTypes.Add(type);
				}
			}
		}
		#endregion
	}
}
