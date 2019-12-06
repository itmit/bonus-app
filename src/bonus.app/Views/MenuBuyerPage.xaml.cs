﻿using bonus.app.Page;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bonus.app.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuBuyerPage : ContentPage
    {
        public MenuBuyerPage()
        {
            InitializeComponent();

            BackgroundColor = Color.FromRgba(160, 150, 142, 235);
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new BuyerProfilePage());
        }

        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            
        }

        private void TapGestureRecognizer_Tapped_2(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SupportPage());
        }
    }
}