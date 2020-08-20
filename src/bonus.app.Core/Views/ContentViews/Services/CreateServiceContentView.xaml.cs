using bonus.app.Core.ViewModels.Businessman.Services;
using MvvmCross.Forms.Views;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Views.ContentViews.Services
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CreateServiceContentView : MvxContentView<CreateServiceContentViewModel>
	{
		#region .ctor
		public CreateServiceContentView()
		{
			InitializeComponent();
		}
		#endregion
	}
}
