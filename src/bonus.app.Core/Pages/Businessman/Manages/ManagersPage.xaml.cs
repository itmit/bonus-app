using bonus.app.Core.ViewModels.Businessman.Managers;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Pages.Businessman.Manages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ManagersPage : MvxContentPage<ManagersViewModel>
	{
		public ManagersPage()
		{
			InitializeComponent();
		}

		private void SelectableItemsView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			((CollectionView) sender).SelectedItem = null;
		}
	}
}