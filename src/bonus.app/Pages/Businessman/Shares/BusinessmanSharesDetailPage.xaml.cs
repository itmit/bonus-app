using bonus.app.Core.ViewModels.Businessman.Shares;
using MvvmCross.Forms.Views;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Pages.Businessman.Shares
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BusinessmanSharesDetailPage : MvxContentPage<BusinessmanSharesDetailViewModel>
	{
		#region .ctor
		public BusinessmanSharesDetailPage()
		{
			InitializeComponent();
		}
		#endregion
	}
}
