using System.Collections.ObjectModel;
using bonus.app.Core.ViewModels;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Page
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditBusinessmanServicesPage : MvxContentPage<EditBusinessmanServicesViewModel>
    {
        public EditBusinessmanServicesPage()
        {
            InitializeComponent();

			var source = new ObservableCollection<string>();
            source.Add("0");
            source.Add("0");
            source.Add("0");
			RepeaterView.ItemsSource = source;
		}
    }
}