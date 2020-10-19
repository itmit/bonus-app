using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using bonus.app.Core.Dtos;
using bonus.app.Core.Models;
using bonus.app.Core.Services.Interfaces;
using MvvmCross.Logging;
using Newtonsoft.Json;
using Plugin.InAppBilling;
using Plugin.InAppBilling.Abstractions;
using Xamarin.Forms;

namespace bonus.app.Core.Services.Implementations
{
	public class RateService : BaseService, IRateService
	{
		private IMvxLog _logger;

		public RateService(IAuthService authService, IMvxLogProvider logProvider)
			: base(authService)
		{
			_logger = logProvider.GetLogFor(GetType());
		}

		private const string RatesUri = "http://bonus.itmit-studio.ru/api/rates";
		private const string MyRateUri = "http://bonus.itmit-studio.ru/api/rates/getMyRate";
		private const string ChangeRateUri = "http://bonus.itmit-studio.ru/api/rates";
		private const string HtmlPaymentUri = "http://bonus.itmit-studio.ru/api/rates/payRate";
		private const string PaySuccessUrlConst = "http://bonus.itmit-studio.ru/api/rates/success";
		private const string PayErrorUrlConst = "http://bonus.itmit-studio.ru/api/rates/error";

		public string PaySuccessUrl => PaySuccessUrlConst;

		public string PayErrorUrl => PayErrorUrlConst;

		public async Task<List<Rate>> GetRates() => await GetAsync<List<Rate>>(RatesUri);

		public async Task<Rate> GetMyRate() => await GetAsync<Rate>(MyRateUri);

		public async Task<bool> ChangeRate(Rate rate)
		{
			var response = await HttpClient.PostAsync(ChangeRateUri, new StringContent($"{{\"id\":{rate.Id}}}", Encoding.UTF8, ApplicationJson));

			var json = await response.Content.ReadAsStringAsync();

			Debug.WriteLine(json);

			if (string.IsNullOrEmpty(json))
			{
				return false;
			}

			var data = JsonConvert.DeserializeObject<ResponseDto<Rate>>(json);

			return data?.Data != null;
		}

		public async Task<bool> PurchaseAsync(Rate rate)
		{
			try
			{
				var productId = rate.Id.ToString();

				var connected = await CrossInAppBilling.Current.ConnectAsync();

				if (!connected)
				{
					// Не удалось подключиться к биллингу, устройство в автономном режиме, оповещаем пользователя
					return false;
				}

				// Пробуем купить товар
				var purchase = await CrossInAppBilling.Current.PurchaseAsync(productId, ItemType.InAppPurchase, "apppayload");
				if (purchase == null)
				{
					// Купить не удалось, оповещаем пользователя

					return false;
				}
				else
				{
					// Покупка совершена, сохраняем информацию
					var id = purchase.Id;
					var token = purchase.PurchaseToken;
					var state = purchase.State;

					// Вызываем после успешной покупки или позднее (необходимо вызвать ConnectAsync() раньше времени):
					if (Device.RuntimePlatform == Device.Android)
					{
						var consumedItem = await CrossInAppBilling.Current.ConsumePurchaseAsync(purchase.ProductId, purchase.PurchaseToken);

						if (consumedItem != null)
						{
							// Товар использован
						}
					}
					else
					{
						//
					}
				}
			}
			catch (Exception ex)
			{
				// Произошла ошибка, оповещаем пользователя.
				_logger.Log(MvxLogLevel.Debug, () => "Purchase Error", ex);
				return false;
			}
			finally
			{
				// Отключаемся, это нормально если не удалось связаться.
				await CrossInAppBilling.Current.DisconnectAsync();
			}

			return true;
		}
	}
}
