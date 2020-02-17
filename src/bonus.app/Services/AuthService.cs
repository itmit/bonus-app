using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using bonus.app.Core.Dto;
using bonus.app.Core.Dtos;
using bonus.app.Core.Models;
using bonus.app.Views;
using Newtonsoft.Json;

namespace bonus.app.Core.Services
{
	public class AuthService : IAuthService
	{
		private readonly Mapper _mapper;

		public AuthService()
		{
			_mapper = new Mapper(new MapperConfiguration(cfg =>
			{
				cfg.CreateMap<AccessToken, UserDto>();

				cfg.CreateMap<UserDto, User>()
				   .ForPath(m => m.AccessToken.Body, o => o.MapFrom(q => q.Body))
				   .ForPath(m => m.AccessToken.Type, o => o.MapFrom(q => q.Type));
			}));
		}

		public Dictionary<string, string[]> ErrorDetails
		{
			get;
			private set;
		} = new Dictionary<string, string[]>();

		public async Task<User> Register(User user, string password, string confirmPassword)
		{
			using (var client = new HttpClient())
			{
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				var regDto = new RegisterDto
				{
					Email = user.Email,
					Name = user.Name,
					Login = user.Login,
					Password = password,
					PasswordConfirm = confirmPassword,
				};
				if (user.Role == UserRole.Businessman)
				{
					regDto.Type = "businessman";
				} else if (user.Role == UserRole.Customer)
				{
					regDto.Type = "customer";
				}
				var requestBody = JsonConvert.SerializeObject(regDto);

				Debug.WriteLine(requestBody);

				var response = await client.PostAsync(RegisterUri, new StringContent(requestBody, Encoding.UTF8, "application/json"));

				var jsonString = await response.Content.ReadAsStringAsync();
				Debug.WriteLine(jsonString);

				var data = JsonConvert.DeserializeObject<ResponseDto<UserDto>>(jsonString);

				if (data.Success)
				{
					user = await Task.FromResult(_mapper.Map<User>(data.Data));
					return user;
				}

				if (data.ErrorDetails != null)
				{
					ErrorDetails = data.ErrorDetails;
				}

				Error = data.Error;

				return null;
			}
		}

		public string Error
		{
			get;
			private set;
		}

		private const string LoginUri = "http://bonus.itmit-studio.ru/api/login";
		private const string RegisterUri = "http://bonus.itmit-studio.ru/api/register";

		public async Task<User> Login(AuthDto authData)
		{
			using (var client = new HttpClient())
			{
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				var json = JsonConvert.SerializeObject(authData);
				Debug.WriteLine(json);
				var response = await client.PostAsync(LoginUri, new StringContent(json, Encoding.UTF8, "application/json"));

				var jsonString = await response.Content.ReadAsStringAsync();
				Debug.WriteLine(jsonString);

				if (string.IsNullOrEmpty(jsonString))
				{
					Error = "Нет ответа от сервера";
					return null;
				}
				var data = JsonConvert.DeserializeObject<ResponseDto<UserDto>>(jsonString);

				if (response.IsSuccessStatusCode)
				{
					return _mapper.Map<User>(data.Data);
				}

				if (data.ErrorDetails != null)
				{
					ErrorDetails = data.ErrorDetails;
				}

				Error = data.Error;
				return null;
			}
		}
	}
}
