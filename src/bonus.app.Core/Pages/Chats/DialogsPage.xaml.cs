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

		private void SelectableItemsView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			((CollectionView) sender).SelectedItem = null;
		}
	}
}
