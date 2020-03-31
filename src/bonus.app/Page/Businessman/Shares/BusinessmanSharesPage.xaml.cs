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

            var toolBar = new ToolbarItem
            {
                Text = "Создать новую акцию",
                Order = ToolbarItemOrder.Secondary,
                Priority = 0
            };

            var toolBar1 = new ToolbarItem
            {
                Text = "Архив акций",
                Order = ToolbarItemOrder.Secondary,
                Priority = 1
            };

            toolBar.Clicked += ToolBar_Clicked;
            toolBar1.Clicked += ToolBar1_Clicked;

            ToolbarItems.Add(toolBar);
            ToolbarItems.Add(toolBar1);

			BackgroundColor = Color.FromRgba(196, 196, 196, 0.2);
        }

        private void ToolBar1_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ArchiveStockPage());
        }

        private void ToolBar_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CreateStockPage());
        }

		private void ListView_OnItemTapped(object sender, ItemTappedEventArgs e)
		{
			((ListView) sender).SelectedItem = null;
		}
	}
}