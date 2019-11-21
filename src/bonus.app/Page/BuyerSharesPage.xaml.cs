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
    public partial class BuyerSharesPage : ContentPage
    {
        public BuyerSharesPage()
        {
            InitializeComponent();

            BackgroundColor = Color.FromRgba(196, 196, 196, 0.2);
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new BayerSharesDetailPage());
        }
    }
}