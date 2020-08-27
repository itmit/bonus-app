using bonus.app.Core.ViewModels.Businessman;
using bonus.app.Core.ViewModels.Manager;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Pages.Manager
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	[MvxMasterDetailPagePresentation(MasterDetailPosition.Root, WrapInNavigationPage = false, Title = "Главная")]
	public partial class MainManagerPage : MvxMasterDetailPage<MainManagerViewModel>
	{
		#region .ctor
		public MainManagerPage()
		{
			InitializeComponent();
		}
		#endregion
	}
}
