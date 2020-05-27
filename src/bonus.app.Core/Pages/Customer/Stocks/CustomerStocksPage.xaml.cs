using System;
using bonus.app.Core.Pages.Businessman.Stocks;
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
		private void ImageButton_OnClicked(object sender, EventArgs e)
		{
			if (StackLayout.IsEnabled)
			{
				StackLayout.IsVisible = false;
				StackLayout.IsEnabled = false;
			}
			else
			{
				StackLayout.IsVisible = true;
				StackLayout.IsEnabled = true;
			}
		}

		#endregion

		private void SelectableItemsView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			((CollectionView) sender).SelectedItem = null;
		}
	}
}
