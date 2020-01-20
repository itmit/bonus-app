using System;
using bonus.app.Page;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Page
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BusinessmanServicesPage : ContentPage
    {
        public BusinessmanServicesPage()
        {
            InitializeComponent();

            var toolBar = new ToolbarItem
            {
                Text = "Добавить свою услугу",
                Order = ToolbarItemOrder.Secondary,
                Priority = 0
            };

            var toolBar1 = new ToolbarItem
            {
                Text = "Редактировать список услуг",
                Order = ToolbarItemOrder.Secondary,
                Priority = 1
            };

            toolBar.Clicked += ToolbarItem_Clicked;
            toolBar1.Clicked += ToolBar1_Clicked;

            ToolbarItems.Add(toolBar);
            ToolbarItems.Add(toolBar1);
        }

        private void ToolBar1_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new EditBusinessmanServicesPage());
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            return;
        }
    }
}