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
using bonus.app.Core.Models.ServiceModels;
using Newtonsoft.Json;

namespace bonus.app.Core.Services
{
	public class ServicesService : BaseService, IServicesService
	{
		#region Data
		#region Consts
		private const string BusinessmanServicesUri = "http://bonus.itmit-studio.ru/api/businessmanservice";
		private const string BusinessmanServiceUri = "http://bonus.itmit-studio.ru/api/businessmanservice/{0}";
		private const string CreateServiceTypeUri = "http://bonus.itmit-studio.ru/api/service/storeServiceType";
		private const string CreateServiceUri = "http://bonus.itmit-studio.ru/api/service/storeServiceItem";
		private const string GetAllUri = "http://bonus.itmit-studio.ru/api/getAllServices";

		private const string RemoveServiceTypeItemUri = "http://bonus.itmit-studio.ru/api/service/removeServiceItem";
		private const string ServicesUri = "http://bonus.itmit-studio.ru/api/service";
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
			var request = JsonConvert.SerializeObject(createServiceDto);
			var response = await HttpClient.PostAsync(BusinessmanServicesUri, new StringContent(request, Encoding.UTF8, ApplicationJson));
			var json = await response.Content.ReadAsStringAsync();
			Debug.WriteLine(json);
			if (string.IsNullOrEmpty(json))
			{
				return false;
			}
			var data = JsonConvert.DeserializeObject<ResponseDto<object>>(json);
			if (data.Success)
			{
				MyServicesListChanged?.Invoke(this, EventArgs.Empty);
			}
			return data.Success;
		}

		public async Task<ServiceType> CreateServiceType(string name)
		{
			var response = await HttpClient.PostAsync(CreateServiceTypeUri, new StringContent($"{{\"name\":\"{name}\"}}", Encoding.UTF8, ApplicationJson));
			var json = await response.Content.ReadAsStringAsync();
			Debug.WriteLine(json);
			if (string.IsNullOrEmpty(json))
			{
				return null;
			}
			var data = JsonConvert.DeserializeObject<ResponseDto<ServiceType>>(json);
			return data.Data;
		}

		public async Task<ServiceTypeItem> CreateServiceTypeItem(string name, Guid serviceTypeUuid)
		{
			var response = await HttpClient.PostAsync(CreateServiceUri,
												  new StringContent($"{{\"name\":\"{name}\",\"uuid\":\"{serviceTypeUuid}\"}}", Encoding.UTF8, ApplicationJson));
			var json = await response.Content.ReadAsStringAsync();
			Debug.WriteLine(json);
			if (string.IsNullOrEmpty(json))
			{
				return null;
			}
			var data = JsonConvert.DeserializeObject<ResponseDto<ServiceTypeItem>>(json);
			return data.Data;
		}

		public async Task<List<Service>> GetAllServices()
		{
			var services = await GetAsync<List<Service>>(GetAllUri);
			if (services == null)
			{
				return new List<Service>();
			}
			foreach (var service in services)
			{
				service.Client.PhotoSource = Domain + service.Client.PhotoSource;
			}
			return services;
		}

		public async Task<IEnumerable<Service>> GetBusinessmenService() => await GetAsync<IEnumerable<Service>>(BusinessmanServicesUri);

		public async Task<IEnumerable<Service>> GetBusinessmenService(Guid businessmenUuid) =>
			await GetAsync<IEnumerable<Service>>(string.Format(BusinessmanServiceUri, businessmenUuid));

		public async Task<IEnumerable<ServiceType>> GetMyServices()
		{
			var dtos = await GetAsync<IEnumerable<ServicesDto>>(ServicesUri);
			if (dtos == null)
			{
				return new List<ServiceType>();
			}
			return _mapper.Map<ServiceType[]>(dtos);
		}

		public async Task<bool> RemoveServiceTypeItem(Guid uuid)
		{
			var response = await HttpClient.PostAsync(RemoveServiceTypeItemUri, new StringContent($"{{\"uuid\":\"{uuid}\"}}", Encoding.UTF8, ApplicationJson));
			var json = await response.Content.ReadAsStringAsync();
			Debug.WriteLine(json);
			var data = JsonConvert.DeserializeObject<ResponseDto<object>>(json);
			return data.Success;
		}

		public async Task<bool> UpdateService(CreateServiceDto service, Guid uuid)
		{
			var request = JsonConvert.SerializeObject(service);
			var response = await HttpClient.PutAsync(string.Format(BusinessmanServiceUri, uuid), new StringContent(request, Encoding.UTF8, ApplicationJson));
			var json = await response.Content.ReadAsStringAsync();
			Debug.WriteLine(json);
			var data = JsonConvert.DeserializeObject<ResponseDto<object>>(json);
			if (data.Success)
			{
				MyServicesListChanged?.Invoke(this, EventArgs.Empty);
			}
			return data.Success;
		}

		public event EventHandler MyServicesListChanged;
		#endregion
	}
}
