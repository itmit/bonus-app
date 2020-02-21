using System.ComponentModel;
using bonus.app.Core.ViewModels;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;

namespace bonus.app.Views
{
    [DesignTimeVisible(false)]
    [MvxMasterDetailPagePresentation(MasterDetailPosition.Detail, NoHistory = true)]
    [MvxTabbedPagePresentation(TabbedPosition.Root, NoHistory = true)]
    public partial class MainPage : MvxTabbedPage<MainViewModel>
    {
        public MainPage()
        {
            InitializeComponent();
            On<Xamarin.Forms.PlatformConfiguration.Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);
        }
    }
}