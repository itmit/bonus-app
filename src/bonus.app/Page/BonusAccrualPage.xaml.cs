using System;
using bonus.app.Core.ViewModels;
using bonus.app.Page;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Page
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
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