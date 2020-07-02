using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bonus.app.Core.ViewModels.Manager;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Pages.Manager
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	[MvxTabbedPagePresentation(Position = TabbedPosition.Tab, Icon = "ic_profile", Title = "Профиль")]
	public partial class ProfileManagerPage : MvxContentPage<ProfileManagerViewModel>
	{
		public ProfileManagerPage()
		{
			InitializeComponent();
		}
	}
}