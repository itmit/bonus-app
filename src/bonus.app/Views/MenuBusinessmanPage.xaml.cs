using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bonus.app.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuBusinessmanPage : ContentPage
    {
        public MenuBusinessmanPage()
        {
            InitializeComponent();

            BackgroundColor = Color.FromRgba(160, 150, 142, 235);
        }
    }
}