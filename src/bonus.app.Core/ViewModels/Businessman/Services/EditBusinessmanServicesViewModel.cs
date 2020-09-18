using System;
using System.Threading.Tasks;
using bonus.app.Core.Models;
using bonus.app.Core.Models.ServiceModels;
using bonus.app.Core.Services;
using MvvmCross.Commands;
using MvvmCross.Forms.Presenters;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Xamarin.Forms;
using XF.Material.Forms.UI.Dialogs;

namespace bonus.app.Core.ViewModels.Businessman.Services
{
	public class EditBusinessmanServicesViewModel : MvxViewModel
	{
		#region Data
		#region Fields
		private MvxCommand _editServiceCommand;

		private bool _isRefreshing;
		private MvxObservableCollection<Service> _myServices;
		private readonly IMvxNavigationService _navigationService;
		private MvxCommand _refreshCommand;
		private Service _selectedService;
		private readonly IServicesService _servicesServices;
		#endregion
		#endregion

		#region .ctor
		public EditBusinessmanServicesViewModel(IMvxNavigationService navigationService, IServicesService servicesServices)
		{
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
											  await MaterialDialog.Instance.AlertAsync("Выберете услугу, которую необходимо отредактировать", "Внимание", "Ок");
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
