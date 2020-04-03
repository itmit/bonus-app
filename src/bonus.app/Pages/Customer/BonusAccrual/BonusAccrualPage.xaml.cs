using System;
using bonus.app.Core.Page;
using bonus.app.Core.ViewModels.Customer.BonusAccrual;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;

namespace bonus.app.Core.Pages.Customer.BonusAccrual
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	[MvxTabbedPagePresentation(Position = TabbedPosition.Tab,
		Icon = "ic_charges",
		Title = "Начисления")]
    public partial class BonusAccrualPage : MvxContentPage<CustomerBonusAccrualViewModel>
    {
        public BonusAccrualPage()
        {
            InitializeComponent();

			BarcodeImageView.BarcodeOptions.Width = 225;
			BarcodeImageView.BarcodeOptions.Height = 225;
		}

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MyBonusPage());
        }
    }
}