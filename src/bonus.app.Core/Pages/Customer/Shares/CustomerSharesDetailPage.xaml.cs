using bonus.app.Core.ViewModels.Customer.Shares;
using MvvmCross.Forms.Views;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Pages.Customer.Shares
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CustomerSharesDetailPage : MvxContentPage<CustomerSharesDetailViewModel>
	{
		#region .ctor
		public CustomerSharesDetailPage()
		{
			InitializeComponent();
		}
		#endregion
	}
}
