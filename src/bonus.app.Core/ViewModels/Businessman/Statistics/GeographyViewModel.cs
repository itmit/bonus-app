using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace bonus.app.Core.ViewModels.Businessman.Statistics
{
	public class GeographyViewModel : MvxNavigationViewModel
	{
		#region .ctor
		public GeographyViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService)
			: base(logProvider, navigationService)
		{
		}
		#endregion

		#region .prop
		private MvxCommand _openGeographyDetailPageCommand;

		public IMvxCommand OpenGeographyDetailPageCommand
		{
			get
			{
				_openGeographyDetailPageCommand = _openGeographyDetailPageCommand ??
												  new MvxCommand(() =>
												  {
													  NavigationService.Navigate<GeographyDetailViewModel>();
												  });
				return _openGeographyDetailPageCommand;
			}
		}
		#endregion
	}
}
