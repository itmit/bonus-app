using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bonus.app.Core.ViewModels;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bonus.app.Page
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	[MvxMasterDetailPagePresentation(MasterDetailPosition.Detail)]
    public partial class TariffPage : MvxContentPage<TariffViewModel>
    {
        public TariffPage()
        {
            InitializeComponent();
			Tariff.BackgroundColor = Color.FromRgba(160, 150, 142, 0.1);
            Frame.BackgroundColor = Color.FromRgba(160, 150, 142, 0.1);
        }
    }
}