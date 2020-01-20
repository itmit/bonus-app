using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Page
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BusinessmanBonusAccrualDetails : ContentPage
    {
        public BusinessmanBonusAccrualDetails()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage.DisplayAlert("Начисление прошло успешно","Списано 200 бонусов\nНачисленно 200 бонусов","Перейти в профиль");
            Navigation.PopAsync();
        }
    }
}