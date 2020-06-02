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
	public class MyServicesViewModel : MvxViewModel, IServiceParentViewModel, ICreateServiceViewModel
	{
		#region Data
		#region Fields
		private bool _isVisibleServices;
		private readonly Mapper _mapper;
		private MvxObservableCollection<CreatedServiceViewModel> _myServiceTypes = new MvxObservableCollection<CreatedServiceViewModel>();
		private ServiceViewModel _selectedService;
		private MvxObservableCollection<ServiceTypeViewModel> _services = new MvxObservableCollection<ServiceTypeViewModel>();
		private readonly IServicesService _servicesServices;
		private int _shapeRotation;
		private MvxCommand _showMyServiceTypesCommand;
		private MvxCommand _showOrHideTypesServicesCommand;
		private IAuthService _authService;
		#endregion
		#endregion

		#region .ctor
		public MyServicesViewModel(IServicesService servicesServices, IAuthService authService)
		{
			_authService = authService;
			_servicesServices = servicesServices;
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
		public ServiceType UserServiceType
		{
			get;
			set;
		}

		public MvxCommand AddServiceCommand
		{
			get
			{
				_showMyServiceTypesCommand = _showMyServiceTypesCommand ??
											 new MvxCommand(() =>
											 {
												 MyServiceTypes.Add(new CreatedServiceViewModel
												 {
													 ViewModel = this
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
		private async Task ReloadServices()
		{
			try
			{
				var types = await _servicesServices.GetMyServices();
				UserServiceType = types.SingleOrDefault(t => t.Name.Equals(_authService.User.Uuid.ToString()));
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
			await base.Initialize();
			await ReloadServices();
			if (UserServiceType != null)
			{
				foreach (var type in UserServiceType.Services.Select(service => new CreatedServiceViewModel
				{
					IsCreated = true,
					ViewModel = this,
					ServiceTypeItem = service,
					Name =
					{
						Value = service.Name
					}
				}))
				{
					MyServiceTypes.Add(type);
				}

				await RaisePropertyChanged(() => MyServiceTypes);
			}
		}
		#endregion

		public async Task<ServiceTypeItem> CreateServiceTypeItem(string name)
		{
			if (UserServiceType == null)
			{
				var type = await _servicesServices.CreateServiceType(_authService.User.Uuid.ToString());
				if (type == null)
				{
					throw new InvalidOperationException("Невозможно создать вид услуги без категории.");
				}

				type.Name = "Ваши услуги";
				UserServiceType = type;
				Services.Insert(0, new ServiceTypeViewModel
				{
					Name = type.Name,
					Uuid = type.Uuid
				});
				await RaisePropertyChanged(() => Services);
			}

			if (UserServiceType == null)
			{
				throw new InvalidOperationException("Невозможно создать вид услуги без категории.");
			}

			var item = await _servicesServices.CreateServiceTypeItem(name, UserServiceType.Uuid);
			if (item == null)
			{
				Device.BeginInvokeOnMainThread(() =>
				{
					Application.Current.MainPage.DisplayAlert("Внимание", $"Не удалось создать услугу: \"{name}\"", "Ок");
				});
			}
			else
			{
				Services.Single(t => t.Uuid.Equals(UserServiceType.Uuid)).Services.Add(new ServiceViewModel
				{
					Color = Color.Transparent,
					Name = item.Name,
					Uuid = item.Uuid,
					ParentViewModel = this
				});
				await RaisePropertyChanged(() => Services);
			}

			return item;
		}

		public async Task<bool> RemoveServiceTypeItem(Guid uuid)
		{
			bool res = false;
			try
			{
				res = await _servicesServices.RemoveServiceTypeItem(uuid);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}

			if (res)
			{
				var type = Services.Single(t => t.Uuid.Equals(UserServiceType.Uuid));
				type.Services.Remove(type.Services.Single(s => s.Uuid.Equals(uuid)));
				await RaisePropertyChanged(() => Services);
				MyServiceTypes.Remove(MyServiceTypes.Single(t => t.ServiceTypeItem.Uuid.Equals(uuid)));
				await RaisePropertyChanged(() => MyServiceTypes);
			}
			else
			{
				return false;
			}
			return true;
		}

		public async Task<bool> EditServiceTypeItem(Guid uuid, string name)
		{
			try
			{
				var res = await _servicesServices.RemoveServiceTypeItem(uuid);
				if (res)
				{
					if (UserServiceType == null)
					{
						throw new InvalidOperationException("Невозможно создать вид услуги без категории.");
					}

					var item = await _servicesServices.CreateServiceTypeItem(name, UserServiceType.Uuid);

					if (item != null)
					{
						return true;
					}

					Device.BeginInvokeOnMainThread(() =>
					{
						Application.Current.MainPage.DisplayAlert("Внимание", $"Не удалось создать услугу: \"{name}\"", "Ок");
					});
					return false;

				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}

			return false;
		}
	}
}
