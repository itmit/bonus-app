using MvvmCross.ViewModels;

namespace bonus.app.Core.ViewModels
{
	public class PhotoViewModel : MvxViewModel<string>
	{
		private string _imageSource;

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
