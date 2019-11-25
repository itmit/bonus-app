﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bonus.app.Page
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AuthorizationRecoveryPage : ContentPage
    {
        public AuthorizationRecoveryPage()
        {
            InitializeComponent();
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new BusinessmanAndBuyerPage());
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RecoveryDetailPage());
        }
    }
}