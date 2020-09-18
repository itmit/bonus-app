using System.Linq;
using System.Threading.Tasks;
using bonus.app.Core.Dtos.BusinessmanDtos;
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
	public class EditBusinessmanServicesDetailsViewModel : MvxViewModel<Service, Service>
	{
		#region Data
		#region Fields
		private int? _bonusAmount;
		private int? _bonusPercentage;
		private int? _cancellationBonusAmount;
		private int? _cancellationBonusPercentage;

		private readonly IMvxNavigationService _navigationService;

		private Service _service;
		private readonly IServicesService _servicesService;
		private MvxCommand _updateCommand;
		#endregion
		#endregion

		#region .ctor
		public EditBusinessmanServicesDetailsViewModel(IMvxNavigationService navigationService,
													   IServicesService servicesService)
		{
			_servicesService = servicesService;
			_navigationService = navigationService;
		}
		#endregion

		#region Properties
		public int? BonusAmount
		{
			get => _bonusAmount;
			set => SetProperty(ref _bonusAmount, value);
		}

		public int? BonusPercentage
		{
			get => _bonusPercentage;
			set => SetProperty(ref _bonusPercentage, value);
		}

		public int? CancellationBonusAmount
		{
			get => _cancellationBonusAmount;
			set => SetProperty(ref _cancellationBonusAmount, value);
		}

		public int? CancellationBonusPercentage
		{
			get => _cancellationBonusPercentage;
			set => SetProperty(ref _cancellationBonusPercentage, value);
		}

		public MvxCommand UpdateCommand
		{
			get
			{
				_updateCommand = _updateCommand ?? new MvxCommand(UpdateCommandExecute);
				return _updateCommand;
			}
		}
		#endregion

		#region Overrided
		public override async Task Initialize()
		{
			await base.Initialize();

			if (_service.AccrualMethod == BonusValueType.Points)
			{
				BonusAmount = _service.AccrualValue / 100;
			}
			else
			{
				BonusPercentage = _service.AccrualValue;
			}

			if (_service.WhiteOffMethod == BonusValueType.Points)
			{
				CancellationBonusAmount = _service.WhiteOffValue / 100;
			}
			else
			{
				CancellationBonusPercentage = _service.WhiteOffValue;
			}
		}

		public override void Prepare(Service parameter)
		{
			_service = parameter;
		}
		#endregion

		#region Private
		private async void UpdateCommandExecute()
		{
			var service = new CreateServiceDto
			{
				Uuid = _service.Uuid
			};

			BonusValueType accrualMethod;
			BonusValueType writeOffMethod;
			int accrualValue;
			int writeOffValue;
			if (BonusAmount != null && BonusAmount > 0)
			{
				service.AccrualMethod = BonusValueType.Points.ToString()
													  .ToLower();
				service.AccrualValue = BonusAmount.Value * 100;
				accrualValue = BonusAmount.Value;
				accrualMethod = BonusValueType.Points;
			}
			else if (BonusPercentage != null && BonusPercentage > 0)
			{
				service.AccrualMethod = BonusValueType.Percent.ToString()
													  .ToLower();
				service.AccrualValue = BonusPercentage.Value;
				accrualValue = BonusPercentage.Value;
				accrualMethod = BonusValueType.Percent;
			}
			else
			{
				await MaterialDialog.Instance.AlertAsync("Укажите количество или процент начисляемых бонусов", "Внимание", "Ок");
				return;
			}

			if (CancellationBonusAmount != null && CancellationBonusAmount > 0)
			{
				service.WriteOffMethod = BonusValueType.Points.ToString()
													   .ToLower();
				service.WriteOffValue = CancellationBonusAmount.Value * 100;
				writeOffValue = CancellationBonusAmount.Value;
				writeOffMethod = BonusValueType.Points;
			}
			else if (CancellationBonusPercentage != null && CancellationBonusPercentage > 0)
			{
				service.WriteOffMethod = BonusValueType.Percent.ToString()
													   .ToLower();
				service.WriteOffValue = CancellationBonusPercentage.Value;
				writeOffValue = CancellationBonusPercentage.Value;
				writeOffMethod = BonusValueType.Percent;
			}
			else
			{
				await MaterialDialog.Instance.AlertAsync("Укажите количество или процент списываемых бонусов", "Внимание", "Ок");
				return;
			}

			var result = await _servicesService.UpdateService(service, _service.Uuid);

			if (!result)
			{
				await MaterialDialog.Instance.AlertAsync("Не удалось обновить услугу", "Внимание", "Ок");
				return;
			}

			_service.AccrualMethod = accrualMethod;
			_service.WhiteOffMethod = writeOffMethod;
			_service.AccrualValue = accrualValue;
			_service.WhiteOffValue = writeOffValue;

			await _navigationService.Close(this, _service);
		}
		#endregion
	}
}
