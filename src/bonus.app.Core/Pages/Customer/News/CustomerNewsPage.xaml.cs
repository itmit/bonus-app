using bonus.app.Core.ViewModels.Customer.News;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Pages.Customer.News
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	[MvxTabbedPagePresentation(Position = TabbedPosition.Tab, Icon = "ic_news", Title = "Новости")]
	public partial class CustomerNewsPage : MvxContentPage<CustomerNewsViewModel>
	{
		#region .ctor
		public CustomerNewsPage()
		{
			InitializeComponent();
		}
		#endregion

		#region Private
        private void CollectionView_SelectionChanged(System.Object sender, Xamarin.Forms.SelectionChangedEventArgs e)
		{
			((CollectionView) sender).SelectedItem = null;
		}
        #endregion
    }
}
