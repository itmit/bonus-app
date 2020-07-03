using System;
using System.Threading.Tasks;
using bonus.app.Core.Models.UserModels;
using bonus.app.Core.Services;
using bonus.app.Core.Validations;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace bonus.app.Core.ViewModels.Businessman.Managers
{
	public class ManagersViewModel : MvxViewModel
	{
		private MvxCommand _openCreateManagerCommand;
		private readonly IMvxNavigationService _navigationService;
		private IManagerService _managerService;
		private MvxObservableCollection<User> _managers;
		private User _selectedManager;
		private bool _isRefreshing;
		private MvxCommand _refreshCommand;

		public ManagersViewModel(IMvxNavigationService navigationService, IManagerService managerService)
		{
			_managerService = managerService;
			_navigationService = navigationService;
		}

		public MvxObservableCollection<User> Managers
		{
			get => _managers;
			private set => SetProperty(ref _managers, value);
		}

		public bool IsRefreshing
		{
			get => _isRefreshing;
			set => SetProperty(ref _isRefreshing, value);
		}

		public MvxCommand RefreshCommand
		{
			get
			{
				_refreshCommand = _refreshCommand ??
								  new MvxCommand(async () =>
								  {
									  IsRefreshing = true;
									  await Initialize();
									  IsRefreshing = false;
								  });
				return _refreshCommand;
			}
		}

		public User SelectedManager
		{
			get => _selectedManager;
			set
			{
				if (value == null)
				{
					return;
				}

				SetProperty(ref _selectedManager, value);
				OpenEditPage(value);
			}
		}

		private async void OpenEditPage(User value)
		{
			if (await _navigationService.Navigate<EditManagerViewModel, User, bool>(value))
			{
				RefreshCommand.Execute();
			}
		}

		public override async Task Initialize() 
		{
			await base.Initialize();

			try
			{
				Managers = new MvxObservableCollection<User>(await _managerService.GetManagers());
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}

		public MvxCommand OpenCreateManagerCommand
		{
			get
			{
				_openCreateManagerCommand = _openCreateManagerCommand ??
											new MvxCommand(async () =>
											{
												if (await _navigationService.Navigate<CreateManagerViewModel, bool>())
												{
													RefreshCommand.Execute();
												}
											});
				return _openCreateManagerCommand;
			}
		}
	}
}
