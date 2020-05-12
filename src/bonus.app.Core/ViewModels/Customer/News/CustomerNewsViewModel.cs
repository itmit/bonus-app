using System;
using System.Threading.Tasks;
using bonus.app.Core.Pages.Customer.News;
using bonus.app.Core.Services;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace bonus.app.Core.ViewModels.Customer.News
{
	public class CustomerNewsViewModel : MvxNavigationViewModel
	{
		private Models.News _selectedNews;
		private MvxCommand _refreshCommand;
		private bool _isRefreshing;
		private readonly INewsService _newsService;
		private MvxObservableCollection<Models.News> _news;

		#region .ctor
		public CustomerNewsViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, INewsService newsService)
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

				NavigationService.Navigate<CustomerNewsDetailsViewModel, Models.News>(value);
			}
		}
	}
}
