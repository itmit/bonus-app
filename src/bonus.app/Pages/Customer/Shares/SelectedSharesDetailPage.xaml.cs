using bonus.app.Core.ViewModels.Customer.Shares;
using MvvmCross.Forms.Views;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Pages.Customer.Shares
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SelectedSharesDetailPage : MvxContentPage<SelectedSharesDetailViewModel>
	{
		public SelectedSharesDetailPage()
		{
			InitializeComponent();
		}
	}
}