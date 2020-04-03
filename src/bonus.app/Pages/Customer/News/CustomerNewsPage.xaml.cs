using bonus.app.Core.ViewModels.Customer.News;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Pages.Customer.News
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	[MvxTabbedPagePresentation(Position = TabbedPosition.Tab,
		Icon = "ic_news",
		Title = "Новости")]
	public partial class CustomerNewsPage : MvxContentPage<CustomerNewsViewModel>
    {
        public CustomerNewsPage()
        {
            InitializeComponent();
        }

		private void ListView_OnItemTapped(object sender, ItemTappedEventArgs e)
		{
			Navigation.PushAsync(new CustomerNewsDetailsPage());
		}
	}
}