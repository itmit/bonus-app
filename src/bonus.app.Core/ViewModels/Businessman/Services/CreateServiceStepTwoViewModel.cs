using System;
using bonus.app.Core.Dtos.BusinessmanDtos;
using bonus.app.Core.Models;
using bonus.app.Core.Models.ServiceModels;
using bonus.app.Core.Services;
using bonus.app.Core.Services.Interfaces;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Xamarin.Forms;
using XF.Material.Forms.UI.Dialogs;

namespace bonus.app.Core.ViewModels.Businessman.Services
{
	public class CreateServiceStepTwoViewModel : MvxViewModel<CreateServiceStepTwoViewModel.CreateServiceStepTwoViewModelArgs>
	{
		private readonly IServicesService _servicesService;
		private readonly IMvxNavigationService _navigationService;
		private int? _bonusAmount;
		private int? _bonusPercentage;
		private int? _cancellationBonusAmount;
		private int? _cancellationBonusPercentage;
		private MvxCommand _addServiceCommand;
		private CreateServiceStepTwoViewModelArgs _parameter;

		public CreateServiceStepTwoViewModel(IMvxNavigationService navigationService, IServicesService servicesService)
		{
			_navigationService = navigationService;
			_servicesService = servicesService;
		}

		public override void Prepare(CreateServiceStepTwoViewModelArgs parameter)
		{
			_parameter = parameter;
		}


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

		public MvxCommand AddServiceCommand
		{
			get
			{
				_addServiceCommand = _addServiceCommand ?? new MvxCommand(AddServiceCommandExecute);
				return _addServiceCommand;
			}
		}

		public class CreateServiceStepTwoViewModelArgs
		{
			public CreateServiceStepTwoViewModelArgs(Guid serviceGuid, CreateServiceStepOneViewModel parentViewModel)
			{
				ParentViewModel = parentViewModel;
				ServiceGuid = serviceGuid;
			}

			public CreateServiceStepOneViewModel ParentViewModel
			{
				get;
			}

			public Guid ServiceGuid
			{
				get;
			}
		}

		private async void AddServiceCommandExecute()
		{
			try
			{
				var service = new CreateServiceDto
				{
					Uuid = _parameter.ServiceGuid
				};
				if (BonusAmount != null && BonusAmount > 0)
				{
					service.AccrualMethod = BonusValueType.Points.ToString()
														  .ToLower();
					service.AccrualValue = BonusAmount.Value;
				}
				else if (BonusPercentage != null && BonusPercentage > 0)
				{
					service.AccrualMethod = BonusValueType.Percent.ToString()
														  .ToLower();
					service.AccrualValue = BonusPercentage.Value;
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
					service.WriteOffValue = CancellationBonusAmount.Value;
				}
				else if (CancellationBonusPercentage != null && CancellationBonusPercentage > 0)
				{
					service.WriteOffMethod = BonusValueType.Percent.ToString()
														   .ToLower();
					service.WriteOffValue = CancellationBonusPercentage.Value;
				}
				else
				{
					await MaterialDialog.Instance.AlertAsync("Укажите количество или процент списываемых бонусов", "Внимание", "Ок");
					return;
				}

				using (await MaterialDialog.Instance.LoadingDialogAsync("Сохранение..."))
				{
					var result = await _servicesService.CreateService(service);

					if (!result)
					{
						return;
					}

					await MaterialDialog.Instance.AlertAsync("Услуга создана", "Внимание", "Ок");
					await _navigationService.Close(_parameter.ParentViewModel);
					await _navigationService.Close(this);
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}
	}
}
