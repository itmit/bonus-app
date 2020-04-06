﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Views.ContentViews
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PicPhotoContentView : ContentView
	{
		public string Placeholder
		{
			get;
			set;
		}

		public PicPhotoContentView()
		{
			InitializeComponent();
			PlaceholderLabel.Text = Placeholder;
		}
	}
}