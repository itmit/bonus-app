﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bonus.app.Core.ViewModels.Businessman.Services;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Pages.Businessman.Services
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CreateServiceStepOnePage : MvxContentPage<CreateServiceStepOneViewModel>
	{
		public CreateServiceStepOnePage()
		{
			InitializeComponent();
		}
	}
}