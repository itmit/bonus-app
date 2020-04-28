using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Views.ViewCells.Chat
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ChatInputBarView : ContentView
	{
		#region .ctor
		public ChatInputBarView()
		{
			InitializeComponent();
		}
		#endregion

		#region Public
		public void UnFocusEntry()
		{
			ChatTextInput?.Unfocus();
		}
		#endregion
	}
}
