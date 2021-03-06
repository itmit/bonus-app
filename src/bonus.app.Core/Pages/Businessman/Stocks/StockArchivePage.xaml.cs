﻿using System;
using System.Threading.Tasks;
using bonus.app.Core.ViewModels.Businessman.Stocks;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Pages.Businessman.Stocks
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class StockArchivePage : MvxContentPage<StockArchiveViewModel>
	{
		#region .ctor
		public StockArchivePage()
		{
			InitializeComponent();
			Filter.TranslationY = Device.Info.PixelScreenSize.Height - Device.Info.PixelScreenSize.Height - Device.Info.PixelScreenSize.Height;
		}
		#endregion

		#region Private
		/// <summary>
		/// Задерживает видимость черного фона
		/// </summary>
		/// <returns></returns>
		private async Task<bool> GetEndVisible()
		{
			await Task.Delay(500);
			return false;
		}

		private void SelectableItemsView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			((CollectionView) sender).SelectedItem = null;
		}
		#endregion

		/// <summary>When overridden, allows application developers to customize behavior immediately prior to the <see cref="T:Xamarin.Forms.Page" /> becoming visible.</summary>
		/// <remarks>To be added.</remarks>
		protected override void OnAppearing()
		{
			base.OnAppearing();

			Filter.ServicesFilterContentView.ViewModel = ViewModel.MyServicesContentViewModel;
		}

		private async void ShowFilter(object sender, EventArgs e)
		{
			if (Filter.IsEnabled)
			{
				BlackBackground.FadeTo(0, 500)
							   .GetAwaiter();
				Filter.TranslateTo(0, Device.Info.PixelScreenSize.Height - Device.Info.PixelScreenSize.Height - Device.Info.PixelScreenSize.Height, 500)
					  .GetAwaiter();
				BlackBackground.IsVisible = await GetEndVisible();
				Filter.IsEnabled = false;
			}
			else
			{
				BlackBackground.FadeTo(0.7, 500)
							   .GetAwaiter();
				Filter.TranslateTo(0, Device.Info.PixelScreenSize.Height - Device.Info.PixelScreenSize.Height, 500)
					  .GetAwaiter();
				BlackBackground.IsVisible = true;
				Filter.IsEnabled = true;
			}
		}
	}
}
