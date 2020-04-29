using bonus.app.Core.ViewModels;
using MvvmCross.Forms.Views;
using Xamarin.Forms.Xaml;

namespace bonus.app.Page
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SubscribesPage : MvxContentPage<SubscribesViewModel>
	{
		#region .ctor
		public SubscribesPage()
		{
			InitializeComponent();
		}
		#endregion
	}
}
