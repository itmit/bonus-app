﻿using System;
using System.Threading.Tasks;
using bonus.app.Core.ViewModels;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Page
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ArchiveStockPage : MvxContentPage<ArchiveStockViewModel>
    {
        public ArchiveStockPage()
        {
            InitializeComponent();
        }

		/// <summary>
		/// Управляет выплывающим фильтром
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private async void MenuItem_OnClicked(object sender, EventArgs e)
		{
			if (Filter.IsEnabled)
			{
				BlackBackground.FadeTo(0, 500).GetAwaiter();
				Filter.TranslateTo(0, -480, 500).GetAwaiter();
				BlackBackground.IsVisible = await GetEndVisible();
				Filter.IsEnabled = false;
			}
			else
			{
				BlackBackground.FadeTo(0.7, 500).GetAwaiter();
				Filter.TranslateTo(0, 0, 500).GetAwaiter();
				BlackBackground.IsVisible = true;
				Filter.IsEnabled = true;
			}
		}

		/// <summary>
		/// Задерживает видимость черного фона
		/// </summary>
		/// <returns></returns>
		private async Task<bool> GetEndVisible()
		{
			await Task.Delay(500);
			return false;
		}
	}
}