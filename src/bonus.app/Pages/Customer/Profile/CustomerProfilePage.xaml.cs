using System;
using bonus.app.Core.ViewModels.Customer.Profile;
using bonus.app.Page;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Page.Customer.Profile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	[MvxTabbedPagePresentation(Position = TabbedPosition.Tab,
		Icon = "ic_profile",
		WrapInNavigationPage = false,
		Title = "Профиль")]
    public partial class CustomerProfilePage : MvxContentPage<CustomerProfileViewModel>
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
            Navigation.PushAsync(new SubscribesPage());
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MessageListPage());
        }

		private void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
		{
			Navigation.PushAsync(new SubscribesPage());
		}
	}
}