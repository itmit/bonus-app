using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmCross.ViewModels;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Views.Popups
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SuccessRegisterPopupPage : PopupPage
	{
		public SuccessRegisterPopupPage(MvxViewModel context)
		{
			InitializeComponent();
			BindingContext = context;
		}

		private void Button_OnClicked(object sender, EventArgs e)
		{
			Navigation.PopPopupAsync();
		}
	}
}