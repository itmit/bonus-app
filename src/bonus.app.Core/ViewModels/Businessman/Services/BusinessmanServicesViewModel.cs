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
		private int? _bonusAmount;
		private int? _bonusPercentage;
		private int? _cancellationBonusAmount;
		private int? _cancellationBonusPercentage;
		private bool _isRefreshing;

		private MvxObservableCollection<Service> _myServices;
		private MvxCommand _openEditCommand;
		private MvxCommand _refreshCommand;
		private readonly IServicesService _servicesServices;
		#endregion
		#endregion

		#region .ctor
		public BusinessmanServicesViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IServicesService servicesServices, IAuthService authService)
			: base(logProvider, navigationService)
		{
			_servicesServices = servicesServices;
			MyServicesViewModel = new MyServicesViewModel(servicesServices, authService);
		}
		#endregion

		#region Properties
		public MyServicesViewModel MyServicesViewModel
		{
			get;
		}

		public MvxCommand AddServiceCommand
		{
			get
			{
				_addServiceCommand = _addServiceCommand ?? new MvxCommand(AddServiceCommandExecute);
				return _addServiceCommand;
			}
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
									  MyServicesViewModel.MyServiceTypes.Clear();
									  MyServicesViewModel.Services.Clear();
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
			await MyServicesViewModel.Initialize();

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

		#region Private
		private async void AddServiceCommandExecute()
		{
			try
			{
				var service = new CreateServiceDto
				{
					Uuid = MyServicesViewModel.SelectedService.Uuid
				};
				if (BonusAmount != null && BonusAmount > 0)
				{
					service.AccrualMethod = BonusValueType.Points.ToString()
														  .ToLower();
					service.AccrualValue = BonusAmount.Value;
				}
				else if (BonusPercentage != null && BonusPercentage > 0)
				{
					service.AccrualMethod = BonusValueType.Percent.ToString()
														  .ToLower();
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
					service.WriteOffMethod = BonusValueType.Points.ToString()
														   .ToLower();
					service.WriteOffValue = CancellationBonusAmount.Value;
				}
				else if (CancellationBonusPercentage != null && CancellationBonusPercentage > 0)
				{
					service.WriteOffMethod = BonusValueType.Percent.ToString()
														   .ToLower();
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
		#endregion
	}
}
