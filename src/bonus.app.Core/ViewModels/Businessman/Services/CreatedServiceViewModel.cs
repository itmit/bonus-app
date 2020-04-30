﻿using System;
using bonus.app.Core.Services;
using bonus.app.Core.Validations;
using MvvmCross.ViewModels;
using Xamarin.Forms;

namespace bonus.app.Core.ViewModels.Businessman.Services
{
	public class CreatedServiceViewModel : MvxViewModel
	{
		#region Data
		#region Fields
		private Command<bool> _createServiceCommand;
		private ValidatableObject<string> _name = new ValidatableObject<string>();
		private readonly IServicesService _servicesService;
		#endregion
		#endregion

		#region .ctor
		public CreatedServiceViewModel(IServicesService servicesService)
		{
			_servicesService = servicesService;
			Name.Validations.Add(new IsNotNullOrEmptyRule
			{
				ValidationMessage = "Заполните название вида услуги."
			});
			Name.Validations.Add(new MinLengthRule(2)
			{
				ValidationMessage = "Название вида услуги не может быть меньше 3 символов."
			});
		}
		#endregion

		#region Properties
		public bool IsBusy
		{
			get;
			private set;
		}

		public bool IsCreated
		{
			get;
			set;
		}

		public MyServicesViewModel ParentViewModel
		{
			get;
			set;
		}

		public Command<bool> CreateServiceCommand
		{
			get
			{
				_createServiceCommand = _createServiceCommand ??
										new Command<bool>(async isFocused =>
										{
											if (!isFocused)
											{
												if (!Name.Validate())
												{
													return;
												}

												if (IsBusy&& IsCreated)
												{
													return;
												}

												IsBusy = true;
												try
												{
													if (ParentViewModel.UserServiceType == null)
													{
														if (!await _servicesService.CreateServiceType(ParentViewModel.UserUuid.ToString()) || !await ParentViewModel.ReloadServices())
														{
															throw new InvalidOperationException("Невозможно создать вид услуги без категории.");
														}
													}

													if (ParentViewModel.UserServiceType == null)
													{
														throw new InvalidOperationException("Невозможно создать вид услуги без категории.");
													}

													if (await _servicesService.CreateService(Name.Value, ParentViewModel.UserServiceType.Uuid))
													{
														IsCreated = true;
														await ParentViewModel.ReloadServices();
													}
													else
													{
														Device.BeginInvokeOnMainThread(() =>
														{
															Application.Current.MainPage.DisplayAlert("Внимание", $"Не удалось создать услугу: \"{Name.Value}\"", "");
														});
													}
												}
												catch (Exception e)
												{
													Console.WriteLine(e);
												}

												IsBusy = false;
											}
										});
				return _createServiceCommand;
			}
		}

		public ValidatableObject<string> Name
		{
			get => _name;
			set => SetProperty(ref _name, value);
		}
		#endregion
	}
}