using System;
using System.Threading.Tasks;
using bonus.app.Core.Services;
using MvvmCross.ViewModels;

namespace bonus.app.Core.ViewModels.News
{
	public class NewsDetailsViewModel : MvxViewModel<Models.News>
	{
		private Models.News _news;
		private readonly INewsService _newsService;
		private MvxObservableCollection<string> _images;

		public NewsDetailsViewModel(INewsService newsService)
		{
			_newsService = newsService;
		}

		public Models.News News
		{
			get => _news;
			private set => SetProperty(ref _news, value);
		}

		public override async Task Initialize() 
		{
			await base.Initialize();
			try
			{
				Images = new MvxObservableCollection<string>(await _newsService.GetNewsImagesSources(News.Uuid));
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}

		public MvxObservableCollection<string> Images
		{
			get => _images;
			private set => SetProperty(ref _images, value);
		}

		public override void Prepare(Models.News parameter)
		{
			News = parameter;
		}
	}
}
