using System;
using bonus.app.Core.Models;
using bonus.app.Core.Models.UserModels;
using bonus.app.Core.Services;
using bonus.app.Core.Services.Interfaces;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace bonus.app.Core.ViewModels.Businessman.Profile
{
	public class PortfolioViewModel : MvxViewModel
	{
		#region Data
		#region Fields
		private readonly IProfileService _profileService;
		private MvxCommand _removeImageCommand;
		private MvxCommand _showImageCommand;
		private IMvxNavigationService _navigationService;
		#endregion
		#endregion

		#region .ctor
		public PortfolioViewModel(IProfileService profileService, IMvxNavigationService navigationService)
		{
			_profileService = profileService;
			_navigationService = navigationService;
		}
		#endregion

		#region Properties
		public string ImageName
		{
			get;
			set;
		}

		public MvxCommand ShowImageCommand
		{
			get
			{
				_showImageCommand = _showImageCommand ?? new MvxCommand(() =>
				{
					_navigationService.Navigate<PhotoViewModel, string>(ImageSource);
				});
				return _showImageCommand;
			}
		}

		public string ImageSource
		{
			get;
			set;
		}

		public IPortfolioParentViewModel ParentViewModel
		{
			get;
			set;
		}

		public PortfolioImage PortfolioImage
		{
			get;
			set;
		}

		public MvxCommand RemoveImageCommand
		{
			get
			{
				_removeImageCommand = _removeImageCommand ??
									  new MvxCommand(async () =>
									  {
										  try
										  {
											  if (await _profileService.RemoveImageFromPortfolio(PortfolioImage.Uuid))
											  {
												  ParentViewModel.RemovedPortfolioImage(this);
											  }
										  }
										  catch (Exception e)
										  {
											  Console.WriteLine(e);
										  }
									  });
				return _removeImageCommand;
			}
		}
		#endregion
	}
}
