using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using bonus.app.Core.Dtos;
using bonus.app.Core.Models;
using bonus.app.Core.Repositories;
using MvvmCross;
using Newtonsoft.Json;

namespace bonus.app.Core.Services
{
	public class AuthService : IAuthService
	{
		private readonly Mapper _mapper;
		private readonly IUserRepository _userRepository;
		private Guid _userUuid = Guid.Empty;

		public AuthService(IUserRepository userRepository)
		{
			_userRepository = userRepository;
			_mapper = new Mapper(new MapperConfiguration(cfg =>
			{
				cfg.CreateMap<AccessToken, UserDto>();

				cfg.CreateMap<UserDto, User>()
				   .ForPath(m => m.AccessToken.Body, o => o.MapFrom(q => q.Body))
				   .ForPath(m => m.AccessToken.Type, o => o.MapFrom(q => q.Type));
				cfg.CreateMap<UserInfoDto, User>()
				   .ForPath(m => m.Guid, o => o.MapFrom(q => q.Uuid))
				   .ForPath(m => m.Role, o => o.MapFrom(q => q.Role));
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

		public User User
		{
			get
			{
				if (_userUuid == Guid.Empty)
				{
					var u = _userRepository.GetAll()
										  .SingleOrDefault();
					if (u != null)
					{
						_userUuid = u.Guid;
						return u;
					}
				}

				return _userRepository.Find(_userUuid);
			}
		}

		public AccessToken Token => User?.AccessToken;

		private const string LogOutUri = "http://bonus.itmit-studio.ru/api/logout";

		public async Task<bool> Logout(User user)
		{
			_userUuid = Guid.Empty;
			_userRepository.Remove(user);
			return true;
			
			// TODO: Сделать выход на сервере.
			using (var client = new HttpClient())
			{
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse($"{user.AccessToken.Type} {user.AccessToken.Body}");

				var response = await client.PostAsync(LogOutUri, null);

				var json = await response.Content.ReadAsStringAsync();

				var data = JsonConvert.DeserializeObject<ResponseDto<object>>(json);
				if (data.Success)
				{
					_userUuid = Guid.Empty;
					_userRepository.Remove(user);
				}

				return data.Success;
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
					if (data.Success)
					{
						var user = _mapper.Map<User>(data.Data.Client);
						user.AccessToken = _mapper.Map<User>(data.Data).AccessToken;

						if (string.IsNullOrEmpty(user.AccessToken.Body) && user.Guid != Guid.Empty)
						{
							return user;
						}

						_userUuid = user.Guid;
						_userRepository.Add(user);

						return user;
					}

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
