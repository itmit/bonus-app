using System;
using bonus.app.Core.ViewModels.Businessman.Statistics;
using MvvmCross.Forms.Views;
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

		#region Private
		private void Cell_OnTapped(object sender, EventArgs e)
		{
			Navigation.PushAsync(new SalesTypesPage());
		}
		#endregion
	}
}
