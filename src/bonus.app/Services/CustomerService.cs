using System;
using System.Collections.Concurrent;
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
using Newtonsoft.Json;

namespace bonus.app.Core.Services
{
	public class CustomerService : ICustomerService
	{
		private readonly Mapper _mapper;
		private readonly AccessToken _token;

		public CustomerService(IUserRepository repository)
		{
			_token = repository.GetAll().Single().AccessToken;
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

		private const string GetCustomerByUuidUri = "http://bonus.itmit-studio.ru/api/service/getCustomerByUUID";

		public async Task<User> GetCustomerByUuid(Guid uuid)
		{
			using (var client = new HttpClient())
			{
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse($"{_token.Type} {_token.Body}");

				var response = await client.PostAsync(GetCustomerByUuidUri, new FormUrlEncodedContent(new Dictionary<string, string>
				{
					{"uuid", uuid.ToString()}
				}));

				var jsonString = await response.Content.ReadAsStringAsync();
				Debug.WriteLine(jsonString);

				if (string.IsNullOrEmpty(jsonString))
				{
					return null;
				}
				var data = JsonConvert.DeserializeObject<ResponseDto<UserDto>>(jsonString);

				if (response.IsSuccessStatusCode)
				{
					return _mapper.Map<User>(data.Data);
				}

				return null;
			}
		}
	}
}
