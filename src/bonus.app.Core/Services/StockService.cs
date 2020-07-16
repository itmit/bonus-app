using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using bonus.app.Core.Dtos;
using bonus.app.Core.Models;
using bonus.app.Core.Models.UserModels;
using Newtonsoft.Json;

namespace bonus.app.Core.Services
{
	public class StockService : BaseService, IStockService
	{
		#region Delegates and events
		public delegate void EditedStockEventHandler(Stock stock);

		public event EventHandler CreatedStockEventHandler;

		public event EditedStockEventHandler EditedStock;
		#endregion

		#region Data
		#region Consts
		private const string AddToFavoriteUri = "http://bonus.itmit-studio.ru/api/addStockToFavorite";
		private const string BusinessmenStocksUri = "http://bonus.itmit-studio.ru/api/businessmanstock";
		private const string EditStockUri = "http://bonus.itmit-studio.ru/api/businessmanstock/{0}";
		private const string GetAllUri = "http://bonus.itmit-studio.ru/api/customerstock";
		private const string GetArchiveStockUri = "http://bonus.itmit-studio.ru/api/customerstockarchive";
		private const string GetFavoriteStocksUri = "http://bonus.itmit-studio.ru/api/getFavoriteStocks";
		private const string GetFilterArchiveStockUri = "http://bonus.itmit-studio.ru/api/customerstockarchive/filtered";
		private const string GetMyArchiveStockUri = "http://bonus.itmit-studio.ru/api/businessmanstockarchive";
		private const string GetMyFilterArchiveStockUri = "http://bonus.itmit-studio.ru/api/businessmanstockarchive/filtered";
		private const string GetStockForEditUri = "http://bonus.itmit-studio.ru/api/businessmanstock/{0}/edit";
		#endregion
		#endregion

		#region .ctor
		public StockService(IAuthService authService)
			: base(authService)
		{
		}
		#endregion

		#region IStockService members
		public async Task<bool> AddToFavorite(Guid stockUuid)
		{
			var resp = await HttpClient.PostAsync(AddToFavoriteUri, new StringContent($"{{\"stock_uuid\":\"{stockUuid}\"}}", Encoding.UTF8, ApplicationJson));
			var json = await resp.Content.ReadAsStringAsync();
			Debug.WriteLine(json);

			if (string.IsNullOrEmpty(json))
			{
				return false;
			}

			var data = JsonConvert.DeserializeObject<ResponseDto<object>>(json);
			return data.Success;
		}

		public async Task<bool> CreateStock(Stock stock, byte[] imageBytes)
		{
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

			var response = await HttpClient.PostAsync(new Uri(BusinessmenStocksUri), content);

			var json = await response.Content.ReadAsStringAsync();
			Debug.WriteLine(json);

			if (string.IsNullOrEmpty(json))
			{
				return false;
			}

			var data = JsonConvert.DeserializeObject<ResponseDto<object>>(json);
			if (data.Success)
			{
				CreatedStockEventHandler?.Invoke(this, EventArgs.Empty);
			}

			return data.Success;
		}

		public async Task<bool> EditStock(Stock stock, byte[] imageBytes = null)
		{
			var date = stock.ShareTime.ToString("yyyy-MM-dd");
			var content = new MultipartFormDataContent
			{
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
					new StringContent(stock.Service.ToString()), "service_uuid"
				}
			};

			if (imageBytes != null)
			{
				var byteArrayContent = new ByteArrayContent(imageBytes);
				byteArrayContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpeg");
				content.Add(byteArrayContent, "\"photo\"", $"\"{stock.ImageSource}\"");
			}

			var response = await HttpClient.PostAsync(new Uri(string.Format(EditStockUri, stock.Uuid)), content);

			var json = await response.Content.ReadAsStringAsync();
			Debug.WriteLine(json);

			if (string.IsNullOrEmpty(json))
			{
				return false;
			}

			var data = JsonConvert.DeserializeObject<ResponseDto<object>>(json);
			if (data.Success)
			{
				EditedStock?.Invoke(stock);
			}

			return data.Success;
		}

		public async Task<IEnumerable<Stock>> GetAll()
		{
			var stocks = await GetAsync<List<Stock>>(GetAllUri);

			return ConvertSocksImages(stocks);
		}

