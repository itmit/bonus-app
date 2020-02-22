﻿using System;
using bonus.app.Core.ViewModels;
using bonus.app.Page;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Page
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	[MvxTabbedPagePresentation(Position = TabbedPosition.Tab,
		Icon = "ic_stock",
		WrapInNavigationPage = false,
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
        }

        private void ToolBar1_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Core.Page.ArchiveStockPage());
        }

        private void ToolBar_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CreateStockPage());
        }

		private void ListView_OnItemTapped(object sender, ItemTappedEventArgs e)
		{
			Navigation.PushAsync(new BusinessmanSharesDetailPage());
		}
	}
}