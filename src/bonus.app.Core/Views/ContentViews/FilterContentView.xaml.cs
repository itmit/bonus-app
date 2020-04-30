﻿using System;
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
		}

		/// <summary>
		/// Раскрывает и закрывает список городов
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void VisibleCity_OnTapped(object sender, EventArgs e)
		{
			if (CityList.IsEnabled)
			{
				CityList.IsVisible = false;
				CityList.IsEnabled = false;
				Shape.Rotation = 0;
			}
			else
			{
				CityList.IsVisible = true;
				CityList.IsEnabled = true;
				Shape.Rotation = 180;
				ServicesList.IsVisible = false;
				ServicesList.IsEnabled = false;
				Shape1.Rotation = 0;
			}
		}

		/// <summary>
		/// Раскрывает и закрывает список с видами услуг
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void VisibleServices_OnTapped(object sender, EventArgs e)
		{
			if (ServicesList.IsEnabled)
			{
				ServicesList.IsVisible = false;
				ServicesList.IsEnabled = false;
				Shape1.Rotation = 0;
			}
			else
			{
				ServicesList.IsVisible = true;
				ServicesList.IsEnabled = true;
				Shape1.Rotation = 180;
				CityList.IsVisible = false;
				CityList.IsEnabled = false;
				Shape.Rotation = 0;
			}
		}
		#endregion
	}
}