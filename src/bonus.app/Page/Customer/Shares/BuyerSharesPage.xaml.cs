using System;
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