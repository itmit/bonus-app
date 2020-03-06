﻿using bonus.app.Core.ViewModels.Businessman.News;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Page.Businessman.News
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BusinessmanNewsDetailsPage : MvxContentPage<BusinessmanNewsDetailsViewModel>
	{
		#region Fields
		private int _top = 20;
		#endregion

		public BusinessmanNewsDetailsPage()
        {
            InitializeComponent();
			FrameImage.IsVisible = true;
			CarouselViewImages.IsVisible = false;
			ControlVisible(FrameImage.IsVisible, CarouselViewImages.IsVisible);
		}

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

		/// <summary>When overridden, allows application developers to customize behavior immediately prior to the <see cref="T:Xamarin.Forms.Page" /> becoming visible.</summary>
		/// <remarks>To be added.</remarks>
		protected override void OnAppearing()
		{
			base.OnAppearing();
			var app = Application.Current.MainPage.Navigation.NavigationStack;
		}
	}
}