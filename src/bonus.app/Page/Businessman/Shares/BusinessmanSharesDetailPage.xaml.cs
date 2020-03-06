using System;
using bonus.app.Core.ViewModels;
using bonus.app.Core.ViewModels.Businessman.Shares;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Page.Businessman.Shares
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BusinessmanSharesDetailPage : MvxContentPage<BusinessmanSharesDetailViewModel>
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

			TextSpan.Text = "Отклонена";
            ChangeBackground(TextSpan.Text);
		}

        private void ToolBar2_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new EditorStockPage());
        }

        private void ToolBar1_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Core.Page.ArchiveStockPage());
        }

        private void ToolBar_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CreateStockPage());
        }

        /// <summary>
        /// Меняет цвет у страницы в зависимости от статуса акции
        /// </summary>
        /// <param name="status">статус акции</param>
        private void ChangeBackground(string status)
        {
			if (status.Equals("Завершена"))
			{
				ContentPageBackground.BackgroundColor = Color.FromHex("#807D746D");
			}
            else if (status.Equals("Отклонена"))
			{
				ContentPageBackground.BackgroundColor = Color.FromHex("#80BB8D91");
			}
        }
    }
}