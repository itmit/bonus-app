using System;
using System.Threading.Tasks;
using bonus.app.Core.Dtos.BusinessmanDtos;
using bonus.app.Core.Models;

namespace bonus.app.Core.Services
{
	public interface IBonusService
	{
		Task<bool> AccrueAndWriteOffBonuses(AccrueAndWriteOffBonusesDto requestDto);
	}
}
