using bonus.app.Core.ViewModels.Businessman.Services;
using MvvmCross.Forms.Views;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Pages.Businessman.Services
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EditBusinessmanServicesPage : MvxContentPage<EditBusinessmanServicesViewModel>
	{
		#region .ctor
		public EditBusinessmanServicesPage()
		{
			InitializeComponent();
		}
		#endregion
	}
}
