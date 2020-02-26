using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using bonus.app.Core.Dtos;
using bonus.app.Core.Dtos.BusinessmanDtos;
using bonus.app.Core.Models;
using bonus.app.Core.Repositories;
using Newtonsoft.Json;

namespace bonus.app.Core.Services
{
	public class ServicesService : IServicesService
	{
		private readonly AccessToken _token;
		private readonly Mapper _mapper;
		private const string GetAllUri = "http://bonus.itmit-studio.ru/api/service";

		public ServicesService(IUserRepository repository)
		{
			_token = repository.GetAll().Single().AccessToken;
			_mapper = new Mapper(new MapperConfiguration(cfg =>
			{
				cfg.CreateMap<ServicesDto, ServiceType>()
				   .ForPath(model => model.Id, m => m.MapFrom(dto => dto.Type.Id))
				   .ForPath(model => model.Name, m => m.MapFrom(dto => dto.Type.Name))
				   .ForPath(model => model.Uuid, m => m.MapFrom(dto => dto.Type.Uuid))
				   .ForPath(model => model.Services, m => m.MapFrom(dto => dto.Items));
			}));
		}

		public async Task<IEnumerable<ServiceType>> GetAll()
		{
			using (var client = new HttpClient())
			{
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse($"{_token.Type} {_token.Body}");

				var response = await client.GetAsync(GetAllUri);

				var json = await response.Content.ReadAsStringAsync();
				Debug.WriteLine(json);

				if (string.IsNullOrEmpty(json))
				{
					return null;
				}

				var data = JsonConvert.DeserializeObject<ResponseDto<ServicesDto[]>>(json);
				if (data.Success)
				{
					return _mapper.Map<ServiceType[]>(data.Data);
				}

				return null;
			}
		}
		private const string CreateServiceUri = "http://bonus.itmit-studio.ru/api/businessmanservice";
		public async Task<bool> CreateService(CreateServiceDto createServiceDto)
		{
			using (var client = new HttpClient())
			{
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse($"{_token.Type} {_token.Body}");

				var request = JsonConvert.SerializeObject(createServiceDto);

				var response = await client.PostAsync(CreateServiceUri, new StringContent(request, Encoding.UTF8, "application/json"));

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
	}
}
