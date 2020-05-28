using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using bonus.app.Core.Dtos.BusinessmanDtos;
using bonus.app.Core.Dtos.CustomerDtos;
using bonus.app.Core.Models;

namespace bonus.app.Core.Services
{
	public interface IProfileService
	{
		#region Properties
		string Error
		{
			get;
		}

		Dictionary<string, string[]> ErrorDetails
		{
			get;
		}
		#endregion

		#region Overridable
		Task<User> Edit(EditBusinessmanDto arguments, string imagePath);

		Task<User> Edit(EditCustomerDto arguments, string imagePath);

		Task<PortfolioImage> AddImageToPortfolio(string imageSource);

		Task<User> GetUser(Guid uuid);

		Task<List<PortfolioImage>> GetPortfolio();

		Task<List<PortfolioImage>> GetPortfolio(Guid uuid);
		Task<bool> RemoveImageFromPortfolio(Guid uuid);
		#endregion
	}
}
