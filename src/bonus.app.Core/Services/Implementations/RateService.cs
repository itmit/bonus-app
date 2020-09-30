using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using bonus.app.Core.Dtos;
using bonus.app.Core.Models;
using bonus.app.Core.Services.Interfaces;
using Newtonsoft.Json;

namespace bonus.app.Core.Services.Implementations
{
	public class RateService : BaseService, IRateService
	{
		public RateService(IAuthService authService)
			: base(authService)
		{
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

		public async Task<string> GetHtmlPayment()
		{
			var response = await HttpClient.PostAsync(HtmlPaymentUri, new StringContent("{\"count_rates\":1}", Encoding.UTF8, ApplicationJson));
			var json = await response.Content.ReadAsStringAsync();

			Debug.WriteLine(json);

			if (string.IsNullOrEmpty(json))
			{
				return "";
			}

			var data = JsonConvert.DeserializeObject<ResponseDto<Dictionary<string, string>>>(json);

			return data.Success ? data.Data?["url"] : null;
		}
	}
}
