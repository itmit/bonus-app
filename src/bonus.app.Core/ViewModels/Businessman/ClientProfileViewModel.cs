using bonus.app.Core.Models;
using bonus.app.Core.ViewModels.Chats;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace bonus.app.Core.ViewModels.Businessman
{
	public class ClientProfileViewModel : MvxViewModel<User>
	{
		#region Data
		#region Fields
		private readonly IMvxNavigationService _navigationService;
		private MvxCommand _openChatCommand;
		private User _user;
		#endregion
		#endregion

		#region .ctor
		public ClientProfileViewModel(IMvxNavigationService navigationService) => _navigationService = navigationService;
		#endregion

		#region Properties
		public MvxCommand OpenChatCommand
		{
			get
			{
				_openChatCommand = _openChatCommand ??
								   new MvxCommand(() =>
								   {
									   _navigationService.Navigate<ChatViewModel, ChatViewModelArguments>(new ChatViewModelArguments(User, null));
								   });
				return _openChatCommand;
			}
		}

		public User User
		{
			get => _user;
			private set => SetProperty(ref _user, value);
		}
		#endregion

		#region Overrided
		public override void Prepare(User parameter)
		{
			User = parameter;
		}
		#endregion
	}
}
