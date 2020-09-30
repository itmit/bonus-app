using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using bonus.app.Core.Dtos;
using bonus.app.Core.Models.UserModels;
using bonus.app.Core.Services.Interfaces;
using Newtonsoft.Json;

namespace bonus.app.Core.Services.Implementations
{
	public class SubscribeService : BaseService, ISubscribeService
	{
		#region Data
		#region Consts
		private const string SubscriptionsUri = "http://bonus.itmit-studio.ru/api/getSubscriptuions";
		private const string SubscribeToBusinessmanUri = "http://bonus.itmit-studio.ru/api/subscribeToBusinessman";
		private const string UnsubscribeToBusinessmanUri = "http://bonus.itmit-studio.ru/api/unsubscribeToBusinessman";
		#endregion
		#endregion

		#region .ctor
		public SubscribeService(IAuthService authService)
			: base(authService)
		{
		}
		#endregion

		#region ISubscribeService members
		public async Task<List<Subscription>> GetSubscriptions()
		{
			var subs = await GetAsync<List<Subscription>>(SubscriptionsUri);
			if (subs == null)
			{
				return new List<Subscription>();
			}

			foreach (var subscription in subs)
			{
				if (string.IsNullOrEmpty(subscription.PhotoSource))
				{
					subscription.PhotoSource = "about:blank";
					continue;
				}

				subscription.PhotoSource = Domain + subscription.PhotoSource;
			}

			return subs;
		}

		public async Task<bool> SubscribeToBusinessman(Guid businessmanUuid)
		{
			var resp = await HttpClient.PostAsync(new Uri(SubscribeToBusinessmanUri),
												  new StringContent($"{{\"businessmen_uuid\":\"{businessmanUuid}\"}}", Encoding.UTF8, ApplicationJson));

			var json = await resp.Content.ReadAsStringAsync();
			Debug.WriteLine(json);

			if (string.IsNullOrEmpty(json))
			{
				return false;
			}

			var data = JsonConvert.DeserializeObject<ResponseDto<object>>(json);

			return data.Success;
		}

		public async Task<bool> UnsubscribeToBusinessman(Guid businessmanUuid)
		{
			var resp = await HttpClient.PostAsync(new Uri(UnsubscribeToBusinessmanUri),
													  new StringContent($"{{\"businessmen_uuid\":\"{businessmanUuid}\"}}", Encoding.UTF8, ApplicationJson));

			var json = await resp.Content.ReadAsStringAsync();
			Debug.WriteLine(json);

			if (string.IsNullOrEmpty(json))
			{
				return false;
			}

			var data = JsonConvert.DeserializeObject<ResponseDto<object>>(json);

			return data.Success;
		}
		#endregion
	}
}
