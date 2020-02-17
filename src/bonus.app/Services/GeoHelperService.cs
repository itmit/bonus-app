using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
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
		private const string GetCitiesUri = "http://geohelper.info/api/v1/cities?apiKey={0}&locale[lang]={1}&locale[fallbackLang]={2}";

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

		public async Task<List<City>> GetCities(LocaleDto locale, CityFilterDto filter = null, PaginationRequestDto pagination = null, OrderDto order = null) 
		{
			using (var client = new HttpClient())
			{
				var uri = string.Format(FormatUriForGetCities(filter, pagination, order), Secrets.GeoHelperApiKey, locale.Lang, locale.FallbackLang);
				Debug.WriteLine(uri);
				var resp = await client.GetAsync(uri);

				var json = await resp.Content.ReadAsStringAsync();
				Debug.WriteLine(json);

				if (string.IsNullOrEmpty(json))
				{
					return null;
				}

				var data = JsonConvert.DeserializeObject<GeoHelperResponseDto<City>>(json);

				if (data.Success)
				{
					return data.Result.ToList();
				}

				return null;
			}
		}

		private string FormatUriForGetCities(CityFilterDto filter, PaginationRequestDto pagination, OrderDto order)
		{
			var sb = new StringBuilder(GetCitiesUri);
			if (filter != null)
			{
				if (!string.IsNullOrEmpty(filter.CountryIso))
				{
					sb.Append("&filter[countryIso]=");
					sb.Append(filter.CountryIso);
				}

				if (filter.Id > 0)
				{
					sb.Append("&filter[id]=");
					sb.Append(filter.Id);
				}

				if (filter.Ids != null)
				{
					for (int i = 0; i < filter.Ids.Length; i++)
					{
						sb.Append("&filter[ids][");
						sb.Append(i);
						sb.Append("]=");
						sb.Append(filter.Ids[i]);
					}
				}

				if (!string.IsNullOrEmpty(filter.Name))
				{
					sb.Append("&filter[name]=");
					sb.Append(filter.Name);
				}

				if (!string.IsNullOrEmpty(filter.NameStrictLanguage))
				{
					sb.Append("&filter[nameStrictLanguage]=");
					sb.Append(filter.NameStrictLanguage);
				}

				if (filter.RegionId > 0)
				{
					sb.Append("&filter[regionId]=");
					sb.Append(filter.RegionId);
				}

				if (filter.RegionCodes != null)
				{
					for (int i = 0; i < filter.RegionCodes.Length; i++)
					{
						sb.Append("&filter[regionCodes][");
						sb.Append(i);
						sb.Append("]=");
						sb.Append(filter.RegionCodes[i]);
					}
				}
			}

			if (pagination != null)
			{
				sb.Append("&pagination[page]=");
				sb.Append(pagination.Page);
				sb.Append("&pagination[limit]=");
				sb.Append(pagination.Limit);
			}

			if (order != null)
			{
				if (!string.IsNullOrEmpty(order.By))
				{
					sb.Append("&order[by]=");
					sb.Append(order.By);
				}

				if (!string.IsNullOrEmpty(order.Dir))
				{
					sb.Append("&order[dir]=");
					sb.Append(order.Dir);
				}
			}

			return sb.ToString();
		}
	}
}
