using System;
using System.Linq;
using System.Threading.Tasks;
using bonus.app.Core.Models;
using bonus.app.Core.Models.ServiceModels;
using bonus.app.Core.Services;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using Xamarin.Forms;

namespace bonus.app.Core.ViewModels.Businessman.Services
{
	public class CreateServiceViewModel : MvxViewModel, ICreateServiceViewModel
	{
		#region Data
		#region Fields
		private readonly IAuthService _authService;
		private MvxObservableCollection<CreatedServiceViewModel> _myServiceTypes = new MvxObservableCollection<CreatedServiceViewModel>();
		private readonly IServicesService _servicesServices;
		private MvxCommand _showMyServiceTypesCommand;
		#endregion
		#endregion

		#region .ctor
		public CreateServiceViewModel(IServicesService servicesServices, IAuthService authService)
		{
			_authService = authService;
			_servicesServices = servicesServices;
		}
		#endregion

		#region Properties
		public ServiceType UserServiceType
		{
			get;
			set;
		}

		public MvxCommand AddServiceCommand
		{
			get
			{
				_showMyServiceTypesCommand = _showMyServiceTypesCommand ??
											 new MvxCommand(() =>
											 {
												 MyServiceTypes.Add(new CreatedServiceViewModel
												 {
													 ViewModel = this
												 });
												 RaisePropertyChanged(() => MyServiceTypes);
											 });
				return _showMyServiceTypesCommand;
			}
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
				var types = await _servicesServices.GetMyServices();
				UserServiceType = types.SingleOrDefault(t => t.Name.Equals(_authService.User.Uuid.ToString()));
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

		#region ICreateServiceViewModel members
		public async Task<ServiceTypeItem> CreateServiceTypeItem(string name)
		{
			if (UserServiceType == null)
			{
				var type = await _servicesServices.CreateServiceType(_authService.User.Uuid.ToString());
				if (type == null)
				{
					throw new InvalidOperationException("Невозможно создать вид услуги без категории.");
				}

				type.Name = "Ваши услуги";
				UserServiceType = type;
			}

			if (UserServiceType == null)
			{
				throw new InvalidOperationException("Невозможно создать вид услуги без категории.");
			}

			var item = await _servicesServices.CreateServiceTypeItem(name, UserServiceType.Uuid);
			if (item == null)
			{
				Device.BeginInvokeOnMainThread(() =>
				{
					Application.Current.MainPage.DisplayAlert("Внимание", $"Не удалось создать услугу: \"{name}\"", "Ок");
				});
			}

			return item;
		}

		public async Task<bool> EditServiceTypeItem(Guid uuid, string name)
		{
			try
			{
				var res = await _servicesServices.RemoveServiceTypeItem(uuid);
				if (res)
				{
					if (UserServiceType == null)
					{
						throw new InvalidOperationException("Невозможно создать вид услуги без категории.");
					}

					var item = await _servicesServices.CreateServiceTypeItem(name, UserServiceType.Uuid);

					if (item != null)
					{
						return true;
					}

					Device.BeginInvokeOnMainThread(() =>
					{
						Application.Current.MainPage.DisplayAlert("Внимание", $"Не удалось создать услугу: \"{name}\"", "Ок");
					});
					return false;
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}

			return false;
		}

		public async Task<bool> RemoveServiceTypeItem(Guid uuid)
		{
			var res = false;
			try
			{
				res = await _servicesServices.RemoveServiceTypeItem(uuid);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}

			if (res)
			{
				MyServiceTypes.Remove(MyServiceTypes.Single(t => t.ServiceTypeItem.Uuid.Equals(uuid)));
				await RaisePropertyChanged(() => MyServiceTypes);
			}
			else
			{
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
					var type = new CreatedServiceViewModel
					{
						IsCreated = true,
						ViewModel = this,
						ServiceTypeItem = service,
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
