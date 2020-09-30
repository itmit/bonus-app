using bonus.app.Core.Services;
using bonus.app.Core.Services.Interfaces;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace bonus.app.Core.ViewModels.Businessman.Statistics
{
	public class GeographyViewModel : MvxViewModel
	{
		#region .ctor
		public GeographyViewModel(IMvxNavigationService navigationService) => _navigationService = navigationService;
		#endregion

		#region .prop
		private IMvxCommand<GeographyStatisticsType> _openGeographyDetailPageCommand;
		private readonly IMvxNavigationService _navigationService;

		public IMvxCommand<GeographyStatisticsType> OpenGeographyDetailPageCommand
		{
			get
			{
				_openGeographyDetailPageCommand = _openGeographyDetailPageCommand ??
												  new MvxCommand<GeographyStatisticsType>(type =>
												  {
													  _navigationService.Navigate<GeographyDetailViewModel, GeographyStatisticsType>(type);
												  });
				return _openGeographyDetailPageCommand;
			}
		}
		#endregion
	}
}
