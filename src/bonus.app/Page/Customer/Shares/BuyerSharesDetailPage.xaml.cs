using bonus.app.Core.ViewModels.Shares;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Shares
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