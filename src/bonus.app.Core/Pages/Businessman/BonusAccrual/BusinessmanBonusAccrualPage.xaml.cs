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

			BarcodeImageView.BarcodeOptions.Width = 225;
			BarcodeImageView.BarcodeOptions.Height = 225;
		}
		#endregion

		/// <summary>Application developers can override this method to provide behavior when the back button is pressed.</summary>
		/// <returns>To be added.</returns>
		/// <remarks>To be added.</remarks>
		protected override bool OnBackButtonPressed()
		{
			Navigation.PopModalAsync();
			return true;
		}
	}
}
