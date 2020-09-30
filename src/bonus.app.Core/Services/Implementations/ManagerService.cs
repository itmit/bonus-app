using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using bonus.app.Core.Dtos;
using bonus.app.Core.Models.UserModels;
using bonus.app.Core.Services.Interfaces;
using Newtonsoft.Json;

namespace bonus.app.Core.Services.Implementations
{
	public class ManagerService : BaseService, IManagerService
	{
		public ManagerService(IAuthService authService)
			: base(authService)
		{
		}

		private const string ManagersUri = "http://bonus.itmit-studio.ru/api/manager";
		private const string ManagerUri = "http://bonus.itmit-studio.ru/api/manager/{0}";

		public async Task<Guid> StoreManager(User user, string password, string confirmPassword)
		{
			var response = await HttpClient.PostAsync(ManagersUri, new FormUrlEncodedContent(new Dictionary<string, string>
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

			LastError = data.ErrorDetails?.Values.LastOrDefault()?
							.LastOrDefault();

			return data.Success ? data.Data.Uuid : Guid.Empty;
		}

		public async Task<List<User>> Managers() => await GetAsync<List<User>>(ManagersUri);

		public Task<User> Manager() => throw new NotImplementedException();

		public async Task<bool> EditManager(int managerId, string name, string phone)
		{
			var response = await HttpClient.PostAsync(string.Format(ManagerUri, managerId), new FormUrlEncodedContent(new Dictionary<string, string>
			{
				{"name",  name},
				{"phone",  phone},
				{"_method", "PUT"}
			}));

			var jsonString = await response.Content.ReadAsStringAsync();
			Debug.WriteLine(jsonString);

			var data = JsonConvert.DeserializeObject<ResponseDto<object>>(jsonString);
			
			return data.Success;
		}

		public async Task<bool> DeleteManager(int managerId)
		{
			var response = await HttpClient.DeleteAsync(string.Format(ManagerUri, managerId));

			var jsonString = await response.Content.ReadAsStringAsync();
			Debug.WriteLine(jsonString);

			var data = JsonConvert.DeserializeObject<ResponseDto<object>>(jsonString);

			return data.Success;
		}

		public string LastError
		{
			get;
			private set;
		}
	}
}
