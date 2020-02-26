using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bonus.app.Core.ViewModels.Businessman.Pay;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Page.Businessman.Pay
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaySubscribesPage : MvxContentPage<PaySubscribesViewModel>
    {
        public PaySubscribesPage()
        {
            InitializeComponent();
        }
    }
}