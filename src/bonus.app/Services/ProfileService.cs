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

		public Task<User> Edit(EditBusinessmanDto arguments, byte[] photo, string imageName)
		{
			var byteArrayContent = new ByteArrayContent(photo);
			MultipartFormDataContent content = new MultipartFormDataContent
			{
				{
					byteArrayContent, "\"photo\"", $"\"{imageName}\""
				},
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
				},
				{
					new StringContent(arguments.Password), "password"
				}
			};
			return Edit(content);
		}

		public Task<User> Edit(EditCustomerDto arguments, byte[] photo, string imageName)
		{
			var byteArrayContent = new ByteArrayContent(photo);
			MultipartFormDataContent content = new MultipartFormDataContent
			{
				{
					byteArrayContent, "\"photo\"", $"\"{imageName}\""
				},
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
				},
				{
					new StringContent(arguments.Password), "password"
				}
			};

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

				var response = await client.PostAsync(EditUri, content);

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
