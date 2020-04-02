using System;
using bonus.app.Core.ViewModels.Businessman.Popups;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Pages.Businessman.Popups
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	[MvxPopupPagePresentationAttribute()]
	public partial class SharePopupPage : MvxPopupPage<SharePopupViewModel>
	{
		public SharePopupPage()
		{
			InitializeComponent();
		}

		private void ImageButton_OnClicked(object sender, EventArgs e)
		{
			Navigation.PopPopupAsync();
		}
	}
}