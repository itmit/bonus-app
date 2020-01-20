using System.Collections.ObjectModel;
using bonus.app.Core.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Page
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
                new Stock()
                {
                    Imgsource = "girl.png",
                    Name = "Черный загар",
                    Company = "Студия загар Ibiza",
                    Date = "10.06.2019",
                    Status = "Закрыто",
                    Text = "Всю неделю до 12.00 на загар в солярии предоставляем бонус 50%.",
                },
            };
        }
    }
}