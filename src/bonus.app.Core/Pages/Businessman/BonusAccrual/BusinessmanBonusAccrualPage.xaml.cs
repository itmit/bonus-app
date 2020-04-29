using bonus.app.Core.ViewModels.Businessman.BonusAccrual;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;

namespace bonus.app.Core.Pages.Businessman.BonusAccrual
{
	[MvxTabbedPagePresentation(Position = TabbedPosition.Tab, Icon = "ic_charges", WrapInNavigationPage = false, Title = "Начисления")]
	public partial class BusinessmanBonusAccrualPage : MvxContentPage<BusinessmanBonusAccrualViewModel>
	{
		#region .ctor
		public BusinessmanBonusAccrualPage()
		{
			InitializeComponent();
		}
		#endregion
	}
}
