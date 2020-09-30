using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using bonus.app.Core.Models;

namespace bonus.app.Core.Services.Interfaces
{
	public interface INewsService
	{
		#region Overridable
		Task<List<News>> GetNews();

		Task<List<string>> GetNewsImagesSources(Guid uuid);
		#endregion
	}
}
