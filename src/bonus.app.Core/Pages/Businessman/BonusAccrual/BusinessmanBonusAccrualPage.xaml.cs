using bonus.app.Core.ViewModels.Businessman.BonusAccrual;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;

namespace bonus.app.Core.Pages.Businessman.BonusAccrual
{
	[MvxTabbedPagePresentation(Position = TabbedPosition.Tab, Icon = "ic_charges", Title = "Бонусы")]
	public partial class BusinessmanBonusAccrualPage : MvxContentPage<BusinessmanBonusAccrualViewModel>
	{
		#region .ctor
		public BusinessmanBonusAccrualPage()
		{
			InitializeComponent();

			BarcodeImageView.BarcodeOptions.Width = 225;
			BarcodeImageView.BarcodeOptions.Height = 225;
		}
		#endregion
	}
}
