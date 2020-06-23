using bonus.app.Core.ViewModels.Businessman.Statistics;
using MvvmCross.Forms.Views;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Pages.Businessman.Statistics
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ViewsProfilePage : MvxContentPage<ViewsProfileViewModel>
	{
		#region .ctor
		public ViewsProfilePage()
		{
			InitializeComponent();
		}
		#endregion

		/// <summary>When overridden, allows application developers to customize behavior immediately prior to the <see cref="T:Xamarin.Forms.Page" /> becoming visible.</summary>
		/// <remarks>To be added.</remarks>
		protected override void OnAppearing()
		{
			base.OnAppearing();

			LineChart.BindingContext = ViewModel;
		}
	}
}
