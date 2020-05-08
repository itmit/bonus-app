using System;
using System.Linq;
using bonus.app.Core.Models;
using bonus.app.Core.Services;
using bonus.app.Core.Validations;
using MvvmCross.Commands;
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
		private MvxCommand _removeServiceCommand;
		#endregion
		#endregion

		#region .ctor
		public CreatedServiceViewModel()
		{
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

		public ICreateServiceViewModel ViewModel
		{
			get;
			set;
		}

		public Command<bool> CreateServiceCommand
		{
			get
			{
				_createServiceCommand = _createServiceCommand ??
										new Command<bool>(Execute);
				return _createServiceCommand;
			}
		}

		public MvxCommand RemoveServiceCommand
		{
			get
			{
				_removeServiceCommand = _removeServiceCommand ?? new MvxCommand(async () =>
				{
					if (IsBusy)
					{
						return;
					}
					if (!IsCreated)
					{
						Device.BeginInvokeOnMainThread(() =>
						{
							Application.Current.MainPage.DisplayAlert("Внимание", $"Услуга {Name.Value} не создана.", "Ок");
						});
					}

					try
					{
						if (await ViewModel.RemoveServiceTypeItem(ServiceTypeItem.Uuid))
						{
							IsBusy = false;
						}
						else
						{
							Device.BeginInvokeOnMainThread(() =>
							{
								Application.Current.MainPage.DisplayAlert("Внимание", "Не удалось удалить услугу.", "Ок");
							});
						}
					}
					catch (Exception e)
					{
						Console.WriteLine(e);
					}
				});
				return _removeServiceCommand;
			}
		}

		private async void Execute(bool isFocused)
		{
			if (!isFocused)
			{
				if (!Name.Validate())
				{
					return;
				}

				if (IsBusy || IsCreated)
				{
					return;
				}

				IsBusy = true;
				try
				{
					ServiceTypeItem = await ViewModel.CreateServiceTypeItem(Name.Value);
					IsCreated = ServiceTypeItem != null;

				}
				catch (InvalidOperationException e)
				{
					Console.WriteLine(e);
					throw;
				}
				catch (Exception e)
				{
					Console.WriteLine(e);
				}

				IsBusy = false;
			}
		}

		public ServiceTypeItem ServiceTypeItem
		{
			get;
			set;
		}

		public ValidatableObject<string> Name
		{
			get => _name;
			set => SetProperty(ref _name, value);
		}
		#endregion
	}
}
