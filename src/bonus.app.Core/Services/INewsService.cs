using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using bonus.app.Core.Models;

namespace bonus.app.Core.Services
{
	public interface INewsService
	{
		Task<List<News>> GetNews();

		Task<List<string>> GetNewsImagesSources(Guid uuid);
	}
}
