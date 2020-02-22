using bonus.app.Core.ViewModels.Businessman.News;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Page.Businessman.News
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	[MvxTabbedPagePresentation(Position = TabbedPosition.Tab,
		Icon = "ic_news",
		WrapInNavigationPage = false,
		Title = "Новости")]
	public partial class BusinessmanNewsPage : MvxContentPage<BusinessmanNewsViewModel>
    {
        public BusinessmanNewsPage()
        {
            InitializeComponent();
        }

		private void ListView_OnItemTapped(object sender, ItemTappedEventArgs e)
		{
			Navigation.PushAsync(new BusinessmanNewsDetailsPage());
		}
	}
}