using System;
using System.Diagnostics;
using bonus.app.Core.ViewModels;
using bonus.app.Core.ViewModels.Businessman;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PhotoPage : MvxPopupPage<PhotoViewModel>
	{
		public PhotoPage()
		{
			InitializeComponent();
		}

		/// <summary>When overridden, allows application developers to customize behavior immediately prior to the <see cref="T:Xamarin.Forms.Page" /> becoming visible.</summary>
		/// <remarks>To be added.</remarks>
		protected override void OnAppearing()
		{
			base.OnAppearing();

			var scrollNum = Scroll.Width / Scroll.Height;
			var imageNum = CachedImage.Width / CachedImage.Height;
			if (scrollNum > imageNum)
			{
				CachedImage.HeightRequest = Scroll.Height;
			}
			else
			{
				if (CachedImage.Width <= Scroll.Width)
				{
					CachedImage.HeightRequest = Scroll.Height;
					return;
				}
				CachedImage.WidthRequest = Scroll.Width;
			}
		}

		private void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
		{
			Navigation.PopAsync();
		}
	}
}