		private const string GetDetailBusinessmanUri = "http://bonus.itmit-studio.ru/api/businessmanstock/{0}";
		private const string GetDetailCustomerUri = "http://bonus.itmit-studio.ru/api/customerstock/{0}";

		public async Task<Stock> GetDetail(Guid uuid)
		{
			Stock stock = null;
			switch (AuthService.User.Role)
			{
				case UserRole.Manager:
				case UserRole.Businessman:
				{
					stock = await GetAsync<Stock>(string.Format(GetDetailBusinessmanUri, uuid));
					break;
				}
				case UserRole.Customer:
				{
					stock = await GetAsync<Stock>(string.Format(GetDetailCustomerUri, uuid));
					break;
				}

				default:
					throw new ArgumentOutOfRangeException();
			}

			if (string.IsNullOrEmpty(stock.ImageSource))
			{
				stock.ImageSource = "about:blank";
			}
			else
			{
				stock.ImageSource = Domain + stock.ImageSource;
			}
			return stock;
		}

		public async Task<IEnumerable<Stock>> GetArchiveStock(Guid? serviceUuid, string city)
		{
			List<Stock> stocks;
			if (serviceUuid == null && string.IsNullOrEmpty(city))
			{
				stocks = await GetAsync<List<Stock>>(GetArchiveStockUri);
			}
			else
			{
				var uuid = Guid.Empty;
				if (serviceUuid != null)
				{
					uuid = serviceUuid.Value;
				}

				stocks = (await GetAsync<IEnumerable<Stock>>(GetFilterArchiveStockUri, $"{{\"uuid\":\"{uuid}\",\"city\":\"{city}\"}}"))?.ToList();
			}

			return ConvertSocksImages(stocks);
		}

		public async Task<List<Stock>> GetFavoriteStocks()
		{
			var stocks = await GetAsync<List<Stock>>(GetFavoriteStocksUri);
			if (stocks == null)
			{
				return new List<Stock>();
			}

			foreach (var share in stocks)
			{
				if (string.IsNullOrEmpty(share.ImageSource))
				{
					share.ImageSource = string.Empty;
					continue;
				}

				share.ImageSource = Domain + share.ImageSource;
			}

			return stocks;
		}

		public Task<IEnumerable<Stock>> GetMyArchiveStock() => GetMyArchiveStock(null, null);

		public async Task<IEnumerable<Stock>> GetMyArchiveStock(Guid? serviceUuid, string city)
		{
			List<Stock> stocks;
			if (serviceUuid == null && string.IsNullOrEmpty(city))
			{
				stocks = await GetAsync<List<Stock>>(GetMyArchiveStockUri);
			}
			else
			{
				var uuid = Guid.Empty;
				if (serviceUuid != null)
				{
					uuid = serviceUuid.Value;
				}

				stocks = (await GetAsync<IEnumerable<Stock>>(GetMyFilterArchiveStockUri, $"{{\"uuid\":\"{uuid}\",\"city\":\"{city}\"}}"))?.ToList();
			}

			return ConvertSocksImages(stocks);
		}

		private static IEnumerable<Stock> ConvertSocksImages(IEnumerable<Stock> stocks)
		{
			if (stocks == null)
			{
				return new List<Stock>();
			}

			var list = stocks.ToList();
			foreach (var stock in list)
			{
				if (string.IsNullOrEmpty(stock.ImageSource))
				{
					stock.ImageSource = "about:blank";
					continue;
				}

				stock.ImageSource = Domain + stock.ImageSource;
			}

			return list;
		}

		public async Task<IEnumerable<Stock>> GetMyStocks()
		{
			var stocks = await GetAsync<List<Stock>>(BusinessmenStocksUri);

			return ConvertSocksImages(stocks);
		}

		public async Task<Stock> GetStockForEdit(Guid uuid)
		{
			var response = await HttpClient.GetAsync(new Uri(string.Format(GetStockForEditUri, uuid)));

			var json = await response.Content.ReadAsStringAsync();
			Debug.WriteLine(json);

			if (string.IsNullOrEmpty(json))
			{
				return null;
			}

			var data = JsonConvert.DeserializeObject<ResponseDto<Stock>>(json);
			data.Data.ImageSource = Domain + data.Data.ImageSource;
			return data.Data;
		}
		#endregion
	}
}
