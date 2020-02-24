using System;
using System.Threading.Tasks;
using bonus.app.Core.Models;
using bonus.app.Core.Services;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace bonus.app.Core.ViewModels.Businessman.Services
{
	public class BusinessmanServicesViewModel : MvxNavigationViewModel
	{
		private readonly IServicesService _servicesServices;
		private MvxObservableCollection<ServiceType> _services;

		#region .ctor
		public BusinessmanServicesViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IServicesService servicesServices)
			: base(logProvider, navigationService)
		{
			_servicesServices = servicesServices;
		}
		#endregion

		public override async Task Initialize()
		{
			await base.Initialize();
			try
			{
				Services = new MvxObservableCollection<ServiceType>(await _servicesServices.GetAll());
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}

		public MvxObservableCollection<ServiceType> Services
		{
			get => _services;
			set => SetProperty(ref _services, value);
		}
	}
}
