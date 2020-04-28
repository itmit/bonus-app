using bonus.app.Core.ViewModels;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Page
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MessageListPage : MvxContentPage<MessageListViewModel>
	{
		#region .ctor
		public MessageListPage()
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
