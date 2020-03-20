using System;
using System.Threading.Tasks;
using bonus.app.Core.Models;
using bonus.app.Core.Services;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace bonus.app.Core.ViewModels.Businessman.BonusAccrual
{
	public class BusinessmanBonusAccrualDetailsViewModel : MvxNavigationViewModel<Guid>
	{
		private Guid _guid;
		private readonly ICustomerService _customerService;
		private readonly IServicesService _servicesServices;
		private User _user;
		private MvxObservableCollection<Service> _services;
		private Service _selectedService;
		private double? _servicePrice;
		private double _bonusesForAccrual;
		private double _bonusesForWriteOff;
		private double? _bonusAmount;
		private int? _bonusPercentage;
		private int? _bonusWhiteOffPercentage;
		private double? _bonusWhiteOffAmount;

		public BusinessmanBonusAccrualDetailsViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, ICustomerService customerService, IServicesService servicesServices)
			: base(logProvider, navigationService)
		{
			_customerService = customerService;
			_servicesServices = servicesServices;
		}

		public override async Task Initialize()
		{
			await base.Initialize();

			try
			{
				User = await _customerService.GetCustomerByUuid(_guid);
				Services = new MvxObservableCollection<Service>(await _servicesServices.GetBusinessmenService());
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}

		public Service SelectedService
		{
			get => _selectedService;
			set
			{
				SetProperty(ref _selectedService, value);

				if (ServicePrice != null)
				{
					Task.Run(() =>
					{
						UpdateBonuses(value, ServicePrice.Value);
					});
				}

				BonusAmount = null;
				BonusPercentage = null;
				BonusWhiteOffAmount = null;
				BonusWhiteOffPercentage = null;

				switch (value.AccrualMethod)
				{
					case BonusValueType.Percent:
						BonusPercentage = value.AccrualValue;
						break;
					case BonusValueType.Points:
						BonusAmount = value.AccrualFloatValue;
						break;
					default:
						throw new ArgumentOutOfRangeException(nameof(value.AccrualMethod),
															  "Метод начисления/списания бонусов должен быть либо в процентном соотношении, либо в абсолютном.");
				}

				switch (value.WhiteOffMethod)
				{
					case BonusValueType.Percent:
						BonusWhiteOffPercentage = value.WhiteOffValue;
						break;
					case BonusValueType.Points:
						BonusWhiteOffAmount = value.WhiteOffFloatValue;
						break;
					default:
						throw new ArgumentOutOfRangeException(nameof(value.WhiteOffMethod),
															  "Метод начисления/списания бонусов должен быть либо в процентном соотношении, либо в абсолютном.");
				}
			}
		}

		private void UpdateBonuses(Service selectedService, double price)
		{
			if (selectedService == null || price < 1)
			{
				BonusesForWriteOff = 0;
				BonusesForAccrual = 0;
				return;
			}

			switch (SelectedService.WhiteOffMethod)
			{
				case BonusValueType.Percent:
					BonusesForWriteOff = Math.Round(price / 100 * selectedService.WhiteOffValue, 2);
					break;
				case BonusValueType.Points:
					BonusesForWriteOff = selectedService.WhiteOffFloatValue > price ? price : selectedService.WhiteOffFloatValue;
					break;
				default: throw new ArgumentOutOfRangeException(nameof(selectedService.WhiteOffMethod), "Метод начисления/списания бонусов должен быть либо в процентном соотношении, либо в абсолютном.");
			}

			var sub = price - BonusesForWriteOff;
			if (sub < 0)
			{
				BonusesForAccrual = 0;
				return;
			}
			switch (SelectedService.AccrualMethod)
			{
				case BonusValueType.Percent:
					var maxBonuses = Math.Round(price / 100 * selectedService.AccrualValue, 2);
					BonusesForAccrual = sub > maxBonuses ? maxBonuses : sub;
					break;
				case BonusValueType.Points:
					BonusesForAccrual = sub > selectedService.AccrualFloatValue ? selectedService.AccrualFloatValue : sub;
					break;
				default: throw new ArgumentOutOfRangeException(nameof(selectedService.AccrualMethod), "Метод начисления/списания бонусов должен быть либо в процентном соотношении, либо в абсолютном.");
			}
		}

		public double? BonusAmount
		{
			get => _bonusAmount;
			set => SetProperty(ref _bonusAmount, value);
		}
		
		public double? BonusWhiteOffAmount
		{
			get => _bonusWhiteOffAmount;
			set => SetProperty(ref _bonusWhiteOffAmount, value);
		}

		public double BonusesForAccrual
		{
			get => _bonusesForAccrual;
			set => SetProperty(ref _bonusesForAccrual, value);
		}

		public double? ServicePrice
		{
			get => _servicePrice;
			set
			{
				SetProperty(ref _servicePrice, value);

				if (value != null)
				{
					Task.Run(() =>
					{
						UpdateBonuses(SelectedService, value.Value);
					});
				}
			}
		}

		public int? BonusPercentage
		{
			get => _bonusPercentage;
			set => SetProperty(ref _bonusPercentage, value);
		}

		public int? BonusWhiteOffPercentage
		{
			get => _bonusWhiteOffPercentage;
			set => SetProperty(ref _bonusWhiteOffPercentage, value);
		}

		public double BonusesForWriteOff
		{
			get => _bonusesForWriteOff;
			set => SetProperty(ref _bonusesForWriteOff, value);
		}

		public User User
		{
			get => _user;
			private set => SetProperty(ref _user, value);
		}

		public MvxObservableCollection<Service> Services
		{
			get => _services;
			private set => SetProperty(ref _services, value);
		}

		public override void Prepare(Guid parameter)
		{
			_guid = parameter;
		}
	}
}
