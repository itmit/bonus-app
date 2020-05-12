using System;
using System.Threading.Tasks;
using bonus.app.Core.Models;
using bonus.app.Core.Services;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace bonus.app.Core.ViewModels.Customer.Services
{
	public class CustomerServicesViewModel : MvxNavigationViewModel
	{
		private readonly IServicesService _servicesService;
		private MvxObservableCollection<Service> _services;

		#region .ctor
		public CustomerServicesViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IServicesService servicesService)
			: base(logProvider, navigationService)
		{
			_servicesService = servicesService;
		}

		public MvxObservableCollection<Service> Services
		{
			get => _services;
			private set => SetProperty(ref _services, value);
		}
		#endregion

		public override async Task Initialize()
		{
			await base.Initialize();

			try
			{
				Services = new MvxObservableCollection<Service>(await _servicesService.GetAllServices());
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}
	}
}
