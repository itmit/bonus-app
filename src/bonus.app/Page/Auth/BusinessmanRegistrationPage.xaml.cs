using System;
using bonus.app.Core.ViewModels.Auth;
using bonus.app.Core.Views.Popups;
using MvvmCross.Forms.Views;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Page.Auth
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BusinessmanRegistrationPage : MvxContentPage<BusinessmanRegistrationViewModel>
    {
		public BusinessmanRegistrationPage()
        {
            InitializeComponent();
        }
	}
}