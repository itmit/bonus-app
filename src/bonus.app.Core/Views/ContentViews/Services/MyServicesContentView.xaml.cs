using System;
using bonus.app.Core.ViewModels.Businessman.Services;
using MvvmCross.Forms.Views;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Views.ContentViews.Services
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MyServicesContentView : MvxContentView<MyServicesViewModel>
	{
		#region .ctor
		public MyServicesContentView()
		{
			InitializeComponent();
		}
		#endregion
	}
}
