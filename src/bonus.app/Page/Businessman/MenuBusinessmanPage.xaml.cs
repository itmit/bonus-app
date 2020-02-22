using System;
using bonus.app.Core.ViewModels.Businessman;
using bonus.app.Page;
using bonus.app.Views;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BusinessmanProfilePage = bonus.app.Core.Page.Businessman.Profile.BusinessmanProfilePage;

namespace bonus.app.Core.Page.Businessman
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	[MvxMasterDetailPagePresentation(Position = MasterDetailPosition.Master, WrapInNavigationPage = false, Title = "Меню")]
    public partial class MenuBusinessmanPage : MvxContentPage<MenuBusinessmanViewModel>
    {
        public MenuBusinessmanPage()
        {
            InitializeComponent();

            BackgroundColor = Color.FromRgba(160, 150, 142, 235);
        }
	}
}