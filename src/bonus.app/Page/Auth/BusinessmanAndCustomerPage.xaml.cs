using System;
using bonus.app.Core.ViewModels.Auth;
using bonus.app.Page;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Page.Auth
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BusinessmanAndCustomerPage : MvxContentPage<BusinessmanAndCustomerViewModel>
    {
        public BusinessmanAndCustomerPage()
        {
            InitializeComponent();
        }
	}
}