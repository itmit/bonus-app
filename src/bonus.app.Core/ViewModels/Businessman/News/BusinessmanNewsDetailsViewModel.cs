using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace bonus.app.Core.ViewModels.Businessman.News
{
	public class BusinessmanNewsDetailsViewModel : MvxViewModel<Models.News>
	{
		private Models.News _news;

		public Models.News News
		{
			get => _news;
			private set => SetProperty(ref _news, value);
		}

		public override void Prepare(Models.News parameter)
		{
			News = parameter;
		}
	}
}
