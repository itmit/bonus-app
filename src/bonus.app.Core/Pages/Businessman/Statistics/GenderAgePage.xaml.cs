using bonus.app.Core.ViewModels.Businessman.Statistics;
using Microcharts;
using MvvmCross.Forms.Views;
using SkiaSharp;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Pages.Businessman.Statistics
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class GenderAgePage : MvxContentPage<GenderAgeViewModel>
	{
		#region .ctor
		public GenderAgePage()
		{
			InitializeComponent();
		}
		#endregion
	}
}
