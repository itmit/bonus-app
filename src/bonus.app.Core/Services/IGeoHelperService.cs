using System.Collections.Generic;
using System.Threading.Tasks;
using bonus.app.Core.Dtos.GeoHelper;
using bonus.app.Core.Models;
using bonus.app.Core.Models.GeoHelperModels;

namespace bonus.app.Core.Services
{
	public interface IGeoHelperService
	{
		#region Overridable
		Task<List<City>> GetCities(LocaleDto locale, CityFilterDto filter = null, PaginationRequestDto pagination = null, OrderDto order = null);

		Task<List<Country>> GetCountries(LocaleDto locale);
		#endregion
	}
}
