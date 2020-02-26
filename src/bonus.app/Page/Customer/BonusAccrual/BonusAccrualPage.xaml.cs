using System;
using bonus.app.Core.ViewModels;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms.Xaml;

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
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MyBonusPage());
        }
    }
}