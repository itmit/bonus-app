using System;
using System.Threading.Tasks;
using bonus.app.Core.ViewModels.Businessman.Statistics;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
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

		#region private
		private async void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
		{
			if (sender is Label)
			{
				((Label)sender).BackgroundColor = Color.FromHex("#BB8D91");
				((Label)sender).BackgroundColor = await GetColor();
			}
		}

		private async Task<Color> GetColor()
		{
			await Task.Delay(100);
			return Color.Transparent;
		}
		#endregion
	}
}
