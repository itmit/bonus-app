using System;
using bonus.app.Core.Page.Profile;
using bonus.app.Core.ViewModels;
using bonus.app.Core.ViewModels.Profile;
using bonus.app.Page;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Page
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [MvxContentPagePresentation(WrapInNavigationPage = false, Title = "Профиль")]
    public partial class BusinessmanProfilePage : MvxContentPage<BusinessmanProfileViewModel>
    {
        public BusinessmanProfilePage()
        {
            InitializeComponent();
        }
    }
}