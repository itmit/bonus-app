using System;
using bonus.app.Core.ViewModels.Customer.Services;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Pages.Customer.Services
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	[MvxTabbedPagePresentation(Position = TabbedPosition.Tab, Icon = "ic_star", Title = "Услуги")]
	public partial class CustomerServicesPage : MvxContentPage<CustomerServicesViewModel>
	{
		#region .ctor
		public CustomerServicesPage()
		{
			InitializeComponent();
		}
		#endregion

		#region Private
		private void ImageButton_OnClicked(object sender, EventArgs e)
		{
			if (GridHeader.IsEnabled)
			{
				GridHeader.IsEnabled = false;
				GridHeader.IsVisible = false;
			}
			else
			{
				GridHeader.IsEnabled = true;
				GridHeader.IsVisible = true;
			}
		}
		#endregion

		private void SelectableItemsView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			((CollectionView) sender).SelectedItem = null;
		}
	}
}
