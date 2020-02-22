﻿using System;
using bonus.app.Core.ViewModels;
using bonus.app.Core.ViewModels.Businessman.Profile;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Page.Businessman.Profile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [MvxTabbedPagePresentation(Position = TabbedPosition.Tab, 
		Icon = "ic_profile", 
		WrapInNavigationPage = false,
		Title = "Профиль")]
    public partial class BusinessmanProfilePage : MvxContentPage<BusinessmanProfileViewModel>
    {
        public BusinessmanProfilePage()
        {
            InitializeComponent();
        }
	}
}