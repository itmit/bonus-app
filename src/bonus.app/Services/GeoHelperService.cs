using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using bonus.app.Core.Dto.GeoHelper;
using bonus.app.Core.Helpers;
using bonus.app.Core.Models;
using Newtonsoft.Json;

namespace bonus.app.Core.Services
{
	public class GeoHelperService : IGeoHelperService
	{
		private const string GetCountriesUri = "http://geohelper.info/api/v1/countries?apiKey={0}&locale[lang]={1}&locale[fallbackLang]={2}";

		public async Task<List<Country>> GetCountries(LocaleDto locale)
		{
			using (var client = new HttpClient())
			{
				var resp = await client.GetAsync(string.Format(GetCountriesUri, Secrets.GeoHelperApiKey, locale.Lang, locale.FallbackLang));

				var json = await resp.Content.ReadAsStringAsync();
				Debug.WriteLine(json);

				if (string.IsNullOrEmpty(json))
				{
					return null;
				}

				var data = JsonConvert.DeserializeObject<GeoHelperResponseDto<Country>>(json);

				if (data.Success)
				{
					return data.Result.ToList();
				}

				return null;
			}
		}

		public Task<List<City>> GetCity(LocaleDto locale, CityFilterDto filter = null, PaginationRequestDto pagination = null, OrderDto order = null) => throw new System.NotImplementedException();
	}
}
