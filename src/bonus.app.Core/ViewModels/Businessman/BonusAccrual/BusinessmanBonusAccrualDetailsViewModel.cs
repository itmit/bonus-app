using System;
using System.Globalization;
using System.Threading.Tasks;
using bonus.app.Core.Dtos.BusinessmanDtos;
using bonus.app.Core.Models;
using bonus.app.Core.Models.ServiceModels;
using bonus.app.Core.Models.UserModels;
using bonus.app.Core.Services;
using bonus.app.Core.Services.Interfaces;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace bonus.app.Core.ViewModels.Businessman.BonusAccrual
{
	public class BusinessmanBonusAccrualDetailsViewModel : MvxViewModel<User>
	{
		#region Data
		#region Fields
		public EventHandler AccrueAndWriteOffBonusesEventHandler;

		private MvxCommand _accrueAndWriteOffBonusesCommand;
		private double _accrueBonuses;
		private string _accrueBonusesString;
		private double _bonusAmount;
		private string _bonusAmountString;
		private double _bonusesForAccrual;
		private string _bonusesForAccrualString;
		private double _bonusesForWriteOff;
		private string _bonusesForWriteOffString;
		private double _bonusPercentage;
		private string _bonusPercentageString;
		private readonly IBonusService _bonusService;
		private double _bonusWhiteOffAmount;
		private string _bonusWhiteOffAmountString;
		private double _bonusWhiteOffPercentage;
		private string _bonusWhiteOffPercentageString;
		private readonly ICustomerService _customerService;
		private Guid _guid;
		private readonly IMvxNavigationService _navigationService;
		private MvxCommand _openClientProfileCommand;
		private readonly IProfileService _profileService;
		private Service _selectedService;
		private double _servicePrice;
		private string _servicePriceString;
		private MvxObservableCollection<Service> _services;
		private readonly IServicesService _servicesServices;
		private User _user;
		#endregion
		#endregion

		#region .ctor
		public BusinessmanBonusAccrualDetailsViewModel(IMvxNavigationService navigationService,
													   ICustomerService customerService,
													   IServicesService servicesServices,
													   IBonusService bonusService,
													   IProfileService profileService)
		{
			_navigationService = navigationService;
			_customerService = customerService;
			_servicesServices = servicesServices;
			_bonusService = bonusService;
			_profileService = profileService;
		}
		#endregion

		#region Properties
		public MvxCommand AccrueAndWriteOffBonusesCommand
		{
			get
			{
				_accrueAndWriteOffBonusesCommand = _accrueAndWriteOffBonusesCommand ?? new MvxCommand(AccrueAndWriteOffBonusesCommandExecute);
				return _accrueAndWriteOffBonusesCommand;
			}
		}

		public double AccrueBonuses
		{
			get => _accrueBonuses;
			private set
			{
				if (value.Equals(_accrueBonuses))
				{
					return;
				}

				_accrueBonuses = value;
				_accrueBonusesString = value.ToString(CultureInfo.CurrentCulture);
				RaisePropertyChanged(() => AccrueBonuses);
			}
		}

		public string AccrueBonusesString
		{
			get => _accrueBonusesString;
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					_accrueBonuses = 0;
					SetProperty(ref _accrueBonusesString, string.Empty);

					return;
				}

				if (double.TryParse(value, out var val))
				{
					_accrueBonuses = val;
					SetProperty(ref _accrueBonusesString,
								Math.Round(val, 2)
									.ToString(CultureInfo.InvariantCulture));
					return;
				}

				RaisePropertyChanged(() => AccrueBonusesString);
			}
		}

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
					SetProperty(ref _bonusAmountString,
								Math.Round(val, 2)
									.ToString(CultureInfo.InvariantCulture));
					return;
				}

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
					SetProperty(ref _bonusesForAccrualString,
								Math.Round(val, 2)
									.ToString(CultureInfo.InvariantCulture));
					return;
				}

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
					SetProperty(ref _bonusesForWriteOffString,
								Math.Round(val, 2)
									.ToString(CultureInfo.InvariantCulture));
					return;
				}

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
					SetProperty(ref _bonusPercentageString,
								Math.Round(val, 2)
									.ToString(CultureInfo.InvariantCulture));
					return;
				}

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
					SetProperty(ref _bonusWhiteOffAmountString,
								Math.Round(val, 2)
									.ToString(CultureInfo.InvariantCulture));
					return;
				}

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
					SetProperty(ref _bonusWhiteOffPercentageString,
								Math.Round(val, 2)
									.ToString(CultureInfo.InvariantCulture));
					return;
				}

				RaisePropertyChanged(() => BonusWhiteOffPercentageString);
			}
		}

		public MvxCommand OpenClientProfileCommand
		{
			get
			{
				_openClientProfileCommand = _openClientProfileCommand ??
											new MvxCommand(async () =>
											{
												await _navigationService.Navigate<ClientProfileViewModel, User>(await _profileService.User(_guid));
											});
				return _openClientProfileCommand;
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
					SetProperty(ref _servicePriceString,
								Math.Round(val, 2)
									.ToString(CultureInfo.InvariantCulture));
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
		public void UpdateAccrueBonuses(Service selectedService)
		{
			if (selectedService == null)
			{
				AccrueBonuses = 0;
				return;
			}

			switch (selectedService.AccrualMethod)
			{
				case BonusValueType.Percent:
					var maxBonuses = Math.Round(BonusesForAccrual / 100 * BonusPercentage, 2);
					AccrueBonuses = BonusesForAccrual > maxBonuses ? maxBonuses : BonusesForAccrual;
					break;
				case BonusValueType.Points:
					AccrueBonuses = BonusesForAccrual > BonusAmount ? BonusAmount : BonusesForAccrual;
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(selectedService.AccrualMethod),
														  "Метод начисления/списания бонусов должен быть либо в процентном соотношении, либо в абсолютном.");
			}
		}

		public void UpdateBonuses(Service selectedService, double price)
		{
			var whiteOffPercentage = BonusWhiteOffPercentage;
			var whiteOffPoints = BonusWhiteOffAmount;
			if (selectedService == null || price < 1 || whiteOffPercentage <= 0 && whiteOffPoints < 1)
			{
				BonusesForWriteOff = 0;
				BonusesForAccrual = 0;
				AccrueBonuses = 0;
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

			UpdateBonusesForAccrual(selectedService, price);
		}

		public void UpdateBonusesForAccrual(Service selectedService, double price)
		{
			if (selectedService == null || price < 1)
			{
				BonusesForAccrual = 0;
				AccrueBonuses = 0;
				return;
			}

			var sub = price - BonusesForWriteOff;

			if (sub < 0)
			{
				BonusesForAccrual = 0;
				return;
			}

			BonusesForAccrual = sub;

			UpdateAccrueBonuses(selectedService);
		}
		#endregion

		#region Overrided
		public override async Task Initialize()
		{
			await base.Initialize();

			try
			{
				Services = new MvxObservableCollection<Service>(await _servicesServices.GetBusinessmenService());
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}

		public override void Prepare(User parameter)
		{
			User = parameter;
			if (string.IsNullOrEmpty(parameter.PhotoSource))
			{
				parameter.PhotoSource = "about:blank";
			}

			_guid = parameter.Uuid;
		}
		#endregion

		#region Private
		private async void AccrueAndWriteOffBonusesCommandExecute()
		{
			if (ServicePrice < 1)
			{
				return;
			}

			var res = await _bonusService.AccrueAndWriteOffBonuses(new AccrueAndWriteOffBonusesDto
			{
				AccrualMethod = SelectedService.AccrualMethod,
				AccrualValue = AccrueBonuses,
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
		#endregion
	}
}
