using System;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using bonus.app.Core.Dtos.BusinessmanDtos;
using bonus.app.Core.Models;
using bonus.app.Core.Repositories;
using Newtonsoft.Json;

namespace bonus.app.Core.Services
{
	public class BonusService : IBonusService
	{
		private readonly IAuthService _authService;

		public BonusService(IAuthService authService)
		{
			_authService = authService;
		}

		private const string AccrueAndWriteOffBonusesUri = "http://bonus.itmit-studio.ru/api/service";

		public async Task<bool> AccrueAndWriteOffBonuses(AccrueAndWriteOffBonusesDto requestDto) 
		{
			using (var client = new HttpClient())
			{
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse(_authService.Token.ToString());

				var json = JsonConvert.SerializeObject(requestDto);
				Debug.WriteLine(json);
				var response = await client.PostAsync(AccrueAndWriteOffBonusesUri, new StringContent(json, Encoding.UTF8, "application/json"));

				var jsonString = await response.Content.ReadAsStringAsync();
				Debug.WriteLine(jsonString);

				return response.IsSuccessStatusCode;
			}
		}
	}
}
