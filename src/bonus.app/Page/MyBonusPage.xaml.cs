using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Page
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyBonusPage : ContentPage
    {
        public MyBonusPage()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage.DisplayAlert("Спасибо за посещение","Салон Бигуди\n\nСписано 200 бонусов,\nНачислено 200 бонусов","Перейти в профиль");
            Navigation.PopAsync();
        }
    }
}