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
    public partial class BuyerProfilePage : ContentPage
    {
        public BuyerProfilePage()
        {
            InitializeComponent();
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new EditProfileBauerPage());
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SubscribePage());
        }
    }
}