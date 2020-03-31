using bonus.app.Core.ViewModels.Customer.Shares;
using MvvmCross.Forms.Views;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Page.Customer.Shares
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BayerSharesDetailPage : MvxContentPage<BayerSharesDetailViewModel>
    {
        public BayerSharesDetailPage()
        {
            InitializeComponent();
        }
    }
}