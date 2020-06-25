using bonus.app.Core.ViewModels;
using bonus.app.Core.ViewModels.Businessman;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Pages.Businessman
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	[MvxContentPagePresentation(WrapInNavigationPage = true)]
	public partial class TariffPage : MvxContentPage<TariffViewModel>
	{
		#region .ctor
		public TariffPage()
		{
			InitializeComponent();
		}
		#endregion
	}
}
