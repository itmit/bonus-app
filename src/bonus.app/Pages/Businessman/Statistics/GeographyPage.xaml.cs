using bonus.app.Core.ViewModels.Businessman.Statistics;
using MvvmCross.Forms.Views;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Pages.Businessman.Statistics
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class GeographyPage : MvxContentPage<GeographyViewModel>
	{
		#region .ctor
		public GeographyPage()
		{
			InitializeComponent();
		}
		#endregion
	}
}
