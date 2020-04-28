using bonus.app.Core.ViewModels;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bonus.app.Page
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	[MvxContentPagePresentation(WrapInNavigationPage = true)]
	public partial class TariffPage : MvxContentPage<TariffViewModel>
	{
		#region .ctor
		public TariffPage()
		{
			InitializeComponent();
			Tariff.BackgroundColor = Color.FromRgba(160, 150, 142, 0.1);
			Frame.BackgroundColor = Color.FromRgba(160, 150, 142, 0.1);
		}
		#endregion
	}
}
