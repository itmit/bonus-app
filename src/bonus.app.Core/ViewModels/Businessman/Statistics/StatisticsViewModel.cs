using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace bonus.app.Core.ViewModels.Businessman.Statistics
{
	public class StatisticsViewModel : MvxViewModel
	{
		#region .ctor
		public StatisticsViewModel(IMvxNavigationService navigationService)
		{
			_navigationService = navigationService;
		}
		#endregion

		#region Sales
		private MvxCommand _openSalesTypesPageCommand;

		public IMvxCommand OpenSalesTypesPageCommand
		{
			get
			{
				_openSalesTypesPageCommand = _openSalesTypesPageCommand ??
											 new MvxCommand(() =>
											 {
												 _navigationService.Navigate<SalesTypesViewModel>();
											 });
				return _openSalesTypesPageCommand;
			}
		}
		#endregion

		#region Gender
		private MvxCommand _openGenderAgePageCommand;

		public IMvxCommand OpenGenderAgePageCommand
		{
			get
			{
				_openGenderAgePageCommand = _openGenderAgePageCommand ??
											new MvxCommand(() =>
											{
												_navigationService.Navigate<GenderAgeViewModel>();
											});
				return _openGenderAgePageCommand;
			}
		}
		#endregion

		#region Geography
		private MvxCommand _openGeographyPageCommand;

		public IMvxCommand OpenGeographyPageCommand
		{
			get
			{
				_openGeographyPageCommand = _openGeographyPageCommand ??
											new MvxCommand(() =>
											{
												_navigationService.Navigate<GeographyViewModel>();
											});
				return _openGeographyPageCommand;
			}
		}
		#endregion

		#region Stock
		private MvxCommand _openViewsStockPageCommand;

		public IMvxCommand OpenViewsStockPageCommand
		{
			get
			{
				_openViewsStockPageCommand = _openViewsStockPageCommand ??
											 new MvxCommand(() =>
											 {
												 _navigationService.Navigate<ViewsStockViewModel>();
											 });
				return _openViewsStockPageCommand;
			}
		}
		#endregion

		#region Profile
		private MvxCommand _openViewsProfilePageCommand;

		public IMvxCommand OpenViewsProfilePageCommand
		{
			get
			{
				_openViewsProfilePageCommand = _openViewsProfilePageCommand ??
											   new MvxCommand(() =>
											   {
												   _navigationService.Navigate<ViewsProfileViewModel>();
											   });
				return _openViewsProfilePageCommand;
			}
		}
		#endregion

		#region Transitions
		private MvxCommand _openTransitionsProfilePageCommand;
		private readonly IMvxNavigationService _navigationService;

		public IMvxCommand OpenTransitionsProfilePageCommand
		{
			get
			{
				_openTransitionsProfilePageCommand = _openTransitionsProfilePageCommand ??
													 new MvxCommand(() =>
													 {
														 _navigationService.Navigate<TransitionsProfileViewModel>();
													 });
				return _openTransitionsProfilePageCommand;
			}
		}
		#endregion
	}
}
