﻿using System;
using bonus.app.Core.ViewModels.Businessman.Services;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Pages.Businessman.Services
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	[MvxTabbedPagePresentation(Position = TabbedPosition.Tab, Icon = "ic_star", Title = "Услуги")]
	public partial class BusinessmanServicesPage : MvxContentPage<BusinessmanServicesViewModel>
	{
		#region .ctor
		public BusinessmanServicesPage()
		{
			InitializeComponent();
		}
		#endregion
	}
}
