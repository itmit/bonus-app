using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using bonus.app.Core.Dtos;
using MonkeyCache.FileStore;
using Newtonsoft.Json;
using Xamarin.Essentials;

namespace bonus.app.Core.Services
{
	public abstract class BaseService
	{
		#region Data
		#region Static
		public const string ApplicationJson = "application/json";

		public const string Domain = "http://bonus.itmit-studio.ru/";
		#endregion
		#endregion

		#region .ctor
		public BaseService(IAuthService authService)
		{
			AuthService = authService;
			HttpClient = new HttpClient();
			HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(ApplicationJson));
			//HttpClient.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse(AuthService.Token.ToString());

		}
		#endregion

		#region Properties
		protected IAuthService AuthService
		{
			get;
		}

		protected HttpClient HttpClient
		{
			get;
		}
		#endregion

		#region Protected
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
					json = await HttpClient.GetStringAsync(new Uri(url));
					
					Debug.WriteLine(json);
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

		protected async Task<T> GetAsync<T>(string url, string jsonData, int days = 1, bool forceRefresh = true)
		{
			var json = string.Empty;
			var urlWithParameters = url + "?" + jsonData;

			//check if we are connected, else check to see if we have valid data
			if (Connectivity.NetworkAccess != NetworkAccess.Internet)
			{
				json = Barrel.Current.Get<string>(urlWithParameters);
			}
			else if (!forceRefresh && !Barrel.Current.IsExpired(urlWithParameters))
			{
				json = Barrel.Current.Get<string>(urlWithParameters);
			}

			try
			{
				ResponseDto<T> response;

				//skip web request because we are using cached data
				if (string.IsNullOrWhiteSpace(json))
				{
					var res = await HttpClient.PostAsync(new Uri(url), new StringContent(jsonData, Encoding.UTF8, ApplicationJson));
					json = await res.Content.ReadAsStringAsync();
					
					Debug.WriteLine(json);
					response = JsonConvert.DeserializeObject<ResponseDto<T>>(json);
					if (response.Success)
					{
						Barrel.Current.Add(urlWithParameters, json, TimeSpan.FromDays(days));
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
		#endregion
	}
}
