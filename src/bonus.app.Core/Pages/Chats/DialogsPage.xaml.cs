using bonus.app.Core.ViewModels;
using bonus.app.Core.ViewModels.Chats;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Pages.Chats
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DialogsPage : MvxContentPage<DialogsViewModel>
	{
		#region .ctor
		public DialogsPage()
		{
			InitializeComponent();
		}
		#endregion

		#region Private
		private void ListView_OnItemTapped(object sender, ItemTappedEventArgs e)
		{
			Navigation.PushAsync(new ChatPage());
		}
		#endregion
	}
}
