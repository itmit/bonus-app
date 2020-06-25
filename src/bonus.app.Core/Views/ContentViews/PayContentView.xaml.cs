using System;
using bonus.app.Core.ViewModels.Businessman.Pay;
using Xamarin.Forms;

namespace bonus.app.Core.Views.ContentViews
{
	public partial class PayContentView : ContentView
	{
		#region .ctor
		public PayContentView()
		{
			InitializeComponent();
		}
		#endregion

		public PaySubscribesViewModel ParentViewModel
		{
			get;
			set;
		}

		private void Button_OnClicked(object sender, EventArgs e)
		{
			ParentViewModel.ChangeRateCommand.Execute(BindingContext);
		}
	}
}
