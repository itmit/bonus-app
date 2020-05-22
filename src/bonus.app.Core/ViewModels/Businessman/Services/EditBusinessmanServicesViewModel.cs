using System;
using System.Threading.Tasks;
using bonus.app.Core.Models;
using bonus.app.Core.Services;
using Microsoft.AppCenter;
using MvvmCross.Commands;
using MvvmCross.Forms.Presenters;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Xamarin.Forms;

namespace bonus.app.Core.ViewModels.Businessman.Services
{
	public class EditBusinessmanServicesViewModel : MvxViewModel
	{
		private readonly IMvxNavigationService _navigationService;
		private readonly IServicesService _servicesServices;
		private MvxObservableCollection<Service> _myServices;
		private MvxCommand _editServiceCommand;
		private Service _selectedService;

		#region .ctor
		public EditBusinessmanServicesViewModel(IMvxNavigationService navigationService, IServicesService servicesServices, IMvxFormsViewPresenter platformPresenter)
		{
			_platformPresenter = platformPresenter;
			_navigationService = navigationService;
			_servicesServices = servicesServices;
		}
		#endregion

		public MvxCommand RefreshCommand
		{
			get
			{
				_refreshCommand = _refreshCommand ?? new MvxCommand(async () =>
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

		public bool IsRefreshing
		{
			get => _isRefreshing;
			set => SetProperty(ref _isRefreshing, value);
		}

		private Application _formsApplication;
		private readonly IMvxFormsViewPresenter _platformPresenter;
		private MvxCommand _refreshCommand;
		private bool _isRefreshing;

		public Application FormsApplication
		{
			get => _formsApplication ?? (_formsApplication = _platformPresenter.FormsApplication);
			set => _formsApplication = value;
		}

		public MvxCommand EditServiceCommand
		{
			get
			{
				_editServiceCommand = _editServiceCommand ?? new MvxCommand(async () =>
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

		public Service SelectedService
		{
			get => _selectedService;
			set => SetProperty(ref _selectedService, value);
		}

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

		public MvxObservableCollection<Service> MyServices
		{
			get => _myServices;
			private set => SetProperty(ref _myServices, value);
		}
		#endregion
	}
}
