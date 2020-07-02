using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using bonus.app.Core.Dtos;
using bonus.app.Core.Models.UserModels;
using Newtonsoft.Json;

namespace bonus.app.Core.Services
{
	public class ManagerService : BaseService, IManagerService
	{
		public ManagerService(IAuthService authService)
			: base(authService)
		{
		}

		private const string ManagerUri = "http://bonus.itmit-studio.ru/api/manager";

		public async Task<Guid> StoreManager(User user, string password, string confirmPassword)
		{
			using (var client = new HttpClient())
			{
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(ApplicationJson));
				client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse(AuthService.Token.ToString());
				
				var response = await client.PostAsync(ManagerUri, new FormUrlEncodedContent(new Dictionary<string, string>
				{
					{"name",  user.Name},
					{"phone",  user.Phone},
					{"email",  user.Email},
					{"password",  password},
					{"password_confirmation",  confirmPassword},
					{"businessman_id", AuthService.User.Id.ToString()}
				}));
				
				var jsonString = await response.Content.ReadAsStringAsync();
				Debug.WriteLine(jsonString);

				var data = JsonConvert.DeserializeObject<ResponseDto<UserDto>>(jsonString);

				return data.Success ? data.Data.Uuid : Guid.Empty;
			}
		}

	public async Task<List<User>> GetManagers() => await GetAsync<List<User>>(ManagerUri);

		public Task<User> GetManager() => throw new NotImplementedException();

		public Task<User> EditManager(string name, string phone) => throw new NotImplementedException();

		public Task<bool> DeleteManager() => throw new NotImplementedException();
	}
}
