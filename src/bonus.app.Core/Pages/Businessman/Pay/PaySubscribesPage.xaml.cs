using bonus.app.Core.ViewModels.Businessman.Pay;
using MvvmCross.Forms.Views;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Pages.Businessman.Pay
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PaySubscribesPage : MvxContentPage<PaySubscribesViewModel>
	{
		#region .ctor
		public PaySubscribesPage()
		{
			InitializeComponent();
		}
		#endregion
	}
}
