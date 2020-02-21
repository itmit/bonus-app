using bonus.app.Core.ViewModels;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Page
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	[MvxMasterDetailPagePresentation(MasterDetailPosition.Root, NoHistory = true)]
    public partial class MainBuyerProfilePage : MvxMasterDetailPage<MainCustomerViewModel>
    {
        public MainBuyerProfilePage()
        {
            InitializeComponent();
        }
    }
}