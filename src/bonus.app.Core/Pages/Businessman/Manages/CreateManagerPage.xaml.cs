using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bonus.app.Core.ViewModels.Businessman.Managers;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Pages.Businessman.Manages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CreateManagerPage : MvxContentPage<CreateManagerViewModel>
	{
		public CreateManagerPage()
		{
			InitializeComponent();
		}
	}
}