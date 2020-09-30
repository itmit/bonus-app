using System;
using System.Threading.Tasks;
using bonus.app.Core.Models;
using bonus.app.Core.Services;
using bonus.app.Core.Services.Interfaces;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace bonus.app.Core.ViewModels.News
{
	public class NewsDetailsViewModel : MvxViewModel<Models.News>
	{
		#region Data
		#region Fields
		private MvxObservableCollection<ImageModel> _images;
		private Models.News _news;
		private readonly INewsService _newsService;
		private MvxCommand<string> _showPhotoCommand;
		private IMvxNavigationService _navigationService;
		#endregion
		#endregion

		#region .ctor
		public NewsDetailsViewModel(INewsService newsService, IMvxNavigationService navigationService)
		{ 
			_newsService = newsService;
			_navigationService = navigationService;
		}
		#endregion

		#region Properties
		public MvxCommand<string> ShowPhotoCommand
		{
			get
			{
				_showPhotoCommand = _showPhotoCommand ?? new MvxCommand<string>(src =>
				{
					_navigationService.Navigate<PhotoViewModel, string>(src);
				});
				return _showPhotoCommand;
			}
		}

		public MvxObservableCollection<ImageModel> Images
		{
			get => _images;
			private set => SetProperty(ref _images, value);
		}

		public Models.News News
		{
			get => _news;
			private set => SetProperty(ref _news, value);
		}
		#endregion

		#region Overrided
		public override async Task Initialize()
		{
			await base.Initialize();
			try
			{
				Images = new MvxObservableCollection<ImageModel>();
				foreach (var src in await _newsService.GetNewsImagesSources(News.Uuid))
				{
					Images.Add(new ImageModel(src));
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}

		public override void Prepare(Models.News parameter)
		{
			News = parameter;
		}
		#endregion
	}
}
