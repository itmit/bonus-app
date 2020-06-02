using System;
using System.Threading.Tasks;
using bonus.app.Core.ViewModels.Businessman.Statistics;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Pages.Businessman.Statistics
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class StatisticsPage : MvxContentPage<StatisticsViewModel>
	{
		#region .ctor
		public StatisticsPage()
		{
			InitializeComponent();
		}
		#endregion

		#region private
		private async void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
		{
			if (sender is StackLayout)
			{
				((StackLayout) sender).BackgroundColor = Color.FromHex("#BB8D91");
				((StackLayout) sender).BackgroundColor = await GetColor();
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
