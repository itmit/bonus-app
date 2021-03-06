﻿using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using bonus.app.Core.Dtos.BusinessmanDtos;
using bonus.app.Core.Models;
using bonus.app.Core.Services.Interfaces;
using Newtonsoft.Json;

namespace bonus.app.Core.Services.Implementations
{
	public class BonusService : BaseService, IBonusService
	{
		#region Data
		#region Consts
		private const string AccrueAndWriteOffBonusesUri = "http://bonus.itmit-studio.ru/api/service";
		private const string GetMyBonusesUri = "http://bonus.itmit-studio.ru/api/getMyBonuses";
		#endregion
		#endregion

		#region .ctor
		public BonusService(IAuthService authService)
			: base(authService)
		{
		}
		#endregion

		#region IBonusService members
		public async Task<bool> AccrueAndWriteOffBonuses(AccrueAndWriteOffBonusesDto requestDto)
		{
			var json = JsonConvert.SerializeObject(requestDto);
			Debug.WriteLine(json);
			var response = await HttpClient.PostAsync(AccrueAndWriteOffBonusesUri, new StringContent(json, Encoding.UTF8, ApplicationJson));

			var jsonString = await response.Content.ReadAsStringAsync();
			Debug.WriteLine(jsonString);

			return response.IsSuccessStatusCode;
		}

		public async Task<List<AccrualBonuses>> GetMyBonuses()
		{
			var services = await GetAsync<List<AccrualBonuses>>(GetMyBonusesUri);
			return services ?? new List<AccrualBonuses>();
		}
		#endregion
	}
}
