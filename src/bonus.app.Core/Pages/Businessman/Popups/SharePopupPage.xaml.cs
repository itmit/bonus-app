using System;
using bonus.app.Core.ViewModels.Businessman.Popups;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Pages.Businessman.Popups
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	[MvxPopupPagePresentationAttribute]
	public partial class SharePopupPage : MvxPopupPage<SharePopupViewModel>
	{
		#region .ctor
		public SharePopupPage()
		{
			InitializeComponent();
		}
		#endregion

		#region Private
		private void ImageButton_OnClicked(object sender, EventArgs e)
		{
			Navigation.PopPopupAsync();
		}
		#endregion
	}
}
