using System;
using bonus.app.Core.Views.ContentViews.Stocks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Views.ViewCells
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FilterContentView : ContentView
	{
		#region .ctor
		public FilterContentView()
		{
			InitializeComponent();
			MyStockLabel.TextColor = Color.Gray;
		}
		#endregion

		#region Private
		/// <summary>
		/// Управляет видимостью двух аккордионов
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ImageButton_OnClicked(object sender, EventArgs e)
		{
			if (StackLayout.IsEnabled)
			{
				StackLayout.IsVisible = false;
				StackLayout.IsEnabled = false;
			}
			else
			{
				StackLayout.IsVisible = true;
				StackLayout.IsEnabled = true;
			}
		}

		/// <summary>
		/// Событие при нажатии на левый таб
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
		{
			RightBoxView.BackgroundColor = Color.Transparent;
			MyStockLabel.Scale = 1.1;
			MyStockLabel.TextColor = Color.FromHex("#505050");
			LeftBoxView.BackgroundColor = Color.FromHex("#BB8D91");
			AllStockLabel.Scale = 1;
			AllStockLabel.TextColor = Color.Gray;
			StackLayout.IsVisible = false;
			((IFilterViewModel) BindingContext).IsMyStocks = true;
		}

		/// <summary>
		/// Событие при нажатии на правый таб
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TapGestureRecognizer_OnTapped1(object sender, EventArgs e)
		{
			RightBoxView.BackgroundColor = Color.FromHex("#BB8D91");
			MyStockLabel.Scale = 1;
			MyStockLabel.TextColor = Color.Gray;
			LeftBoxView.BackgroundColor = Color.Transparent;
			AllStockLabel.Scale = 1.1;
			AllStockLabel.TextColor = Color.FromHex("#505050");
			StackLayout.IsVisible = false;
			((IFilterViewModel) BindingContext).IsMyStocks = false;
		}
		#endregion
	}
}
