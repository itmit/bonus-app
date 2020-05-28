﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using bonus.app.Core.Dtos;
using bonus.app.Core.Models;
using Newtonsoft.Json;

namespace bonus.app.Core.Services
{
	public class SubscribeService : BaseService, ISubscribeService
	{
		private const string SubscribeToBusinessmanUri  = "http://bonus.itmit-studio.ru/api/subscribeToBusinessman";
		private const string GetSubscriptionsUri = "http://bonus.itmit-studio.ru/api/getSubscriptuions";
		private const string UnsubscribeToBusinessmanUri = "http://bonus.itmit-studio.ru/api/unsubscribeToBusinessman";

		public async Task<bool> SubscribeToBusinessman(Guid businessmanUuid)
		{
			using (var client = new HttpClient())
			{

				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(ApplicationJson));
				client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse(AuthService.Token.ToString());

				var resp = await client.PostAsync(new Uri(SubscribeToBusinessmanUri), new StringContent($"{{\"businessmen_uuid\":\"{businessmanUuid}\"}}", Encoding.UTF8, ApplicationJson));

				var json = await resp.Content.ReadAsStringAsync();
				Debug.WriteLine(json);

				if (string.IsNullOrEmpty(json))
				{
					return false;
				}

				var data = JsonConvert.DeserializeObject<ResponseDto<object>>(json);

				return data.Success;
			}
		}

		public async Task<List<Subscription>> GetSubscriptions()
		{
			var subs = await GetAsync<List<Subscription>>(GetSubscriptionsUri);
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

		public async Task<bool> UnsubscribeToBusinessman(Guid businessmanUuid)
		{
			using (var client = new HttpClient())
			{

				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(ApplicationJson));
				client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse(AuthService.Token.ToString());

				var resp = await client.PostAsync(new Uri(UnsubscribeToBusinessmanUri), new StringContent($"{{\"businessmen_uuid\":\"{businessmanUuid}\"}}", Encoding.UTF8, ApplicationJson));

				var json = await resp.Content.ReadAsStringAsync();
				Debug.WriteLine(json);

				if (string.IsNullOrEmpty(json))
				{
					return false;
				}

				var data = JsonConvert.DeserializeObject<ResponseDto<object>>(json);

				return data.Success;
			}
		}

		public SubscribeService(IAuthService authService)
			: base(authService)
		{
		}
	}
}
