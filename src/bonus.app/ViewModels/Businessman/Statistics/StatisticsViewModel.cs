using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace bonus.app.Core.ViewModels.Businessman.Statistics
{
	public class StatisticsViewModel : MvxNavigationViewModel
	{
        

        public StatisticsViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService)
			: base(logProvider, navigationService)
		{
		}

        #region Command
        #region Sales
        private MvxCommand _openSalesTypesPageCommand;
        public IMvxCommand OpenSalesTypesPageCommand
		{
			get
			{
				_openSalesTypesPageCommand = _openSalesTypesPageCommand ??
										new MvxCommand(() =>
										{
											NavigationService.Navigate<SalesTypesViewModel>();
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
											NavigationService.Navigate<GenderAgeViewModel>();
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
											NavigationService.Navigate<GeographyViewModel>();
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
											NavigationService.Navigate<ViewsStockViewModel>();
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
											NavigationService.Navigate<ViewsProfileViewModel>();
										});
				return _openViewsProfilePageCommand;
			}
		}
		#endregion

		#region Transitions
		private MvxCommand _openTransitionsProfilePageCommand;
		public IMvxCommand OpenTransitionsProfilePageCommand
		{
			get
			{
				_openTransitionsProfilePageCommand = _openTransitionsProfilePageCommand ??
										new MvxCommand(() =>
										{
											NavigationService.Navigate<TransitionsProfileViewModel>();
										});
				return _openTransitionsProfilePageCommand;
			}
		}
		#endregion
		#endregion
	}
}
