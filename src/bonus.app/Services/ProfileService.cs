using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using bonus.app.Core.Dtos;
using bonus.app.Core.Dtos.BusinessmanDtos;
using bonus.app.Core.Dtos.CustomerDtos;
using bonus.app.Core.Models;
using bonus.app.Core.Repositories;
using Newtonsoft.Json;

namespace bonus.app.Core.Services
{
	public class ProfileService : IProfileService
	{
		private readonly Mapper _mapper;
		private readonly bool _isActiveUser;
		private readonly IAuthService _authService;
		private readonly IUserRepository _userRepository;

		public ProfileService(IAuthService authService, IUserRepository userRepository)
		{
			_userRepository = userRepository;
			_mapper = new Mapper(new MapperConfiguration(cfg =>
			{
				cfg.CreateMap<AccessToken, UserDto>();

				cfg.CreateMap<UserDto, User>()
				   .ForPath(m => m.AccessToken.Body, o => o.MapFrom(q => q.Body))
				   .ForPath(m => m.AccessToken.Type, o => o.MapFrom(q => q.Type));
				cfg.CreateMap<UserInfoDto, User>()
				   .ForMember(m => m.PhotoSource, o => o.MapFrom(q => BaseService.Domain + q.Photo))
				   .ForMember(m => m.Birthday, o => o.MapFrom(q => q.Birthday ?? DateTime.MinValue));
				cfg.CreateMap<UserData, User>()
				   .ForMember(m => m.Uuid, o => o.MapFrom(q => q.Uuid))
				   .ForMember(m => m.Role, o => o.MapFrom(q => q.Role));
			}));
			_isActiveUser = authService.Token != null;
			_authService = authService;
		}

		private const string FillInfoUri = "http://bonus.itmit-studio.ru/api/fillInfo";
		private const string EditUri = "http://bonus.itmit-studio.ru/api/client/{0}";

		public Task<User> Edit(EditBusinessmanDto arguments, byte[] photo, string imageName)
		{
			MultipartFormDataContent content = new MultipartFormDataContent
			{
				{
					new StringContent(arguments.City), "city"
				},
				{
					new StringContent(arguments.Country), "country"
				},
				{
					new StringContent(arguments.Address), "address"
				},
				{
					new StringContent(arguments.Contact), "contact"
				},
				{
					new StringContent(arguments.Description), "description"
				},
				{
					new StringContent(arguments.Uuid.ToString()), "uuid"
				},
				{
					new StringContent(arguments.Phone), "phone"
				},
				{
					new StringContent(arguments.WorkTime), "worktime"
				}
			};

			if (!string.IsNullOrWhiteSpace(arguments.Password))
			{
				content.Add(new StringContent(arguments.Password), "password");
			}

			if (photo != null & !string.IsNullOrEmpty(imageName))
			{
				var byteArrayContent = new ByteArrayContent(photo);
				content.Add(byteArrayContent, "\"photo\"", $"\"{imageName}\"");
			}

			return Edit(content);
		}

		public Task<User> Edit(EditCustomerDto arguments, byte[] photo, string imageName)
		{
			MultipartFormDataContent content = new MultipartFormDataContent
			{
				{
					new StringContent(arguments.City), "city"
				},
				{
					new StringContent(arguments.Country), "country"
				},
				{
					new StringContent(arguments.Address), "address"
				},
				{
					new StringContent(arguments.Birthday), "birthday"
				},
				{
					new StringContent(arguments.Car), "car"
				},
				{
					new StringContent(arguments.Uuid.ToString()), "uuid"
				},
				{
					new StringContent(arguments.Phone), "phone"
				},
				{
					new StringContent(arguments.Sex), "sex"
				}
			};

			if (!string.IsNullOrWhiteSpace(arguments.Password))
			{
				content.Add(new StringContent(arguments.Password), "password");
			}

			if (photo != null & !string.IsNullOrEmpty(imageName))
			{
				var byteArrayContent = new ByteArrayContent(photo);
				content.Add(byteArrayContent, "\"photo\"", $"\"{imageName}\"");
			}
			return Edit(content);
		}

		public string Error
		{
			get;
			private set;
		}

		public Dictionary<string, string[]> ErrorDetails
		{
			get;
			private set;
		}

		private async Task<User> Edit(HttpContent content)
		{
			using (var client = new HttpClient())
			{
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				HttpResponseMessage response;
				if(_isActiveUser)
				{
					client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse(_authService.Token.ToString());
					response = await client.PutAsync(string.Format(EditUri, _authService.User.Uuid), content);
				}
				else
				{
					response = await client.PostAsync(FillInfoUri, content);
				}

				var json = await response.Content.ReadAsStringAsync();
				Debug.WriteLine(json);

				var data = JsonConvert.DeserializeObject<ResponseDto<UserDto>>(json);

				if (data.Success)
				{
					var user = _mapper.Map<User>(data.Data.Client);
					var userInfo = _mapper.Map<User>(data.Data.ClientInfo);

					userInfo.Role = user.Role;
					userInfo.Uuid = user.Uuid;
					userInfo.Email = user.Email ?? string.Empty;
					userInfo.Phone = user.Phone ?? string.Empty;
					userInfo.Name = user.Name ?? string.Empty;
					userInfo.Login = user.Login ?? string.Empty;

					userInfo.AccessToken = new AccessToken
					{
						Body = data.Data.Body,
						Type = data.Data.Type
					};

					if (string.IsNullOrEmpty(userInfo.AccessToken.Body) && userInfo.Uuid != Guid.Empty)
					{
						return userInfo;
					}

					_userRepository.Update(userInfo);

					return userInfo;
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
