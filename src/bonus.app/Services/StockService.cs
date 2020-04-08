using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using bonus.app.Core.Dtos;
using bonus.app.Core.Models;
using Newtonsoft.Json;

namespace bonus.app.Core.Services
{
	internal class StockService : BaseService, IStockService
	{
		#region Delegates and events
		public event EventHandler CreatedShareEventHandler;
		#endregion

		#region Data
		#region Consts
		private const string CreateUri = "http://bonus.itmit-studio.ru/api/businessmanstock";
		private const string GetAllUri = "http://bonus.itmit-studio.ru/api/customerstock";
		private const string GetMyStocksUri = "http://bonus.itmit-studio.ru/api/businessmanstock";
		private const string GetArchiveStockUri = "http://bonus.itmit-studio.ru/api/customerstockarchive";
		private const string GetMyFilterStocksUri = "http://bonus.itmit-studio.ru/api/businessmanstock";
		private const string GetFilterArchiveStockUri = "http://bonus.itmit-studio.ru/api/customerstockarchive";
		#endregion
		#endregion

		#region .ctor
		public StockService(IAuthService authService)
			: base(authService)
		{
		}
		#endregion

		#region IStockService members
		public Task<IEnumerable<Stock>> GetArchiveStock() => GetArchiveStock(null, null);

		public async Task<bool> CreateStock(Stock stock, byte[] imageBytes)
		{
			using (var client = new HttpClient())
			{
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse(AuthService.Token.ToString());

				var byteArrayContent = new ByteArrayContent(imageBytes);
				byteArrayContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpeg");
				var date = stock.ShareTime.ToString("yyyy-MM-dd");
				var content = new MultipartFormDataContent
				{
					{
						byteArrayContent, "\"photo\"", $"\"{stock.ImageSource}\""
					},
					{
						new StringContent(stock.Description), "description"
					},
					{
						new StringContent(stock.Country), "country"
					},
					{
						new StringContent(stock.City), "city"
					},
					{
						new StringContent(stock.Name), "name"
					},
					{
						new StringContent(date), "expires_at"
					},
					{
						new StringContent(stock.IsSubscriberOnly ? "1" : "0"), "sub_only"
					},
					{
						new StringContent(stock.Service.ToString()), "service_uuid"
					}
				};

				var response = await client.PostAsync(new Uri(CreateUri), content);

				var json = await response.Content.ReadAsStringAsync();
				Debug.WriteLine(json);

				if (string.IsNullOrEmpty(json))
				{
					return false;
				}

				var data = JsonConvert.DeserializeObject<ResponseDto<object>>(json);
				if (data.Success)
				{
					CreatedShareEventHandler?.Invoke(this, EventArgs.Empty);
				}

				return data.Success;
			}
		}

		public async Task<IEnumerable<Stock>> GetAll()
		{
			var shares = (await GetAsync<IEnumerable<Stock>>(GetAllUri))?.ToList();
			if (shares == null)
			{
				return new List<Stock>();
			}

			foreach (var share in shares)
			{
				if (string.IsNullOrEmpty(share.ImageSource))
				{
					share.ImageSource = string.Empty;
					continue;
				}

				share.ImageSource = Domain + share.ImageSource;
			}

			return shares;
		}

		public async Task<IEnumerable<Stock>> GetArchiveStock(Guid? serviceUuid, string city)
		{
			List<Stock> stocks;
			if (serviceUuid == null && string.IsNullOrEmpty(city))
			{
				stocks = (await GetAsync<IEnumerable<Stock>>(GetArchiveStockUri))?.ToList();
			}
			else
			{
				Guid uuid = Guid.Empty;
				if (serviceUuid != null)
				{
					uuid = serviceUuid.Value;
				}

				stocks = (await GetAsync<IEnumerable<Stock>>(GetFilterArchiveStockUri, $"{{\"uuid\":\"{uuid}\",\"city\":\"{city}\"}}"))?.ToList();
			}

			if (stocks == null)
			{
				return new List<Stock>();
			}

			foreach (var stock in stocks)
			{
				if (string.IsNullOrEmpty(stock.ImageSource))
				{
					stock.ImageSource = string.Empty;
					continue;
				}

				stock.ImageSource = Domain + stock.ImageSource;
			}

			return stocks;
		}

		public Task<IEnumerable<Stock>> GetMyStock() => GetMyStock(null, null);

		public async Task<IEnumerable<Stock>> GetMyStock(Guid? serviceUuid, string city)
		{
			List<Stock> stocks;
			if (serviceUuid == null && string.IsNullOrEmpty(city))
			{
				stocks = (await GetAsync<IEnumerable<Stock>>(GetMyStocksUri))?.ToList();
			}
			else
			{
				Guid uuid = Guid.Empty;
				if (serviceUuid != null)
				{
					uuid = serviceUuid.Value;
				}

				stocks = (await GetAsync<IEnumerable<Stock>>(GetMyFilterStocksUri, $"{{\"uuid\":\"{uuid}\",\"city\":\"{city}\"}}"))?.ToList();
			}

			if (stocks == null)
			{
				return new List<Stock>();
			}

			foreach (var stock in stocks)
			{
				if (string.IsNullOrEmpty(stock.ImageSource))
				{
					stock.ImageSource = string.Empty;
					continue;
				}

				stock.ImageSource = Domain + stock.ImageSource;
			}

			return stocks;
		}
		#endregion
	}
}
