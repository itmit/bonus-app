using bonus.app.Core.ViewModels.Customer.Shares;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Pages.Customer.Shares
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SelectedSharesPage : MvxContentPage<SelectedSharesViewModel>
	{
		public SelectedSharesPage()
		{
			InitializeComponent();
		}

		private void TapGestureRecognizer_Tapped(object sender, ItemTappedEventArgs e)
		{
			Navigation.PushAsync(new SelectedSharesDetailPage());
		}
	}
}