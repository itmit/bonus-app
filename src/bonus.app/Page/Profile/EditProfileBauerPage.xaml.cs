using System;
using bonus.app.Core.ViewModels.Profile;
using MvvmCross.Forms.Views;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Page.Profile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditProfileBauerPage : MvxContentPage<EditProfileBauerViewModel>
    {
        public EditProfileBauerPage()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}