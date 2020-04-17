using System;
using System.Threading.Tasks;
using bonus.app.Core.ViewModels.Businessman.Shares;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Pages.Businessman.Shares
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ShareArchivePage : MvxContentPage<ShareArchiveViewModel>
	{
		#region .ctor
		public ShareArchivePage()
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

		/// <summary>
		/// Управляет выплывающим фильтром
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private async void MenuItem_OnClicked(object sender, EventArgs e)
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

        /// <summary>
        /// Скрывает фильтр
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
			BlackBackground.FadeTo(0, 500)
							   .GetAwaiter();
			Filter.TranslateTo(0, Device.Info.PixelScreenSize.Height - Device.Info.PixelScreenSize.Height - Device.Info.PixelScreenSize.Height, 500)
				  .GetAwaiter();
			BlackBackground.IsVisible = await GetEndVisible();
			Filter.IsEnabled = false;
		}
        #endregion
    }
}
