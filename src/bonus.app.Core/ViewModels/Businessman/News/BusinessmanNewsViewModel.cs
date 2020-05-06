using System;
using System.Threading.Tasks;
using bonus.app.Core.Services;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace bonus.app.Core.ViewModels.Businessman.News
{
	public class BusinessmanNewsViewModel : MvxNavigationViewModel
	{
		private MvxObservableCollection<Models.News> _news;
		private Models.News _selectedNews;
		private readonly INewsService _newsService;

		#region .ctor
		public BusinessmanNewsViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, INewsService newsService)
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

				NavigationService.Navigate<BusinessmanNewsDetailsViewModel, Models.News>(value);

				SetProperty(ref _selectedNews, null);
			}
		}
		#endregion
	}
}
