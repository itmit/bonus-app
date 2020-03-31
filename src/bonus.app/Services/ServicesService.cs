using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse(AuthService.Token.ToString());

				var request = JsonConvert.SerializeObject(createServiceDto);

				var response = await client.PostAsync(ServiceUri, new StringContent(request, Encoding.UTF8, "application/json"));

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

		public async Task<IEnumerable<ServiceType>> GetAll() => await GetAsync<IEnumerable<ServiceType>>(GetAllUri);

		public async Task<IEnumerable<Service>> GetBusinessmenService() => await GetAsync<IEnumerable<Service>>(ServiceUri);
		#endregion
	}
}
