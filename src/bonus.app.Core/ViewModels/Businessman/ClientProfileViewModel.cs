using bonus.app.Core.Models;
using bonus.app.Core.ViewModels.Chats;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace bonus.app.Core.ViewModels.Businessman
{
	public class ClientProfileViewModel : MvxViewModel<User>
	{
		private User _user;
		private MvxCommand _openChatCommand;

		private readonly IMvxNavigationService _navigationService;

		public ClientProfileViewModel(IMvxNavigationService navigationService)
		{
			_navigationService = navigationService;
		}

		public User User
		{
			get => _user;
			private set => SetProperty(ref _user, value);
		}

		public MvxCommand OpenChatCommand
		{
			get
			{
				_openChatCommand = _openChatCommand ?? new MvxCommand(() =>
				{
					_navigationService.Navigate<ChatViewModel, ChatViewModelArguments>(new ChatViewModelArguments(User, null));
				});
				return _openChatCommand;
			}
		}

		public override void Prepare(User parameter)
		{
			User = parameter;
		}
	}
}
