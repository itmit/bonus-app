using bonus.app.Core.ViewModels.Businessman.News;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Pages.Businessman.News
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	[MvxTabbedPagePresentation(Position = TabbedPosition.Tab, Icon = "ic_news", Title = "Новости")]
	public partial class BusinessmanNewsPage : MvxContentPage<BusinessmanNewsViewModel>
	{
		#region .ctor
		public BusinessmanNewsPage()
		{
			InitializeComponent();
		}
		#endregion
	}
}
