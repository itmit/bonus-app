using System;
using bonus.app.Core.ViewModels;
using bonus.app.Core.ViewModels.Businessman.Shares;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Page.Businessman.Shares
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	[MvxTabbedPagePresentation(Position = TabbedPosition.Tab,
		Icon = "ic_stock",
		Title = "Акции")]
    public partial class BusinessmanSharesPage : MvxContentPage<BusinessmanSharesViewModel>
    {
        public BusinessmanSharesPage()
        {
            InitializeComponent();
			BackgroundColor = Color.FromRgba(196, 196, 196, 0.2);
        }

		private void ListView_OnItemTapped(object sender, ItemTappedEventArgs e)
		{
			((ListView) sender).SelectedItem = null;
		}
	}
}