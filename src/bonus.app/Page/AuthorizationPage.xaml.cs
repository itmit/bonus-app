using bonus.app.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bonus.app.Page
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AuthorizationPage : ContentPage
    {
        public AuthorizationPage()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Удалить, когда будет нормальная авторизация
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Clicked(object sender, EventArgs e)
        {
            if (Login.Text == "User")
            {
                if(Password.Text == "1")
                {
                    Application.Current.MainPage = new MainPage();
                }
            }
            else if (Login.Text == "Manager")
            {
                if(Password.Text == "1")
                {
                    Application.Current.MainPage = new MainBusinessmanTabbedPage();
                }
            }
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new BusinessmanAndBuyerPage());
        }

        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AuthorizationRecoveryPage());
        }

        private void TapGestureRecognizer_Tapped_2(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AuthVKFC());
        }
    }
}