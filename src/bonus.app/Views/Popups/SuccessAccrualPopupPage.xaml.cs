using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Views.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SuccessAccrualPopupPage : Rg.Plugins.Popup.Pages.PopupPage
    {
        public SuccessAccrualPopupPage(double bonusesAccrued)
        {
            InitializeComponent();
            Span.Text = bonusesAccrued.ToString();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PopPopupAsync();
        }
    }
}