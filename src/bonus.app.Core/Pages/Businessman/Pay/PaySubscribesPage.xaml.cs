using bonus.app.Core.ViewModels.Businessman.Pay;
using bonus.app.Core.Views.ContentViews;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Pages.Businessman.Pay
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PaySubscribesPage : MvxContentPage<PaySubscribesViewModel>
	{
		#region .ctor
		public PaySubscribesPage()
		{
			InitializeComponent();
		}
		#endregion

		/// <summary>When overridden, allows application developers to customize behavior immediately prior to the <see cref="T:Xamarin.Forms.Page" /> becoming visible.</summary>
		/// <remarks>To be added.</remarks>
		protected override void OnAppearing()
		{
			RateCollection.ItemTemplate = new DataTemplate(() => new PayContentView
			{
				ParentViewModel = ViewModel
			});
			base.OnAppearing();
		}
	}
}
