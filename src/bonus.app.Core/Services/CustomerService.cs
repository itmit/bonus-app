﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AutoMapper;
using bonus.app.Core.Dtos;
using bonus.app.Core.Models;
using Newtonsoft.Json;

namespace bonus.app.Core.Services
{
	public class CustomerService : ICustomerService
	{
		#region Data
		#region Consts
		private const string GetCustomerByLoginUri = "http://bonus.itmit-studio.ru/api/service/searchCustomer";

		private const string GetCustomerByUuidUri = "http://bonus.itmit-studio.ru/api/service/getCustomerByUUID";
		#endregion

		#region Fields
		private readonly IAuthService _authService;
		private readonly Mapper _mapper;
		#endregion
		#endregion

		#region .ctor
		public CustomerService(IAuthService authService)
		{
			_authService = authService;
			_mapper = new Mapper(new MapperConfiguration(cfg =>
			{
				cfg.CreateMap<AccessToken, UserDto>();

				cfg.CreateMap<UserDto, User>()
				   .ForPath(m => m.AccessToken.Body, o => o.MapFrom(q => q.Body))
				   .ForPath(m => m.AccessToken.Type, o => o.MapFrom(q => q.Type));
				cfg.CreateMap<UserData, User>()
				   .ForPath(m => m.Uuid, o => o.MapFrom(q => q.Uuid))
				   .ForPath(m => m.PhotoSource, o => o.MapFrom(q => BaseService.Domain + q.Photo))
				   .ForPath(m => m.Role, o => o.MapFrom(q => q.Role));
			}));
		}
		#endregion

		#region ICustomerService members
		public async Task<User> GetCustomerByLogin(string login)
		{
			using (var client = new HttpClient())
			{
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(BaseService.ApplicationJson));
				client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse(_authService.Token.ToString());

				var response = await client.PostAsync(GetCustomerByLoginUri,
													  new FormUrlEncodedContent(new Dictionary<string, string>
													  {
														  {
															  "login", login
														  }
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
					var user = _mapper.Map<User>(data.Data);
					var userInfo = _mapper.Map<User>(data.Data.Client);
					userInfo.Balance = user.Balance / 100;
					
					return userInfo;
				}

				return null;
			}
		}

		public async Task<User> GetCustomerByUuid(Guid uuid)
		{
			using (var client = new HttpClient())
			{
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(BaseService.ApplicationJson));
				client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse(_authService.Token.ToString());

				var response = await client.PostAsync(GetCustomerByUuidUri,
													  new FormUrlEncodedContent(new Dictionary<string, string>
													  {
														  {
															  "uuid", uuid.ToString()
														  }
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
					var user = _mapper.Map<User>(data.Data);
					var userInfo = _mapper.Map<User>(data.Data.Client);
					userInfo.Balance = user.Balance / 100;
					
					return userInfo;
				}

				return null;
			}
		}
		#endregion
	}
}
