using bonus.app.Core.ViewModels.Businessman.Shares;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Page
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditorStockPage : MvxContentPage<EditorStockViewModel>
    {
        public EditorStockPage()
        {
            InitializeComponent();
        }
    }
}