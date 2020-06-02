using bonus.app.Core.ViewModels.Customer.Stocks;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Pages.Customer.Stocks
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FavoriteStocksPage : MvxContentPage<FavoriteStocksViewModel>
	{
		#region .ctor
		public FavoriteStocksPage()
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
