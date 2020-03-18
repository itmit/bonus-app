using System;
using bonus.app.Core.ViewModels;
using bonus.app.Core.ViewModels.Businessman.BonusAccrual;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;

namespace bonus.app.Core.Page.Businessman.BonusAccrual
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
		}

		private void Button_OnClicked(object sender, EventArgs e)
		{
			Navigation.PushAsync(new BusinessmanBonusAccrualDetails());
		}

		private void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
		{
			var page = new ZXingScannerPage();
			page.OnScanResult += async result =>
			{
				await Navigation.PopModalAsync();
				ViewModel.ScanResult(result);
			};

			Navigation.PushModalAsync(page);
		}
	}
}