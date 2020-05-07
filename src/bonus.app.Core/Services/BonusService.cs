using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using bonus.app.Core.Dtos.BusinessmanDtos;
using Newtonsoft.Json;

namespace bonus.app.Core.Services
{
	public class BonusService : IBonusService
	{
		#region Data
		#region Consts
		private const string AccrueAndWriteOffBonusesUri = "http://bonus.itmit-studio.ru/api/service";
		#endregion

		#region Fields
		private readonly IAuthService _authService;
		#endregion
		#endregion

		#region .ctor
		public BonusService(IAuthService authService) => _authService = authService;
		#endregion

		#region IBonusService members
		public async Task<bool> AccrueAndWriteOffBonuses(AccrueAndWriteOffBonusesDto requestDto)
		{
			using (var client = new HttpClient())
			{
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(BaseService.ApplicationJson));
				client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse(_authService.Token.ToString());

				var json = JsonConvert.SerializeObject(requestDto);
				Debug.WriteLine(json);
				var response = await client.PostAsync(AccrueAndWriteOffBonusesUri, new StringContent(json, Encoding.UTF8, BaseService.ApplicationJson));

				var jsonString = await response.Content.ReadAsStringAsync();
				Debug.WriteLine(jsonString);

				return response.IsSuccessStatusCode;
			}
		}
		#endregion
	}
}
