using System.ComponentModel;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;

namespace bonus.app.Views
{
    [DesignTimeVisible(false)]
    public partial class MainBusinessmanTabbedPage : Xamarin.Forms.TabbedPage
    {
        public MainBusinessmanTabbedPage()
        {
            InitializeComponent();
            On<Xamarin.Forms.PlatformConfiguration.Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);
        }
    }
}