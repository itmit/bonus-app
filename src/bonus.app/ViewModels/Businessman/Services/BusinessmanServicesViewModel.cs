using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using bonus.app.Core.Dtos.BusinessmanDtos;
using bonus.app.Core.Models;
using bonus.app.Core.Services;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Xamarin.Forms;

namespace bonus.app.Core.ViewModels.Businessman.Services
{
	public class BusinessmanServicesViewModel : MvxNavigationViewModel, IServiceParentViewModel
	{
		private readonly IServicesService _servicesServices;
		private MvxObservableCollection<ServiceTypeViewModel> _services;
		private MvxObservableCollection<Service> _myServices;
		private int? _bonusAmount;
		private int? _bonusPercentage;
		private int? _cancellationBonusAmount;
		private int? _cancellationBonusPercentage;
		private MvxCommand _addServiceCommand;
		private readonly Mapper _mapper;
		private ServiceViewModel _selectedService;

		#region .ctor
		public BusinessmanServicesViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IServicesService servicesServices)
			: base(logProvider, navigationService)
		{
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

		public override async Task Initialize()
		{
			await base.Initialize();
			try
			{
				var typesVm = _mapper.Map<ServiceTypeViewModel[]>(await _servicesServices.GetAll());
				Services = new MvxObservableCollection<ServiceTypeViewModel>(typesVm);
				MyServices = new MvxObservableCollection<Service>(await _servicesServices.GetBusinessmenService());
				await RaisePropertyChanged(() => HasServices);
				await RaisePropertyChanged(() => NoHasServices);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}

		public bool HasServices => MyServices != null && MyServices.Count > 0;

		public bool NoHasServices => !HasServices;

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
				_selectedService = value;
			}
		}

		public MvxObservableCollection<ServiceTypeViewModel> Services
		{
			get => _services;
			private set => SetProperty(ref _services, value);
		}

		public MvxObservableCollection<Service> MyServices
		{
			get => _myServices;
			private set => SetProperty(ref _myServices, value);
		}

		public int? BonusAmount
		{
			get => _bonusAmount;
			set => SetProperty(ref _bonusAmount, value);
		}

		public int? BonusPercentage
		{
			get => _bonusPercentage;
			set => SetProperty(ref _bonusPercentage, value);
		}

		public int? CancellationBonusAmount
		{
			get => _cancellationBonusAmount;
			set => SetProperty(ref _cancellationBonusAmount, value);
		}

		public int? CancellationBonusPercentage
		{
			get => _cancellationBonusPercentage;
			set => SetProperty(ref _cancellationBonusPercentage, value);
		}

		public MvxCommand AddServiceCommand 
		{ 
			get
			{
				_addServiceCommand = _addServiceCommand ?? new MvxCommand(AddServiceCommandExecute);
				return _addServiceCommand;
			}
		}

		private async void AddServiceCommandExecute()
		{
			try
			{
				var service = new CreateServiceDto
				{
					Uuid = SelectedService.Uuid
				};
				if (BonusAmount != null && BonusAmount > 0)
				{
					service.AccrualMethod = BonusValueType.Points.ToString().ToLower();
					service.AccrualValue = BonusAmount.Value;
				} 
				else if (BonusPercentage != null && BonusPercentage > 0)
				{
					service.AccrualMethod = BonusValueType.Percent.ToString().ToLower();
					service.AccrualValue = BonusPercentage.Value;
				}
				else
				{
					Device.BeginInvokeOnMainThread(() =>
					{
						Application.Current.MainPage.DisplayAlert("Внимание", "Укажите количество или процент начисляемых бонусов.", "Ок");
					});
					return;
				}
				
				if (CancellationBonusAmount != null && CancellationBonusAmount > 0)
				{
					service.WriteOffMethod = BonusValueType.Points.ToString().ToLower();
					service.WriteOffValue = CancellationBonusAmount.Value;
				} 
				else if (CancellationBonusPercentage != null && CancellationBonusPercentage > 0)
				{
					service.WriteOffMethod = BonusValueType.Percent.ToString().ToLower();
					service.WriteOffValue = CancellationBonusPercentage.Value;
				}
				else
				{
					Device.BeginInvokeOnMainThread(() =>
					{
						Application.Current.MainPage.DisplayAlert("Внимание", "Укажите количество или процент списываемых бонусов.", "Ок");
					});
					return;
				}
				var result = await _servicesServices.CreateService(service);

				if (result)
				{
					Device.BeginInvokeOnMainThread(() =>
					{
						Application.Current.MainPage.DisplayAlert("Внимание", "Услуга создана!", "Ок");
					});

					try
					{
						MyServices = new MvxObservableCollection<Service>(await _servicesServices.GetBusinessmenService());
						await RaisePropertyChanged(() => HasServices);
						await RaisePropertyChanged(() => NoHasServices);
					}
					catch (Exception e)
					{
						Console.WriteLine(e);
					}
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}
	}
}
