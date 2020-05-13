using bonus.app.Core.ViewModels.News;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Pages.News
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	[MvxTabbedPagePresentation(Position = TabbedPosition.Tab, Icon = "ic_news", Title = "Новости")]
	public partial class NewsPage : MvxContentPage<NewsViewModel>
	{
		#region .ctor
		public NewsPage()
		{
			InitializeComponent();
		}
		#endregion
	}
}
