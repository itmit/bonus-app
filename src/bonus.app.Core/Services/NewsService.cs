using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using bonus.app.Core.Models;

namespace bonus.app.Core.Services
{
	public class NewsService : BaseService, INewsService
	{
		public NewsService(IAuthService authService)
			: base(authService)
		{
		}

		private const string GetNewsUri = "http://bonus.itmit-studio.ru/api/news";

		public async Task<List<News>> GetNews()
		{
			var news = await GetAsync<List<News>>(GetNewsUri);

			foreach (var newse in news)
			{
				newse.ImageSource = Domain + newse.ImageSource;
			}

			return news;
		}
	}
}
