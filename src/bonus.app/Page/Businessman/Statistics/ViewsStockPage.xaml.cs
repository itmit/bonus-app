using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bonus.app.Core.ViewModels.Businessman.Statistics;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Page.Businessman.Statistics
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewsStockPage : MvxContentPage<ViewsStockViewsStockViewModel>
    {
        public ViewsStockPage()
        {
            InitializeComponent();
        }
    }
}