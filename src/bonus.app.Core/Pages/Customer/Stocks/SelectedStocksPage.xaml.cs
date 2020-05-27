using bonus.app.Core.ViewModels.Customer.Stocks;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Pages.Customer.Stocks
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SelectedStocksPage : MvxContentPage<SelectedStocksViewModel>
	{
		#region .ctor
		public SelectedStocksPage()
		{
			InitializeComponent();
		}
		#endregion

		#region Private
		private void TapGestureRecognizer_Tapped(object sender, ItemTappedEventArgs e)
		{
			Navigation.PushAsync(new SelectedStocksDetailPage());
		}
		#endregion
	}
}
