using System;
using System.Threading.Tasks;
using bonus.app.Core.Dtos.BusinessmanDtos;
using bonus.app.Core.Models;
using bonus.app.Core.Services;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace bonus.app.Core.ViewModels.Businessman.BonusAccrual
{
	public class BusinessmanBonusAccrualDetailsViewModel : MvxViewModel<Guid>
	{
		private Guid _guid;
        private readonly IMvxNavigationService _navigationService;
        private readonly ICustomerService _customerService;
		private readonly IServicesService _servicesServices;
		private User _user;
		private MvxObservableCollection<Service> _services;
		private Service _selectedService;
		private double? _servicePrice;
		private double _bonusesForAccrual;
		private double _bonusesForWriteOff;
		private IBonusService _bonusService;
		private double? _bonusAmount;
		private double? _bonusPercentage;
		private double? _bonusWhiteOffPercentage;
		private double? _bonusWhiteOffAmount;
		private MvxCommand _accrueAndWriteOffBonusesCommand;

		public BusinessmanBonusAccrualDetailsViewModel(IMvxNavigationService navigationService, ICustomerService customerService, IServicesService servicesServices, IBonusService bonusService)
		{
            _navigationService = navigationService;
            _customerService = customerService;
			_servicesServices = servicesServices;
			_bonusService = bonusService;
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

				if (ServicePrice != null)
				{
					Task.Run(() =>
					{
						UpdateBonuses(value, ServicePrice.Value);
					});
				}
			}
		}

		public void UpdateBonuses(Service selectedService, double price)
		{
			var whiteOffPercentage = BonusWhiteOffPercentage ?? 0;
			var whiteOffPoints = BonusWhiteOffAmount ?? 0;
			if (selectedService == null
				|| price < 1
				|| whiteOffPercentage < 1)
			{
				BonusesForWriteOff = 0;
				BonusesForAccrual = 0;
				return;
			}

			double whiteOff;
			switch (selectedService.WhiteOffMethod)
			{
				case BonusValueType.Percent:
					whiteOff = Math.Round(price / 100 * whiteOffPercentage, 2);
					break;
				case BonusValueType.Points:
					whiteOff = whiteOffPoints > price ? price : whiteOffPoints;
					break;
				default: throw new ArgumentOutOfRangeException(nameof(selectedService.WhiteOffMethod), "Метод начисления/списания бонусов должен быть либо в процентном соотношении, либо в абсолютном.");
			}

			BonusesForWriteOff = whiteOff > User.Balance ? User.Balance : whiteOff;
			var sub = price - BonusesForWriteOff;

			var bonusPercentage = BonusPercentage ?? 0;
			var bonusPoints = BonusAmount ?? 0;
			if (sub < 0)
			{
				BonusesForAccrual = 0;
				return;
			}
			switch (selectedService.AccrualMethod)
			{
				case BonusValueType.Percent:
					var maxBonuses = Math.Round(price / 100 * bonusPercentage, 2);
					BonusesForAccrual = sub > maxBonuses ? maxBonuses : sub;
					break;
				case BonusValueType.Points:
					BonusesForAccrual = sub > bonusPoints ? bonusPoints : sub;
					break;
				default: throw new ArgumentOutOfRangeException(nameof(selectedService.AccrualMethod), "Метод начисления/списания бонусов должен быть либо в процентном соотношении, либо в абсолютном.");
			}

			BonusUpdated?.Invoke(this, EventArgs.Empty);
		}

		public EventHandler BonusUpdated;

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
			set => SetProperty(ref _servicePrice, value);
		}

		public double? BonusPercentage
		{
			get => _bonusPercentage;
			set => SetProperty(ref _bonusPercentage, value);
		}

		public double? BonusWhiteOffPercentage
		{
			get => _bonusWhiteOffPercentage;
			set => SetProperty(ref _bonusWhiteOffPercentage, value);
		}

		public double BonusesForWriteOff
		{
			get => _bonusesForWriteOff;
			set => SetProperty(ref _bonusesForWriteOff, value);
		}

        public EventHandler AccrueAndWriteOffBonusesEventHandler;

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

		public MvxCommand AccrueAndWriteOffBonusesCommand
		{
			get
			{
				_accrueAndWriteOffBonusesCommand = _accrueAndWriteOffBonusesCommand ?? new MvxCommand(async () =>
				{
					if (ServicePrice == null)
					{
						return;
					}
                    var res = await _bonusService.AccrueAndWriteOffBonuses(new AccrueAndWriteOffBonusesDto
                    {
                        AccrualMethod = SelectedService.AccrualMethod,
                        AccrualValue = BonusesForAccrual,
                        ClientUuid = _guid,
                        Price = ServicePrice.Value,
                        ServiceUuid = SelectedService.Uuid,
                        WriteOffMethod = SelectedService.WhiteOffMethod,
                        WriteOffValue = BonusesForWriteOff
                    });

                    if (res)
                    {
                        if (await _navigationService.Close(this))
                        {
                            AccrueAndWriteOffBonusesEventHandler?.Invoke(this, EventArgs.Empty);
                        }
                    }

				});
				return _accrueAndWriteOffBonusesCommand;
			}
		}
	}
}
