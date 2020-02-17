using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using bonus.app.Core.Dto;
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

		public ProfileService()
		{
			_mapper = new Mapper(new MapperConfiguration(cfg =>
			{
				cfg.CreateMap<AccessToken, UserDto>();

				cfg.CreateMap<UserDto, User>()
				   .ForPath(m => m.AccessToken.Body, o => o.MapFrom(q => q.Body))
				   .ForPath(m => m.AccessToken.Type, o => o.MapFrom(q => q.Type))
				   .ForPath(m => m.Email, o => o.MapFrom(q => q.Email))
				   .ForPath(m => m.Guid, o => o.MapFrom(q => q.Guid))
				   .ForPath(m => m.Name, o => o.MapFrom(q => q.Name));
			}));
		}

		private const string EditUri = "http://bonus.itmit-studio.ru/api/fillInfo";

		public Task<User> Edit(EditBusinessmanDto arguments, byte[] photo)
		{
			return Edit(JsonConvert.SerializeObject(arguments), photo);
		}

		public Task<User> Edit(EditCustomerDto arguments, byte[] photo)
		{
			return Edit(JsonConvert.SerializeObject(arguments), photo);
		}

		private async Task<User> Edit(string requestBody, byte[] photo)
		{
			using (var client = new HttpClient())
			{
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				Debug.WriteLine(requestBody);

				var response = await client.PostAsync(EditUri, new StringContent(requestBody, Encoding.UTF8, "application/json"));

				var json = await response.Content.ReadAsStringAsync();
				Debug.WriteLine(json);

				var data = JsonConvert.DeserializeObject<ResponseDto<UserDto>>(json);

				return _mapper.Map<User>(data.Data);
			}
		}
	}
}
