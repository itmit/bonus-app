using bonus.app.Core.ViewModels.Customer.News;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Customer.News
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
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