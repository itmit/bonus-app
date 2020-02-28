using bonus.app.Core.ViewModels.Businessman.Statistics;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Page.Businessman.Statistics
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GeographyPage : MvxContentPage<GeographyViewModel>
    {
        public GeographyPage()
        {
            InitializeComponent();
        }
    }
}