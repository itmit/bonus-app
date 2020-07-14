using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using bonus.app.Core.Dtos.BusinessmanDtos;
using bonus.app.Core.Dtos.CustomerDtos;
using bonus.app.Core.Models;
using bonus.app.Core.Models.UserModels;

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
		Task<PortfolioImage> AddImageToPortfolio(string imageSource);

		Task<User> Edit(EditBusinessmanDto arguments, string imagePath);

		Task<User> Edit(EditCustomerDto arguments, string imagePath);

		Task<List<PortfolioImage>> GetPortfolio();

		Task<List<PortfolioImage>> GetPortfolio(Guid uuid);

		Task<User> GetUser(Guid uuid, int? stockId, int? serviceId);
		Task<User> GetUser(Guid uuid);

		Task<bool> RemoveImageFromPortfolio(Guid uuid);
		#endregion
	}
}
