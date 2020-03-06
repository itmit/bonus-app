using System;
using bonus.app.Core.ViewModels;
using bonus.app.Core.Views.Popups;
using MvvmCross.Forms.Views;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Page.Businessman.BonusAccrual
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BusinessmanBonusAccrualDetails : MvxContentPage<BusinessmanBonusAccrualDetailsViewModel>
    {
        public BusinessmanBonusAccrualDetails()
        {
            InitializeComponent();
        }

		private void Button_Clicked(object sender, EventArgs e)
		{
			var popupPage = new SuccessAccrualPopupPage();
			Navigation.PushPopupAsync(popupPage);
			Navigation.PopAsync();
		}
    }
}