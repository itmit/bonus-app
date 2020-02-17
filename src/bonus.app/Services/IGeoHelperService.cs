using System.Collections.Generic;
using System.Threading.Tasks;
using bonus.app.Core.Dto;
using bonus.app.Core.Dto.GeoHelper;
using bonus.app.Core.Models;

namespace bonus.app.Core.Services
{
	public interface IGeoHelperService
	{
		Task<List<Country>> GetCountries(LocaleDto locale);

		Task<List<City>> GetCities(LocaleDto locale, 
								 CityFilterDto filter = null, 
								 PaginationRequestDto pagination = null,
								 OrderDto order = null);
	}
}
