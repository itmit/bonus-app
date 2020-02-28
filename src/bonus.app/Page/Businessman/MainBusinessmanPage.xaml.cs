using bonus.app.Core.ViewModels;
using bonus.app.Core.ViewModels.Businessman;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Page.Businessman
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	[MvxMasterDetailPagePresentation(MasterDetailPosition.Root, WrapInNavigationPage = false, Title = "Главная")]
    public partial class MainBusinessmanPage : MvxMasterDetailPage<MainBusinessmanViewModel>
    {
        public MainBusinessmanPage()
        {
            InitializeComponent();
        }
	}
}