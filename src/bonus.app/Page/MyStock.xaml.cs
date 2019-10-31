using bonus.app.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bonus.app.Page
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyStock : ContentPage
    {
        public MyStock()
        {
            InitializeComponent();

            StockList.ItemsSource = new ObservableCollection<Stock>()
            {
                new Stock()
                {
                    Imgsource = "girl.png",
                    Name = "Бархатный загар",
                    Company = "Студия загар Ibiza",
                    Date = "10.04.2019",
                    Status = "Активно",
                    Text = "Всю неделю до 12.00 на загар в солярии предоставляем бонус 25%. Так же всю неделю до 12.00 на загар в солярии предоставляем бонус",
                },
            };
        }
    }
}