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
using Newtonsoft.Json;

namespace bonus.app.Core.Services
{
	public class ProfileService : IProfileService
	{
		private readonly Mapper _mapper;
		private readonly bool _isActiveUser;
		private readonly IAuthService _authService;

		public ProfileService(IAuthService authService)
		{
			_mapper = new Mapper(new MapperConfiguration(cfg =>
			{
				cfg.CreateMap<AccessToken, UserDto>();

				cfg.CreateMap<UserDto, User>()
				   .ForPath(m => m.AccessToken.Body, o => o.MapFrom(q => q.Body))
				   .ForPath(m => m.AccessToken.Type, o => o.MapFrom(q => q.Type))
				   .ForPath(m => m.Email, o => o.MapFrom(q => q.Email))
				   .ForPath(m => m.Uuid, o => o.MapFrom(q => q.Uuid))
				   .ForPath(m => m.Name, o => o.MapFrom(q => q.Name));
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
