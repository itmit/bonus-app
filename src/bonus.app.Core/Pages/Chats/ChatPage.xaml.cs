using bonus.app.Core.ViewModels.Chats;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Pages.Chats
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ChatPage : MvxContentPage<ChatViewModel>
	{
		#region .ctor
		public ChatPage()
		{
			InitializeComponent();

			Grid.BackgroundColor = Color.FromRgba(160, 150, 142, 76);
		}
		#endregion

		#region Public
		public void OnListTapped(object sender, ItemTappedEventArgs e)
		{
			ChatInput.UnFocusEntry();
		}
		#endregion
	}
}
