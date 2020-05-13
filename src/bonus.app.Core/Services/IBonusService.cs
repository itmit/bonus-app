using System.Collections.Generic;
using System.Threading.Tasks;
using bonus.app.Core.Dtos.BusinessmanDtos;
using bonus.app.Core.Models;

namespace bonus.app.Core.Services
{
	public interface IBonusService
	{
		#region Overridable
		Task<bool> AccrueAndWriteOffBonuses(AccrueAndWriteOffBonusesDto requestDto);

		Task<List<AccrualBonuses>> GetMyBonuses();
		#endregion
	}
}
