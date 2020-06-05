using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bonus.app.Core.ViewModels;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Views.ContentViews.Services
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PicServiceTypeContentView : MvxContentView<PicServiceTypeViewModel>
	{
		public PicServiceTypeContentView()
		{
			InitializeComponent();
		}
	}
}