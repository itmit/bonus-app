using System;
using bonus.app.Core.Page.Businessman.Profile;
using bonus.app.Page;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Page.Customer.Profile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomerProfilePage : ContentPage
    {
        public CustomerProfilePage()
        {
            InitializeComponent();
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new EditProfileCustomerPage());
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