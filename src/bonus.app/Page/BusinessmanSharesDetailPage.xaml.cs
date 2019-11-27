using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bonus.app.Page
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BusinessmanSharesDetailPage : ContentPage
    {
        public BusinessmanSharesDetailPage()
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

            var toolBar2 = new ToolbarItem
            {
                Text = "Редактировать акцию",
                Order = ToolbarItemOrder.Secondary,
                Priority = 2
            };

            toolBar.Clicked += ToolBar_Clicked;
            toolBar1.Clicked += ToolBar1_Clicked;
            toolBar2.Clicked += ToolBar2_Clicked;

            ToolbarItems.Add(toolBar);
            ToolbarItems.Add(toolBar1);
            ToolbarItems.Add(toolBar2);

        }

        private void ToolBar2_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new EditorStockPage());
        }

        private void ToolBar1_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ArchiveStockPage());
        }

        private void ToolBar_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CreateStockPage());
        }
    }
}