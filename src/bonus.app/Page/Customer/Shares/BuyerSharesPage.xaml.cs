using System;
using bonus.app.Core.Page;
using bonus.app.Core.Page.Customer.Shares;
using bonus.app.Core.ViewModels.Shares;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Shares
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BuyerSharesPage : MvxContentPage<BuyerSharesViewModel>
    {
        public BuyerSharesPage()
        {
            InitializeComponent();

            BackgroundColor = Color.FromRgba(196, 196, 196, 0.2);

			var oneItem = new ToolbarItem
			{
				Text = "Избранные акции",
				Order = ToolbarItemOrder.Secondary,
				Priority = 0
			};

			var twoItem = new ToolbarItem
			{
				Text = "Архив акций",
				Order = ToolbarItemOrder.Secondary,
				Priority = 1
			};

			oneItem.Clicked += OneItem_Clicked;
			twoItem.Clicked += TwoItem_Clicked;

			ToolbarItems.Add(oneItem);
			ToolbarItems.Add(twoItem);
		}

		private void TwoItem_Clicked(object sender, EventArgs e)
		{
			Navigation.PushAsync(new ArchiveStockPage());
		}

		private void OneItem_Clicked(object sender, EventArgs e)
		{
			Navigation.PushAsync(new SelectedSharesPage());
		}

		private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new BayerSharesDetailPage());
        }

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
	}
}