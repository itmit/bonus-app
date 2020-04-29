using System;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Views.Popups
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SuccessAccrualPopupPage : PopupPage
	{
		#region .ctor
		public SuccessAccrualPopupPage(double bonusesAccrued)
		{
			InitializeComponent();
			Span.Text = bonusesAccrued.ToString();
		}
		#endregion

		#region Private
		private void Button_Clicked(object sender, EventArgs e)
		{
			Navigation.PopPopupAsync();
		}
		#endregion
	}
}
