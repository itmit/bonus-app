using System;
using System.Globalization;
using System.Threading.Tasks;
using bonus.app.Core.Dtos.BusinessmanDtos;
using bonus.app.Core.Models;
using bonus.app.Core.Services;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace bonus.app.Core.ViewModels.Businessman.BonusAccrual
{
	public class BusinessmanBonusAccrualDetailsViewModel : MvxViewModel<Guid>
	{
		#region Data
		#region Fields
		public EventHandler AccrueAndWriteOffBonusesEventHandler;

		private MvxCommand _accrueAndWriteOffBonusesCommand;
		private string _bonusAmountString;
		private string _bonusesForAccrualString;
		private string _bonusesForWriteOffString;
		private string _bonusPercentageString;
		private readonly IBonusService _bonusService;
		private string _bonusWhiteOffAmountString;
		private string _bonusWhiteOffPercentageString;
		private readonly ICustomerService _customerService;
		private Guid _guid;
		private readonly IMvxNavigationService _navigationService;
		private Service _selectedService;
		private string _servicePriceString;
		private MvxObservableCollection<Service> _services;
		private readonly IServicesService _servicesServices;
		private User _user;
		private double _bonusAmount;
		private double _bonusesForAccrual;
		private double _bonusesForWriteOff;
		private double _bonusPercentage;
		private double _bonusWhiteOffAmount;
		private double _bonusWhiteOffPercentage;
		private double _servicePrice;
		#endregion
		#endregion

		#region .ctor
		public BusinessmanBonusAccrualDetailsViewModel(IMvxNavigationService navigationService,
													   ICustomerService customerService,
													   IServicesService servicesServices,
													   IBonusService bonusService)
		{
			_navigationService = navigationService;
			_customerService = customerService;
			_servicesServices = servicesServices;
			_bonusService = bonusService;
		}
		#endregion

		#region Properties
		public double BonusAmount
		{
			get => _bonusAmount;
			private set
			{
				_bonusAmount = value;
				_bonusAmountString = value.ToString(CultureInfo.CurrentCulture);
				RaisePropertyChanged(() => BonusAmountString);
			}
		}

		public double BonusesForAccrual
		{
			get => _bonusesForAccrual;
			private set
			{
				_bonusesForAccrual = value;
				_bonusesForAccrualString = value.ToString(CultureInfo.CurrentCulture);
				RaisePropertyChanged(() => BonusesForAccrualString);
			}
		}

		public double BonusesForWriteOff
		{
			get => _bonusesForWriteOff;
			private set
			{
				_bonusesForWriteOff = value;
				_bonusesForWriteOffString = value.ToString(CultureInfo.CurrentCulture);
				RaisePropertyChanged(() => BonusesForWriteOffString);
			}
		}

		public double BonusPercentage
		{
			get => _bonusPercentage;
			private set
			{
				_bonusPercentage = value;
				_bonusPercentageString = value.ToString(CultureInfo.CurrentCulture);
				RaisePropertyChanged(() => BonusPercentageString);
			}
		}

		public double BonusWhiteOffAmount
		{
			get => _bonusWhiteOffAmount;
			private set
			{
				_bonusWhiteOffAmount = value;
				_bonusWhiteOffAmountString = value.ToString(CultureInfo.CurrentCulture);
				RaisePropertyChanged(() => BonusWhiteOffAmountString);
			}
		}

		public double BonusWhiteOffPercentage
		{
			get => _bonusWhiteOffPercentage;
			private set
			{
				_bonusWhiteOffPercentage = value;
				_bonusWhiteOffPercentageString = value.ToString(CultureInfo.CurrentCulture);
				RaisePropertyChanged(() => BonusWhiteOffPercentageString);
			}
		}

		public MvxCommand AccrueAndWriteOffBonusesCommand
		{
			get
			{
				_accrueAndWriteOffBonusesCommand = _accrueAndWriteOffBonusesCommand ??
												   new MvxCommand(AccrueAndWriteOffBonusesCommandExecute);
				return _accrueAndWriteOffBonusesCommand;
			}
		}

		private async void AccrueAndWriteOffBonusesCommandExecute()
		{
			if (ServicePrice < 1)
			{
				return;
			}

			var res = await _bonusService.AccrueAndWriteOffBonuses(new AccrueAndWriteOffBonusesDto
			{
				AccrualMethod = SelectedService.AccrualMethod,
				AccrualValue = BonusesForAccrual,
				ClientUuid = _guid,
				Price = ServicePrice,
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
		}
		
		public string BonusAmountString
		{
			get => _bonusAmountString;
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					_bonusAmount = 0;
					SetProperty(ref _bonusAmountString, string.Empty);
					return;
				}

				if (double.TryParse(value, out var val))
				{
					_bonusAmount = val;
					SetProperty(ref _bonusAmountString, value);
					return;
				}

				RaisePropertyChanged(() => BonusAmountString);
			}
		}

		public string BonusesForAccrualString
		{
			get => _bonusesForAccrualString;
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					_bonusesForAccrual = 0;
					SetProperty(ref _bonusesForAccrualString, string.Empty);

					return;
				}

				if (double.TryParse(value, out var val))
				{
					_bonusesForAccrual = val;
					SetProperty(ref _bonusesForAccrualString, value);
					return;
				}

				RaisePropertyChanged(() => BonusesForAccrualString);
			}
		}

		public string BonusesForWriteOffString
		{
			get => _bonusesForWriteOffString;
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					_bonusesForWriteOff = 0;
					SetProperty(ref _bonusesForWriteOffString, string.Empty);
					return;
				}

				if (double.TryParse(value, out var val))
				{
					_bonusesForWriteOff = val;
					SetProperty(ref _bonusesForWriteOffString, value);
					return;
				}

				RaisePropertyChanged(() => BonusesForWriteOffString);
			}
		}

		public string BonusPercentageString
		{
			get => _bonusPercentageString;
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					_bonusPercentage = 0;
					SetProperty(ref _bonusPercentageString, string.Empty);
					return;
				}

				if (double.TryParse(value, out var val))
				{
					_bonusPercentage = val;
					SetProperty(ref _bonusPercentageString, value);
					return;
				}

				RaisePropertyChanged(() => BonusPercentageString);
			}
		}

		public string BonusWhiteOffAmountString
		{
			get => _bonusWhiteOffAmountString;
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					_bonusWhiteOffAmount = 0;
					SetProperty(ref _bonusWhiteOffAmountString, string.Empty);
					return;
				}

				if (double.TryParse(value, out var val))
				{
					_bonusWhiteOffAmount = val;
					SetProperty(ref _bonusWhiteOffAmountString, value);
					return;
				}

				RaisePropertyChanged(() => BonusWhiteOffAmountString);
			}
		}

		public string BonusWhiteOffPercentageString
		{
			get => _bonusWhiteOffPercentageString;
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					_bonusWhiteOffPercentage = 0;
					SetProperty(ref _bonusWhiteOffPercentageString, string.Empty);
					return;
				}

				if (double.TryParse(value, out var val))
				{
					_bonusWhiteOffPercentage = val;
					SetProperty(ref _bonusWhiteOffPercentageString, value);
					return;
				}

				RaisePropertyChanged(() => BonusWhiteOffPercentageString);
			}
		}
		
		public Service SelectedService
		{
			get => _selectedService;
			set
			{
				SetProperty(ref _selectedService, value);

				BonusAmount = 0;
				BonusPercentage = 0;
				BonusWhiteOffAmount = 0;
				BonusWhiteOffPercentage = 0;

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

				if (ServicePrice > 1)
				{
					Task.Run(() =>
					{
						UpdateBonuses(value, ServicePrice);
					});
				}
			}
		}

		public double ServicePrice
		{
			get => _servicePrice;
			private set
			{
				_servicePrice = value;
				ServicePriceString = value.ToString(CultureInfo.CurrentCulture);
			}
		}

		public string ServicePriceString
		{
			get => _servicePriceString;
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					_servicePrice = 0;
					SetProperty(ref _servicePriceString, value);
					return;
				}

				if (double.TryParse(value, out var val))
				{
					_servicePrice = val;
					SetProperty(ref _servicePriceString, value);
					return;
				}

				RaisePropertyChanged(() => ServicePriceString);
			}
		}

		public MvxObservableCollection<Service> Services
		{
			get => _services;
			private set => SetProperty(ref _services, value);
		}

		public User User
		{
			get => _user;
			private set => SetProperty(ref _user, value);
		}
		#endregion

		#region Public
		public void UpdateBonuses(Service selectedService, double price)
		{
			var whiteOffPercentage = BonusWhiteOffPercentage;
			var whiteOffPoints = BonusWhiteOffAmount;
			if (selectedService == null || price < 1 || whiteOffPercentage <= 0 && whiteOffPoints < 1)
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
				default:
					throw new ArgumentOutOfRangeException(nameof(selectedService.WhiteOffMethod),
														  "Метод начисления/списания бонусов должен быть либо в процентном соотношении, либо в абсолютном.");
			}

			BonusesForWriteOff = whiteOff > User.Balance ? User.Balance : whiteOff;
			var sub = price - BonusesForWriteOff;

			var bonusPercentage = BonusPercentage;
			var bonusPoints = BonusAmount;
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
				default:
					throw new ArgumentOutOfRangeException(nameof(selectedService.AccrualMethod),
														  "Метод начисления/списания бонусов должен быть либо в процентном соотношении, либо в абсолютном.");
			}
		}
		#endregion

		#region Overrided
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

		public override void Prepare(Guid parameter)
		{
			_guid = parameter;
		}
		#endregion
	}
}
