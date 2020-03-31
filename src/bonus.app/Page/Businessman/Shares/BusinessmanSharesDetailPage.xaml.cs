using System;
using bonus.app.Core.ViewModels;
using bonus.app.Core.ViewModels.Businessman.Shares;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Page.Businessman.Shares
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BusinessmanSharesDetailPage : MvxContentPage<BusinessmanSharesDetailViewModel>
    {
        public BusinessmanSharesDetailPage()
        {
            InitializeComponent();
		}
	}
}