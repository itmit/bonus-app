using bonus.app.Core.ViewModels;
using bonus.app.Core.ViewModels.Businessman;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Page.Businessman
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	[MvxMasterDetailPagePresentation(Position = MasterDetailPosition.Detail, WrapInNavigationPage = true)]
	[MvxTabbedPagePresentation(TabbedPosition.Root, Title = "Табы", WrapInNavigationPage = true)]
	public partial class BusinessmanTabbedPage : MvxTabbedPage<MainTabbedBusinessmanViewModel>
    {
        public BusinessmanTabbedPage()
        {
            InitializeComponent();
			On<Android>()
				.SetToolbarPlacement(ToolbarPlacement.Bottom);
		}
    }
}