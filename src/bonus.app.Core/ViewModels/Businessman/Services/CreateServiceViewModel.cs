using System;
using System.Linq;
using System.Threading.Tasks;
using bonus.app.Core.Models;
using bonus.app.Core.Services;
using MvvmCross.Commands;
using MvvmCross.ViewModels;

namespace bonus.app.Core.ViewModels.Businessman.Services
{
	public class CreateServiceViewModel : MvxViewModel, ICreatedServiceParentViewModel
	{
		#region Data
		#region Fields
		private MvxObservableCollection<CreatedServiceViewModel> _myServiceTypes = new MvxObservableCollection<CreatedServiceViewModel>();
		private readonly IServicesService _servicesServices;
		private MvxCommand _showMyServiceTypesCommand;
		#endregion
		#endregion

		#region .ctor
		public CreateServiceViewModel(IServicesService servicesServices, IAuthService authService)
		{
			UserUuid = authService.User.Uuid;
			_servicesServices = servicesServices;
		}
		#endregion

		#region Properties
		public ServiceType UserServiceType
		{
			get;
			private set;
		}


		public MvxCommand AddServiceCommand
		{
			get
			{
				_showMyServiceTypesCommand = _showMyServiceTypesCommand ??
											 new MvxCommand(() =>
											 {
												 MyServiceTypes.Add(new CreatedServiceViewModel(_servicesServices)
												 {
													 ParentViewModel = this
												 });
												 RaisePropertyChanged(() => MyServiceTypes);
											 });
				return _showMyServiceTypesCommand;
			}
		}


		public Guid UserUuid
		{
			get;
		}

		public MvxObservableCollection<CreatedServiceViewModel> MyServiceTypes
		{
			get => _myServiceTypes;
			set => SetProperty(ref _myServiceTypes, value);
		}
		#endregion

		#region Public
		public async Task<bool> ReloadServices()
		{
			try
			{
				var types = await _servicesServices.GetAll();
				UserServiceType = types.SingleOrDefault(t => t.Name.Equals(UserUuid.ToString()));
				if (UserServiceType != null)
				{
					UserServiceType.Name = "Ваши услуги";
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				return false;
			}

			return true;
		}
		#endregion

		#region Overrided
		public override async Task Initialize()
		{
			await base.Initialize();
			await ReloadServices();
			if (UserServiceType != null)
			{
				foreach (var service in UserServiceType.Services)
				{
					var type = new CreatedServiceViewModel(_servicesServices)
					{
						IsCreated = true,
						ParentViewModel = this,
						Name =
						{
							Value = service.Name
						}
					};
					MyServiceTypes.Add(type);
				}
			}
		}
		#endregion
	}
}
