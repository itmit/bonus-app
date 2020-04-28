﻿using bonus.app.Core.ViewModels.Customer.News;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Pages.Customer.News
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CustomerNewsDetailsPage : MvxContentPage<CustomerNewsDetailsViewModel>
	{
		#region Data
		#region Fields
		private int _top = 20;
		#endregion
		#endregion

		#region .ctor
		public CustomerNewsDetailsPage()
		{
			InitializeComponent();
			FrameImage.IsVisible = true;
			CarouselViewImages.IsVisible = false;
			ControlVisible(FrameImage.IsVisible, CarouselViewImages.IsVisible);
		}
		#endregion

		#region Private
		/// <summary>
		/// Управляет видимостью картинок
		/// </summary>
		/// <param name="x">Видимость одной картинки</param>
		/// <param name="y">Видимость коллекции картинок</param>
		private void ControlVisible(bool x, bool y)
		{
			if (FrameImage.IsVisible == x && CarouselViewImages.IsVisible == y)
			{
				FrameImage.Margin = new Thickness(0, 25, 0, _top);
				LabelNew.Margin = new Thickness(0, 0, 0, 5);
			}
			else if (FrameImage.IsVisible == x && CarouselViewImages.IsVisible == y)
			{
				_top = 10;
				FrameImage.Margin = new Thickness(0, 25, 0, _top);
				LabelNew.Margin = new Thickness(0, 0, 0, 5);
			}
			else if (FrameImage.IsVisible == x && CarouselViewImages.IsVisible == y)
			{
				LabelNew.Margin = new Thickness(0, 25, 0, 5);
			}
		}
		#endregion
	}
}
