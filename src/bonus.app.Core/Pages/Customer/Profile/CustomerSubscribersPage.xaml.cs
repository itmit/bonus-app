using bonus.app.Core.ViewModels.Customer.Profile;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Pages.Customer.Profile
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CustomerSubscribersPage : MvxContentPage<CustomerSubscribersViewModel>
	{
		#region .ctor
		public CustomerSubscribersPage()
		{
			InitializeComponent();
		}
		#endregion

		#region Private
		private void SelectableItemsView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			((CollectionView) sender).SelectedItem = null;
		}
		#endregion
	}
}
