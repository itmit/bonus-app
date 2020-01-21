using System;
using bonus.app.Core.ViewModels.Auth;
using bonus.app.Views;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Page.Auth
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PublicOfferPage : MvxContentPage<PublicOfferViewModel>
    {
        public PublicOfferPage()
        {
            InitializeComponent();
        }

		private void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
		{
			Ch1.IsChecked = !Ch1.IsChecked;
		}

		private void TapGestureRecognizer_OnTapped_1(object sender, EventArgs e)
		{
			Ch2.IsChecked = !Ch2.IsChecked;
		}
	}
}