using bonus.app.Core.ViewModels.Businessman;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Pages.Businessman
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	[MvxMasterDetailPagePresentation(MasterDetailPosition.Root, WrapInNavigationPage = false, Title = "Главная")]
	public partial class MainBusinessmanPage : MvxMasterDetailPage<MainBusinessmanViewModel>
	{
		#region .ctor
		public MainBusinessmanPage()
		{
			InitializeComponent();
		}
		#endregion
	}
}
