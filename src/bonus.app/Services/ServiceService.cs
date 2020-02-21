using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using bonus.app.Core.Dtos;
using bonus.app.Core.Models;
using bonus.app.Core.Repositories;
using Newtonsoft.Json;

namespace bonus.app.Core.Services
{
	public class ServiceService : IServiceService
	{
		private readonly AccessToken _token;
		private const string GetAllUri = "http://bonus.itmit-studio.ru/api/service";

		public ServiceService(IUserRepository repository)
		{
			_token = repository.GetAll().Single().AccessToken;
		}

		public async Task<IEnumerable<Service>> GetAll()
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

				var data = JsonConvert.DeserializeObject<ResponseDto<Service[]>>(json);
				if (data.Success)
				{
					return data.Data;
				}

				return null;
			}
		}
	}
}
