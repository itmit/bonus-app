using System.Threading.Tasks;
using bonus.app.Core.Dtos.BusinessmanDtos;

namespace bonus.app.Core.Services
{
	public interface IBonusService
	{
		#region Overridable
		Task<bool> AccrueAndWriteOffBonuses(AccrueAndWriteOffBonusesDto requestDto);
		#endregion
	}
}
