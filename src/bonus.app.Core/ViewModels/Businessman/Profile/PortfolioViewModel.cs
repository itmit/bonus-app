using System;
using bonus.app.Core.Models;
using bonus.app.Core.Models.UserModels;
using bonus.app.Core.Services;
using MvvmCross.Commands;
using MvvmCross.ViewModels;

namespace bonus.app.Core.ViewModels.Businessman.Profile
{
	public class PortfolioViewModel : MvxViewModel
	{
		#region Data
		#region Fields
		private readonly IProfileService _profileService;
		private MvxCommand _removeImageCommand;
		#endregion
		#endregion

		#region .ctor
		public PortfolioViewModel(IProfileService profileService) => _profileService = profileService;
		#endregion

		#region Properties
		public string ImageName
		{
			get;
			set;
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
