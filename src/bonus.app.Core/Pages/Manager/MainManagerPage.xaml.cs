﻿using bonus.app.Core.ViewModels.Businessman;
using bonus.app.Core.ViewModels.Manager;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Pages.Manager
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	[MvxMasterDetailPagePresentation(MasterDetailPosition.Root, WrapInNavigationPage = false, Title = "Главная")]
	public partial class MainManagerPage : MvxMasterDetailPage<MainManagerViewModel>
	{
		#region .ctor
		public MainManagerPage()
		{
			InitializeComponent();
		}
		#endregion

		#region Overrided
		/// <summary>Application developers can override this method to provide behavior when the back button is pressed.</summary>
		/// <returns>To be added.</returns>
		/// <remarks>To be added.</remarks>
		protected override bool OnBackButtonPressed() => base.OnBackButtonPressed();
		#endregion
	}
}