﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using bonus.app.Core.Dtos;
using bonus.app.Core.Models;
using Newtonsoft.Json;

namespace bonus.app.Core.Services
{
	class ShareService : BaseService, IShareService
	{
		private const string GetMySharesUri = "http://bonus.itmit-studio.ru/api/businessmanstock";
		private const string GetAllUri = "http://bonus.itmit-studio.ru/api/customerstock";

		public async Task<IEnumerable<Share>> GetMyShares()
		{
			var shares = (await GetAsync<IEnumerable<Share>>(GetMySharesUri))?.ToList();
			if (shares == null)
			{
				return new List<Share>();
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

		public const string CreateShareUri = "http://bonus.itmit-studio.ru/api/businessmanstock";

		public async Task<bool> CreateShare(Share share, byte[] imageBytes)
		{
			using (var client = new HttpClient())
			{
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse(AuthService.Token.ToString());
				
				var byteArrayContent = new ByteArrayContent(imageBytes);
				byteArrayContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpeg");
				var date = share.ShareTime.ToString("yyyy-MM-dd");
				var content = new MultipartFormDataContent
				{
					{
						byteArrayContent, "\"photo\"", $"\"{share.ImageSource}\""
					},
					{
						new StringContent(share.Description), "description"
					},
					{
						new StringContent(share.Country), "country"
					},
					{
						new StringContent(share.City), "city"
					},
					{
						new StringContent(share.Name), "name"
					},
					{
						new StringContent(date), "expires_at"
					},
					{
						new StringContent(share.IsSubscriberOnly ? "1" : "0"), "sub_only"
					},
					{
						new StringContent(share.Service.ToString()), "service_uuid"
					}
				};

				var response = await client.PostAsync(new Uri(CreateShareUri), content);

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

		public async Task<IEnumerable<Share>> GetAll()
		{
			var shares = (await GetAsync<IEnumerable<Share>>(GetAllUri))?.ToList();
			if (shares == null)
			{
				return new List<Share>();
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

		public event EventHandler CreatedShareEventHandler;

		public ShareService(IAuthService authService)
			: base(authService)
		{
		}
	}
}