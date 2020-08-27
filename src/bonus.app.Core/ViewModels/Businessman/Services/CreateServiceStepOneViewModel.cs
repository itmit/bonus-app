using System;
using System.Threading.Tasks;
using bonus.app.Core.Services;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using XF.Material.Forms.UI.Dialogs;

namespace bonus.app.Core.ViewModels.Businessman.Services
{
	public class CreateServiceStepOneViewModel : MvxViewModel
	{
		private MyServicesViewModel _myServicesContentViewModel;
		private MvxCommand _openTwoStepCommand;
		private readonly IMvxNavigationService _navigationService;

		public CreateServiceStepOneViewModel(IMvxNavigationService navigationService, IServicesService servicesService, IAuthService authService)
		{
			MyServicesContentViewModel = new MyServicesViewModel(servicesService, authService);
			_navigationService = navigationService;
		}

		public MyServicesViewModel MyServicesContentViewModel
		{
			get => _myServicesContentViewModel;
			set => SetProperty(ref _myServicesContentViewModel, value);
		}

		public MvxCommand OpenTwoStepCommand
		{
			get
			{
				_openTwoStepCommand = _openTwoStepCommand ?? new MvxCommand(() =>
				{
					if (MyServicesContentViewModel.SelectedService == null)
					{
						MaterialDialog.Instance.AlertAsync("Выберите услугу", "Внимание", "Ок");
						return;
					}

					_navigationService.Navigate<CreateServiceStepTwoViewModel, CreateServiceStepTwoViewModel.CreateServiceStepTwoViewModelArgs>(new CreateServiceStepTwoViewModel.CreateServiceStepTwoViewModelArgs(MyServicesContentViewModel.SelectedService.Uuid, this));
				});
				return _openTwoStepCommand;
			}
		}

		public override async Task Initialize() 
		{
			await base.Initialize();
			await MyServicesContentViewModel.Initialize();
			MyServicesContentViewModel.IsVisibleServices = true;
		}
	}
}
