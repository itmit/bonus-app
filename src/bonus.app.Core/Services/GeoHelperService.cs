using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using bonus.app.Core.Dtos.GeoHelper;
using bonus.app.Core.Helpers;
using bonus.app.Core.Models;
using bonus.app.Core.Models.GeoHelperModels;
using MonkeyCache.FileStore;
using Newtonsoft.Json;
using Xamarin.Essentials;

namespace bonus.app.Core.Services
{
	public class GeoHelperService : IGeoHelperService
	{
		#region Data
		#region Consts
		private const string GetCitiesUri = "http://geohelper.info/api/v1/cities?apiKey={0}&locale[lang]={1}&locale[fallbackLang]={2}";
		private const string GetCountriesUri = "http://geohelper.info/api/v1/countries?apiKey={0}&locale[lang]={1}&locale[fallbackLang]={2}";
		#endregion
		#endregion

		#region IGeoHelperService members
		public async Task<List<City>> GetCities(LocaleDto locale, CityFilterDto filter = null, PaginationRequestDto pagination = null, OrderDto order = null)
		{
			var uri = string.Format(FormatUriForGetCities(filter, pagination, order), Secrets.GeoHelperApiKey, locale.Lang, locale.FallbackLang);
			Debug.WriteLine(uri);

			var res = await GetAsync<City>(uri);

			if (res == null)
			{
				return new List<City>();
			}

			return res;
		}

		public async Task<List<Country>> GetCountries(LocaleDto locale, string name = null)
		{
			var uri = string.Format(GetCountriesUri, Secrets.GeoHelperApiKey, locale.Lang, locale.FallbackLang);

			if (!string.IsNullOrWhiteSpace(name))
			{
				uri += $"&filter[name]={name}";
			}

			var res = await GetAsync<Country>(uri);

			return res ?? new List<Country>();
		}
		#endregion

		#region Private
		private static string FormatUriForGetCities(CityFilterDto filter, PaginationRequestDto pagination, OrderDto order)
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
					for (var i = 0; i < filter.Ids.Length; i++)
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
					for (var i = 0; i < filter.RegionCodes.Length; i++)
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

			if (order == null)
			{
				return sb.ToString();
			}

			if (!string.IsNullOrEmpty(order.By))
			{
				sb.Append("&order[by]=");
				sb.Append(order.By);
			}

			if (string.IsNullOrEmpty(order.Dir))
			{
				return sb.ToString();
			}

			sb.Append("&order[dir]=");
			sb.Append(order.Dir);

			return sb.ToString();
		}

		private async Task<List<T>> GetAsync<T>(string url, int days = 30)
		{
			var json = string.Empty;

			//check if we are connected, else check to see if we have valid data
			if (Connectivity.NetworkAccess != NetworkAccess.Internet)
			{
				json = Barrel.Current.Get<string>(url);
			}
			else if (!Barrel.Current.IsExpired(url))
			{
				json = Barrel.Current.Get<string>(url);
			}

			try
			{
				GeoHelperResponseDto<T> response;

				//skip web request because we are using cached data
				if (string.IsNullOrWhiteSpace(json))
				{
					using (var client = new HttpClient())
					{
						client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(BaseService.ApplicationJson));

						json = await client.GetStringAsync(url);
					}

					response = JsonConvert.DeserializeObject<GeoHelperResponseDto<T>>(json);
					if (response.Success)
					{
						Barrel.Current.Add(url, json, TimeSpan.FromDays(days));
					}
				}
				else
				{
					response = JsonConvert.DeserializeObject<GeoHelperResponseDto<T>>(json);
				}

				return response.Result;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Unable to get information from server {ex}");
			}

			return default;
		}
		#endregion
	}
}
