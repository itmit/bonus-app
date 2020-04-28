using bonus.app.Core.ViewModels;
using MvvmCross.Forms.Views;
using Xamarin.Forms.Xaml;

namespace bonus.app.Page
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SubscribersPage : MvxContentPage<SubscribersViewModel>
	{
		#region .ctor
		public SubscribersPage()
		{
			InitializeComponent();
		}
		#endregion
	}
}
