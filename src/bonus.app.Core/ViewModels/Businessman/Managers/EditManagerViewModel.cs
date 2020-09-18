using System;
using bonus.app.Core.Models.UserModels;
using bonus.app.Core.Services;
using bonus.app.Core.Validations;
using MvvmCross.Commands;
using MvvmCross.Forms.Presenters;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Xamarin.Forms;
using XF.Material.Forms.UI.Dialogs;

namespace bonus.app.Core.ViewModels.Businessman.Managers
{
	public class EditManagerViewModel : MvxViewModel<User, bool>
	{
		private User _user;

		public User User
		{
			get => _user;
			private set
			{
				SetProperty(ref _user, value);
				RaisePropertyChanged(() => CanUpdateManager);
			}
		}

		public ValidatableObject<string> PhoneNumber
		{
			get => _phoneNumber;
			set
			{
				SetProperty(ref _phoneNumber, value);
				RaisePropertyChanged(() => CanUpdateManager);
			}
		}

		private ValidatableObject<string> _name = new ValidatableObject<string>();
		private readonly IManagerService _managerService;
		private readonly IMvxNavigationService _navigationService;
		private ValidatableObject<string> _phoneNumber = new ValidatableObject<string>();
		private MvxCommand _editManagerCommand;
		private MvxCommand _deleteManagerCommand;
		private Command<string> _raiseCanUpdateManagerCommand;

		public EditManagerViewModel(IMvxNavigationService navigationService, IManagerService managerService)
		{
			_navigationService = navigationService;
			_managerService = managerService;
		}

		public Command<string> RaiseCanUpdateManagerCommand
		{
			get
			{
				_raiseCanUpdateManagerCommand = _raiseCanUpdateManagerCommand ??
												new Command<string>(s =>
												{
													RaisePropertyChanged(() => CanUpdateManager);
												});
				return _raiseCanUpdateManagerCommand;
			}
		}

		public ValidatableObject<string> Name
		{
			get => _name;
			set
			{
				SetProperty(ref _name, value);
				RaisePropertyChanged(() => CanUpdateManager);
			}
		}

		public bool CanUpdateManager => PhoneNumber.Value != User.Phone || Name.Value != User.Name;

		public MvxCommand EditManagerCommand
		{
			get
			{
				_editManagerCommand = _editManagerCommand ?? new MvxCommand(EditManagerCommandExecute);
				return _editManagerCommand;
			}
		}

		public MvxCommand DeleteManagerCommand
		{
			get
			{
				_deleteManagerCommand = _deleteManagerCommand ?? new MvxCommand(DeleteManagerCommandExecute);
				return _deleteManagerCommand;
			}
		}

		private async void DeleteManagerCommandExecute()
		{
			var confirm = await MaterialDialog.Instance.ConfirmAsync("Вы уверены, что хотите удалить этого менеджера?", "Внимание", "Да", "Нет");

			if (confirm == null)
			{
				return;
			}

			if (!confirm.Value)
			{
				return;
			}

			try
			{
				var res = await _managerService.DeleteManager(User.Id);
				if (!res)
				{
					await MaterialDialog.Instance.AlertAsync("Менеджер уже удален", "Ошибка", "Ок");
				}
				await _navigationService.Close(this, true);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}

		private bool IsValidFields => Name.Validate() & PhoneNumber.Validate();

		private async void EditManagerCommandExecute()
		{
			if (!IsValidFields)
			{
				return;
			}

			try
			{
				await _navigationService.Close(this, await _managerService.EditManager(User.Id, Name.Value, PhoneNumber.Value));
				await MaterialDialog.Instance.AlertAsync("Изменения сохранены", "Внимание", "Ок");
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}

		public override void Prepare(User parameter)
		{
			User = parameter;
			PhoneNumber.Value = User.Phone;
			Name.Value = User.Name;
		}
	}
}
