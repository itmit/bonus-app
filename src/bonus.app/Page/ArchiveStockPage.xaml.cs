using System;
using System.Threading.Tasks;
using bonus.app.Core.ViewModels;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Page
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ArchiveStockPage : MvxContentPage<ArchiveStockViewModel>
    {
        public ArchiveStockPage()
        {
            InitializeComponent();
        }

		private void MenuItem_OnClicked(object sender, EventArgs e)
		{
			if (Filter.IsEnabled)
			{
				Filter.IsEnabled = false;
				Filter.TranslateTo(0, -480, 500);
				BlackBackground.FadeTo(0, 500);
				BlackBackground.IsVisible = false;
			}
			else
			{
				Filter.IsEnabled = true;
				Filter.TranslateTo(0, 0, 500);
				BlackBackground.IsVisible = true;
				BlackBackground.FadeTo(0.7, 500);
			}
		}
	}
}