using bonus.app.Core.ViewModels.Businessman.Statistics;
using MvvmCross.Forms.Views;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Page.Businessman.Statistics
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TransitionsProfilePage : MvxContentPage<TransitionsProfileViewModel>
    {
        public TransitionsProfilePage()
        {
            InitializeComponent();
        }
    }
}