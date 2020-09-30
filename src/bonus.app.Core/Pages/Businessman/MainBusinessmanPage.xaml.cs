using bonus.app.Core.ViewModels.Businessman;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Pages.Businessman
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	[MvxMasterDetailPagePresentation(MasterDetailPosition.Root, WrapInNavigationPage = false, Title = "Главная")]
	public partial class MainBusinessmanPage : MvxMasterDetailPage<MainBusinessmanViewModel>
	{
		private bool _firstTime = true;

		#region .ctor
		public MainBusinessmanPage()
		{
			InitializeComponent();
		}
		#endregion

		/// <summary>Event that is raised when a detail appears.</summary>
		/// <remarks>To be added.</remarks>
		protected override void OnAppearing()
		{
			if (_firstTime)
			{
				ViewModel.ShowMainTabbedBusinessmanViewModelCommand.Execute();
				ViewModel.ShowMenuBusinessmanViewModelCommand.Execute();

				_firstTime = false;
			}

			base.OnAppearing();
		}
	}
}
