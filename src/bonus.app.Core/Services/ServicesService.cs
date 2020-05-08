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
using bonus.app.Core.Models;
using Newtonsoft.Json;

namespace bonus.app.Core.Services
{
	public class ServicesService : BaseService, IServicesService
	{
		#region Data
		#region Consts
		private const string CreateServiceTypeUri = "http://bonus.itmit-studio.ru/api/service/storeServiceType";
		private const string CreateServiceUri = "http://bonus.itmit-studio.ru/api/service/storeServiceItem";
		private const string GetAllUri = "http://bonus.itmit-studio.ru/api/service";

		private const string ServiceUri = "http://bonus.itmit-studio.ru/api/businessmanservice";
		#endregion

		#region Fields
		private readonly Mapper _mapper;
		#endregion
		#endregion

		#region .ctor
		public ServicesService(IAuthService authService)
			: base(authService)
		{
			_mapper = new Mapper(new MapperConfiguration(cfg =>
			{
				cfg.CreateMap<ServicesDto, ServiceType>()
				   .ForPath(model => model.Id, m => m.MapFrom(dto => dto.Type.Id))
				   .ForPath(model => model.Name, m => m.MapFrom(dto => dto.Type.Name))
				   .ForPath(model => model.Uuid, m => m.MapFrom(dto => dto.Type.Uuid))
				   .ForPath(model => model.Services, m => m.MapFrom(dto => dto.Items));
			}));
		}
		#endregion

		#region IServicesService members
		public async Task<bool> CreateService(CreateServiceDto createServiceDto)
		{
			using (var client = new HttpClient())
			{
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(ApplicationJson));
				client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse(AuthService.Token.ToString());

				var request = JsonConvert.SerializeObject(createServiceDto);

				var response = await client.PostAsync(ServiceUri, new StringContent(request, Encoding.UTF8, ApplicationJson));

				var json = await response.Content.ReadAsStringAsync();
				Debug.WriteLine(json);

				if (string.IsNullOrEmpty(json))
				{
					return false;
				}

				var data = JsonConvert.DeserializeObject<ResponseDto<object>>(json);

				return data.Success;
			}
		}

		public async Task<ServiceTypeItem> CreateServiceTypeItem(string name, Guid serviceTypeUuid)
		{
			using (var client = new HttpClient())
			{
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(ApplicationJson));
				client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse(AuthService.Token.ToString());
				var response = await client.PostAsync(CreateServiceUri,
													  new StringContent($"{{\"name\":\"{name}\",\"uuid\":\"{serviceTypeUuid}\"}}", Encoding.UTF8, ApplicationJson));

#if DEBUG
				var json = await response.Content.ReadAsStringAsync();
				Debug.WriteLine(json);
#endif
				if (string.IsNullOrEmpty(json))
				{
					return null;
				}

				var data = JsonConvert.DeserializeObject<ResponseDto<ServiceTypeItem>>(json);

				return data.Data;
			}
		}

		private const string RemoveServiceTypeItemUri = "http://bonus.itmit-studio.ru/api/service/removeServiceItem";

		public async Task<bool> RemoveServiceTypeItem(Guid uuid)
		{
			using (var client = new HttpClient())
			{
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(ApplicationJson));
				client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse(AuthService.Token.ToString());
				var response = await client.PostAsync(RemoveServiceTypeItemUri,
													  new StringContent($"{{\"uuid\":\"{uuid}\"}}", Encoding.UTF8, ApplicationJson));

#if DEBUG
				var json = await response.Content.ReadAsStringAsync();
				Debug.WriteLine(json);
#endif
				return response.IsSuccessStatusCode;
			}
		}

		public async Task<ServiceType> CreateServiceType(string name)
		{
			using (var client = new HttpClient())
			{
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(ApplicationJson));
				client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse(AuthService.Token.ToString());
				var response = await client.PostAsync(CreateServiceTypeUri, new StringContent($"{{\"name\":\"{name}\"}}", Encoding.UTF8, ApplicationJson));

				var json = await response.Content.ReadAsStringAsync();
				Debug.WriteLine(json);
				if (string.IsNullOrEmpty(json))
				{
					return null;
				}

				var data = JsonConvert.DeserializeObject<ResponseDto<ServiceType>>(json);

				return data.Data;
			}
		}

		public async Task<IEnumerable<ServiceType>> GetAll()
		{
			var dtos = await GetAsync<IEnumerable<ServicesDto>>(GetAllUri);
			if (dtos == null)
			{
				return new List<ServiceType>();
			}

			return _mapper.Map<ServiceType[]>(dtos);
		}

		public async Task<IEnumerable<Service>> GetBusinessmenService() => await GetAsync<IEnumerable<Service>>(ServiceUri);
		#endregion
	}
}
