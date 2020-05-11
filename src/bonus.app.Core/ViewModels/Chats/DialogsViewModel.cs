using System;
using System.Threading.Tasks;
using bonus.app.Core.Models;
using bonus.app.Core.Services;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace bonus.app.Core.ViewModels.Chats
{
	public class DialogsViewModel : MvxViewModel
	{
		private IMvxNavigationService _navigationService;
		private IChatsService _chatsService;
		private MvxObservableCollection<Dialog> _dialogs;
		private Dialog _selectedDialog;
		private bool _isRefreshing;
		private MvxCommand _refreshCommand;

		#region .ctor
		public DialogsViewModel(IMvxNavigationService navigationService, IChatsService chatsService)
		{
			_navigationService = navigationService;
			_chatsService = chatsService;
		}
		#endregion

		public bool IsRefreshing
		{
			get => _isRefreshing;
			set => SetProperty(ref _isRefreshing, value);
		}

		public MvxCommand RefreshCommand
		{
			get
			{
				_refreshCommand = _refreshCommand ?? new MvxCommand(async () =>
				{
					IsRefreshing = true;
					try
					{
						Dialogs = new MvxObservableCollection<Dialog>(await _chatsService.GetDialogs());
					}
					catch (Exception e)
					{
						Console.WriteLine(e);
					}
					IsRefreshing = false;
				});
				return _refreshCommand;
			}
		}

		public MvxObservableCollection<Dialog> Dialogs
		{
			get => _dialogs;
			private set => SetProperty(ref _dialogs, value);
		}

		public override async Task Initialize()
		{
			await base.Initialize();

			try
			{
				var dialogs = _chatsService.SavedDialogs;
				if (dialogs.Count == 0)
				{
					try
					{
						dialogs = await _chatsService.GetDialogs();
					}
					catch (Exception e)
					{
						Console.WriteLine(e);
					}
				}
				Dialogs = new MvxObservableCollection<Dialog>(dialogs);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}

		public Dialog SelectedDialog
		{
			get => _selectedDialog;
			set
			{
				if (value == null)
				{
					return;
				}

				SetProperty(ref _selectedDialog, value);

				_navigationService.Navigate<ChatViewModel, ChatViewModelArguments>(new ChatViewModelArguments(value));

				SetProperty(ref _selectedDialog, null);
			}
		}
	}
}
