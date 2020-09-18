using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace bonus.app.Core.ViewModels
{
	public class PhotoViewModel : MvxViewModel<string>
	{
		private string _imageSource;
		private MvxCommand _closePhotoCommand;
		private readonly IMvxNavigationService _navigationService;

		public PhotoViewModel(IMvxNavigationService navigationService) => _navigationService = navigationService;

		public override void Prepare(string parameter)
		{
			ImageSource = parameter;
		}

		public string ImageSource
		{
			get => _imageSource;
			set => SetProperty(ref _imageSource, value);
		}
	}
}
