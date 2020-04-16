using System;
using System.Collections.ObjectModel;
using bonus.app.Core.ViewModels;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Page
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyBonusPage : MvxContentPage<MyBonusViewModel>
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