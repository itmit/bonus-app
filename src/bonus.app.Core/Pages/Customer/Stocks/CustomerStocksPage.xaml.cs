using System;
using bonus.app.Core.ViewModels.Customer.Stocks;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Pages.Customer.Stocks
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	[MvxTabbedPagePresentation(Position = TabbedPosition.Tab, Icon = "ic_stock", Title = "Акции")]
	public partial class CustomerStocksPage : MvxContentPage<CustomerStocksViewModel>
	{
		#region .ctor
		public CustomerStocksPage()
		{
			InitializeComponent();

			BackgroundColor = Color.FromRgba(196, 196, 196, 51);
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
