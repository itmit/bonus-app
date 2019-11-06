using bonus.app.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bonus.app.Page
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewsPage : ContentPage
    {
        public NewsPage()
        {
            InitializeComponent();

            NewsList.ItemsSource = new ObservableCollection<News>()
            {
                new News()
                {
                    Imgsource = "girl.png",
                    Name = "Важная новость",
                    Date = "27.09.2019",
                    Text = "Акция на пакет XL",
                },
                new News()
                {
                    Imgsource = "girl.png",
                    Name = "Важная новость",
                    Date = "10.06.2019",
                    Text = "Внимание, внимание. Спасибо за внимание !",
                },
            };
        }
    }
}