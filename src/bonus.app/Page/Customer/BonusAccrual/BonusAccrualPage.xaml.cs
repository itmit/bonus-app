using System;
using bonus.app.Core.ViewModels;
using bonus.app.Core.ViewModels.Customer.BonusAccrual;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;

namespace bonus.app.Core.Page.Customer.BonusAccrual
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	[MvxTabbedPagePresentation(Position = TabbedPosition.Tab,
		Icon = "ic_charges",
		WrapInNavigationPage = false,
		Title = "Начисления")]
    public partial class BonusAccrualPage : MvxContentPage<BonusAccrualViewModel>
    {
        public BonusAccrualPage()
        {
            InitializeComponent();

			var barcode = new ZXingBarcodeImageView
			{
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.FillAndExpand,
				AutomationId = "zxingBarcodeImageView",
				BarcodeFormat = ZXing.BarcodeFormat.QR_CODE,
				BarcodeOptions =
				{
					Width = 225,
					Height = 225
				},
			};
			barcode.SetBinding(ZXingBarcodeImageView.BarcodeValueProperty, "UserUuid");

			Frame.Content = barcode;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MyBonusPage());
        }
    }
}