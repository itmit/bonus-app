﻿using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace bonus.app.Core.ViewModels.Businessman.Services
{
	public class EditBusinessmanServicesViewModel : MvxNavigationViewModel
	{
		public EditBusinessmanServicesViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService)
			: base(logProvider, navigationService)
		{
		}
	}
}