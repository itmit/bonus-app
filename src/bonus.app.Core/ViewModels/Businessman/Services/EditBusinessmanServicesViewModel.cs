using System;
using System.Threading.Tasks;
using bonus.app.Core.Models;
using bonus.app.Core.Services;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace bonus.app.Core.ViewModels.Businessman.Services
{
	public class EditBusinessmanServicesViewModel : MvxViewModel
	{
		private readonly IMvxNavigationService _navigationService;
		private readonly IServicesService _servicesServices;
		private MvxObservableCollection<Service> _myServices;

		#region .ctor
		public EditBusinessmanServicesViewModel(IMvxNavigationService navigationService, IServicesService servicesServices)
		{
			_navigationService = navigationService;
			_servicesServices = servicesServices;
		}
		#endregion

		public bool HasServices => MyServices != null && MyServices.Count > 0;
		
		#region Overrided
		public override async Task Initialize()
		{
			await base.Initialize();

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

		public MvxObservableCollection<Service> MyServices
		{
			get => _myServices;
			private set => SetProperty(ref _myServices, value);
		}
		#endregion
	}
}
