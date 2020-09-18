using System;
using System.Threading.Tasks;
using bonus.app.Core.Models;
using bonus.app.Core.Models.ChatModels;
using bonus.app.Core.Services;
using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.Navigation.EventArguments;
using MvvmCross.ViewModels;

namespace bonus.app.Core.ViewModels.Chats
{
	public class DialogsViewModel : MvxViewModel
	{
		#region Data
		#region Fields
		private readonly IChatsService _chatsService;
		private MvxObservableCollection<Dialog> _dialogs;
		private bool _isRefreshing;
		private readonly IMvxNavigationService _navigationService;
		private MvxCommand _refreshCommand;
		private Dialog _selectedDialog;
		#endregion
		#endregion

		#region .ctor
		public DialogsViewModel(IMvxNavigationService navigationService, IChatsService chatsService)
		{
			_navigationService = navigationService;
			_chatsService = chatsService;
			var service = Mvx.IoCProvider.Resolve<IMessagingService>();

			if (service != null)
			{
				service.MessageReceived += OnMessageReceived;
			}
		}

		private async void OnMessageReceived(object sender, EventArgs e)
		{
			await Initialize();
		}
		#endregion

		#region Properties
		public MvxObservableCollection<Dialog> Dialogs
		{
			get => _dialogs;
			private set => SetProperty(ref _dialogs, value);
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
			}
		}
		#endregion

		#region Overrided
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
		#endregion
	}
}
