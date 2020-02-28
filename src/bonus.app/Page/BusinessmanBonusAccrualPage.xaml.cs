using System;
using bonus.app.Core.ViewModels;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing;
using ZXing.Net.Mobile.Forms;

namespace bonus.app.Core.Page
{
	[MvxTabbedPagePresentation(Position = TabbedPosition.Tab,
		Icon = "ic_charges",
		WrapInNavigationPage = false,
		Title = "Начисления")]
	public partial class BusinessmanBonusAccrualPage : MvxContentPage<BusinessmanBonusAccrualViewModel>
    {
        public BusinessmanBonusAccrualPage()
        {
            InitializeComponent();
			/*
			var zxing = new ZXingScannerView
			{
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.FillAndExpand,
				AutomationId = "zxingScannerView",
			};
			zxing.OnScanResult += (result) =>
				Device.BeginInvokeOnMainThread(async () => {

					// Stop analysis until we navigate away so we don't keep reading barcodes
					zxing.IsAnalyzing = false;

					// Show an alert
					await DisplayAlert("Scanned Barcode", result.Text, "OK");
				});

			Frame.Content = zxing;*/
		}

		private void Button_OnClicked(object sender, EventArgs e)
		{
			Navigation.PushAsync(new BusinessmanBonusAccrualDetails());
		}

		private void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
		{
			var page = new ZXingScannerPage();
			page.OnScanResult += (result) =>
			{
				Navigation.PopAsync();

				ViewModel.ScanResult(result);
			};

			Navigation.PushModalAsync(page);
		}
	}
}