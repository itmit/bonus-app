﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bonus.app.Core.ViewModels.Shares;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Page.Customer.Shares
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SelectedSharesPage : MvxContentPage<SelectedSharesViewModel>
	{
		public SelectedSharesPage()
		{
			InitializeComponent();
		}

		private void TapGestureRecognizer_Tapped(object sender, ItemTappedEventArgs e)
		{
			Navigation.PushAsync(new SelectedSharesDetailPage());
		}
	}
}