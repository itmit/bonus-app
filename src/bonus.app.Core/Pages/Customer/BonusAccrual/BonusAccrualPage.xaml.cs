using bonus.app.Core.ViewModels.Customer.BonusAccrual;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Pages.Customer.BonusAccrual
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	[MvxTabbedPagePresentation(Position = TabbedPosition.Tab, Icon = "ic_charges", Title = "Начисления")]
	public partial class BonusAccrualPage : MvxContentPage<CustomerBonusAccrualViewModel>
	{
		#region .ctor
		public BonusAccrualPage()
		{
			InitializeComponent();

			BarcodeImageView.BarcodeOptions.Width = 225;
			BarcodeImageView.BarcodeOptions.Height = 225;
		}
		#endregion
	}
}
