using System;
using System.Threading.Tasks;
using bonus.app.Core.Models;
using bonus.app.Core.Services;
using MvvmCross.Commands;
using MvvmCross.Forms.Presenters;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Xamarin.Forms;

namespace bonus.app.Core.ViewModels.Businessman.Services
{
	public class EditBusinessmanServicesViewModel : MvxViewModel
	{
		#region Data
		#region Fields
		private MvxCommand _editServiceCommand;

		private Application _formsApplication;
		private bool _isRefreshing;
		private MvxObservableCollection<Service> _myServices;
		private readonly IMvxNavigationService _navigationService;
		private readonly IMvxFormsViewPresenter _platformPresenter;
		private MvxCommand _refreshCommand;
		private Service _selectedService;
		private readonly IServicesService _servicesServices;
		#endregion
		#endregion

		#region .ctor
		public EditBusinessmanServicesViewModel(IMvxNavigationService navigationService, IServicesService servicesServices, IMvxFormsViewPresenter platformPresenter)
		{
			_platformPresenter = platformPresenter;
			_navigationService = navigationService;
			_servicesServices = servicesServices;
		}
		#endregion

		#region Properties
		public MvxCommand EditServiceCommand
		{
			get
			{
				_editServiceCommand = _editServiceCommand ??
									  new MvxCommand(async () =>
									  {
										  if (SelectedService == null)
										  {
											  await FormsApplication.MainPage.DisplayAlert("Внимание", "Выберете услугу, которую необходимо отредактировать", "Ок");
											  return;
										  }

										  var service = await _navigationService.Navigate<EditBusinessmanServicesDetailsViewModel, Service, Service>(SelectedService);
										  if (service == null)
										  {
											  return;
										  }

										  RefreshCommand.Execute();
									  });
				return _editServiceCommand;
			}
		}

		public Application FormsApplication
		{
			get => _formsApplication ?? (_formsApplication = _platformPresenter.FormsApplication);
			set => _formsApplication = value;
		}

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

		public MvxCommand RefreshCommand
		{
			get
			{
				_refreshCommand = _refreshCommand ??
								  new MvxCommand(async () =>
								  {
									  SelectedService = null;
									  try
									  {
										  IsRefreshing = true;
										  MyServices = new MvxObservableCollection<Service>(await _servicesServices.GetBusinessmenService());
										  IsRefreshing = false;
									  }
									  catch (Exception e)
									  {
										  Console.WriteLine(e);
									  }
								  });
				return _refreshCommand;
			}
		}

		public Service SelectedService
		{
			get => _selectedService;
			set => SetProperty(ref _selectedService, value);
		}
		#endregion

		#region Overrided
		public override async Task Initialize()
		{
			await base.Initialize();

			try
			{
				MyServices = new MvxObservableCollection<Service>(await _servicesServices.GetBusinessmenService());
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}
		#endregion
	}
}
