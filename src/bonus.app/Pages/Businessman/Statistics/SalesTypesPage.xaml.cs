using System;
using bonus.app.Core.ViewModels.Businessman.Statistics;
using MvvmCross.Forms.Views;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Pages.Businessman.Statistics
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SalesTypesPage : MvxContentPage<SalesTypesViewModel>
	{
		#region .ctor
		public SalesTypesPage()
		{
			InitializeComponent();
		}
		#endregion

		#region Private
		private void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
		{
		}
		#endregion
	}
}
