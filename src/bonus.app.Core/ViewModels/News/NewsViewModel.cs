using System;
using System.Threading.Tasks;
using bonus.app.Core.Services;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace bonus.app.Core.ViewModels.News
{
	public class NewsViewModel : MvxNavigationViewModel
	{
		private MvxObservableCollection<Models.News> _news;
		private Models.News _selectedNews;
		private readonly INewsService _newsService;
		private bool _isRefreshing;
		private MvxCommand _refreshCommand;

		#region .ctor
		public NewsViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, INewsService newsService)
			: base(logProvider, navigationService)
		{
			_newsService = newsService;
		}
		#endregion

		public override async Task Initialize() 
		{
			await base.Initialize();
			
			try
			{
				News = new MvxObservableCollection<Models.News>(await _newsService.GetNews());
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}

		#region Public
		public bool IsRefreshing
		{
			get => _isRefreshing;
			set => SetProperty(ref _isRefreshing, value);
		}

		public MvxCommand RefreshCommand
		{
			get
			{
				_refreshCommand = _refreshCommand ?? new MvxCommand(async () =>
				{
					IsRefreshing = true; try
					{
						News = new MvxObservableCollection<Models.News>(await _newsService.GetNews());
					}
					catch (Exception e)
					{
						Console.WriteLine(e);
					}
					IsRefreshing = false;
				});
				return _refreshCommand;
			}
		}

		public MvxObservableCollection<Models.News> News
		{
			get => _news;
			private set => SetProperty(ref _news, value);
		}

		public Models.News SelectedNews
		{
			get => _selectedNews;
			set
			{
				if (value == null)
				{
					return;
				}

				SetProperty(ref _selectedNews, value);

				NavigationService.Navigate<NewsDetailsViewModel, Models.News>(value);

				SetProperty(ref _selectedNews, null);
			}
		}
		#endregion
	}
}
