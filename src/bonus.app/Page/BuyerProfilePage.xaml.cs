using System;
using bonus.app.Core.Page.Profile;
using bonus.app.Page;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Page
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BuyerProfilePage : ContentPage
    {
        public BuyerProfilePage()
        {
            InitializeComponent();
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new EditProfileBauerPage());
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SubscribePage());
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MessageListPage());
        }
    }
}