using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Page
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BusinessmanBonusAccrualPage : ContentPage
    {
        public BusinessmanBonusAccrualPage()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Core.Page.BusinessmanBonusAccrualDetails());
        }
    }
}