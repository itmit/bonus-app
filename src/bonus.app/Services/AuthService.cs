using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using bonus.app.Core.Dto;
using bonus.app.Core.Models;
using bonus.app.Views;
using Newtonsoft.Json;

namespace bonus.app.Core.Services
{
	public class AuthService : IAuthService
	{
		public ErrorsDto<AuthErrorDto> ServerAuthorizationError
		{
			get;
			private set;
		}

		public async Task<User> Login(AuthDto authData)
		{
			if (authData.Login == "buy")
			{
				return await Task.FromResult(new User
				{
					Login = authData.Login,
					AccessToken = new AccessToken
					{
						Body = "-",
						Type = "-"
					},
					Guid = Guid.NewGuid(),
					Role = UserRole.Buyer
				});
			}

			if (authData.Login == "ent")
			{
				return await Task.FromResult(new User
				{
					Login = authData.Login,
					AccessToken = new AccessToken
					{
						Body = "-",
						Type = "-"
					},
					Guid = Guid.NewGuid(),
					Role = UserRole.Entrepreneur
				});
			}

			return null;
			/*
			using (var client = new HttpClient())
			{
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				var json = JsonConvert.SerializeObject(authData);
				Debug.WriteLine(json);
				var response = await client.PostAsync(SignInUri, new StringContent(json, Encoding.UTF8, "application/json"));

				var jsonString = await response.Content.ReadAsStringAsync();
				Debug.WriteLine(jsonString);

				if (string.IsNullOrEmpty(jsonString))
				{
					ServerAuthorizationError = new ErrorsDto<AuthErrorDto>
					{
						Message = "Нет ответа от сервера"
					};
					return null;
				}

				if (response.IsSuccessStatusCode)
				{
					var jsonData = JsonConvert.DeserializeObject<JsonResponseDto<UserDto>>(jsonString);
					return _mapper.Map<User>(jsonData.Data);
				}

				var jsonError = JsonConvert.DeserializeObject<ErrorsDto<AuthErrorDto>>(jsonString);
				ServerAuthorizationError = jsonError;
				return null;
			}
		}
		*/
		}
	}
}
