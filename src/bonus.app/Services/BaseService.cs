using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using bonus.app.Core.Dtos;
using MonkeyCache;
using MonkeyCache.FileStore;
using Newtonsoft.Json;
using Xamarin.Essentials;

namespace bonus.app.Core.Services
{
	public abstract class BaseService
	{
		protected IAuthService AuthService
		{
			get;
		}

		public BaseService(IAuthService authService) => AuthService = authService;

		protected async Task<T> GetAsync<T>(string url, int days = 1, bool forceRefresh = true)
		{
			var json = string.Empty;

			//check if we are connected, else check to see if we have valid data
			if (Connectivity.NetworkAccess != NetworkAccess.Internet)
			{
				json = Barrel.Current.Get<string>(url);
			}
			else if (!forceRefresh && !Barrel.Current.IsExpired(url))
			{
				json = Barrel.Current.Get<string>(url);
			}

			try
			{
				ResponseDto<T> response;

				//skip web request because we are using cached data
				if (string.IsNullOrWhiteSpace(json))
				{
					using (var  client = new HttpClient())
					{
						client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
						client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse($"{AuthService.Token.Type} {AuthService.Token.Body}");

						json = await client.GetStringAsync(new Uri(url));
					}

					response = JsonConvert.DeserializeObject<ResponseDto<T>>(json);
					if (response.Success)
					{
						Barrel.Current.Add(url, json, TimeSpan.FromDays(days));
					}
				}
				else
				{
					response = JsonConvert.DeserializeObject<ResponseDto<T>>(json);
				}

				return response.Data;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Unable to get information from server {ex}");
			}

			return default;
		}
	}
}
