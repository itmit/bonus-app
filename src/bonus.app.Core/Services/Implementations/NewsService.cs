using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using bonus.app.Core.Dtos;
using bonus.app.Core.Models;
using bonus.app.Core.Services.Interfaces;

namespace bonus.app.Core.Services.Implementations
{
	public class NewsService : BaseService, INewsService
	{
		#region Data
		#region Consts
		private const string GetNewsImagesSourcesUri = "http://bonus.itmit-studio.ru/api/news/{0}";

		private const string GetNewsUri = "http://bonus.itmit-studio.ru/api/news";
		#endregion
		#endregion

		#region .ctor
		public NewsService(IAuthService authService)
			: base(authService)
		{
		}
		#endregion

		#region INewsService members
		public async Task<List<News>> GetNews()
		{
			var news = await GetAsync<List<News>>(GetNewsUri);

			foreach (var newse in news)
			{
				newse.ImageSource = Domain + newse.ImageSource;
			}

			return news;
		}

		public async Task<List<string>> GetNewsImagesSources(Guid uuid)
		{
			var images = await GetAsync<List<NewsImage>>(string.Format(GetNewsImagesSourcesUri, uuid));
			var result = new List<string>();
			foreach (var image in images)
			{
				result.Add(Domain + image.Image);
			}

			return result;
		}
		#endregion
	}
}
