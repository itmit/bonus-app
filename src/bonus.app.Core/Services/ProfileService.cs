using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AutoMapper;
using bonus.app.Core.Dtos;
using bonus.app.Core.Dtos.BusinessmanDtos;
using bonus.app.Core.Dtos.CustomerDtos;
using bonus.app.Core.Models;
using bonus.app.Core.Repositories;
using Newtonsoft.Json;

namespace bonus.app.Core.Services
{
	public class ProfileService : BaseService, IProfileService
	{
		#region Data
		#region Consts
		private const string FillInfoUri = "http://bonus.itmit-studio.ru/api/fillInfo";
		private const string UpdateUri = "http://bonus.itmit-studio.ru/api/client/{0}";
		private const string GetUserUri = "http://bonus.itmit-studio.ru/api/client/{0}";
		#endregion

		#region Fields
		private readonly Mapper _mapper;
		private readonly IUserRepository _userRepository;
		#endregion
		#endregion

		#region .ctor
		public ProfileService(IAuthService authService, IUserRepository userRepository)
		:base(authService)
		{
			_userRepository = userRepository;
			_mapper = new Mapper(new MapperConfiguration(cfg =>
			{
				cfg.CreateMap<AccessToken, UserDto>();

				cfg.CreateMap<UserDto, User>()
				   .ForPath(m => m.AccessToken.Body, o => o.MapFrom(q => q.Body))
				   .ForPath(m => m.AccessToken.Type, o => o.MapFrom(q => q.Type));
				cfg.CreateMap<UserInfoDto, User>()
				   .ForMember(m => m.PhotoSource, o => o.MapFrom(q => string.IsNullOrWhiteSpace(q.Photo) ? null : BaseService.Domain + q.Photo))
				   .ForMember(m => m.Birthday, o => o.MapFrom(q => q.Birthday ?? DateTime.MinValue));
				cfg.CreateMap<UserData, User>()
				   .ForMember(m => m.Uuid, o => o.MapFrom(q => q.Uuid))
				   .ForMember(m => m.Role, o => o.MapFrom(q => q.Role));
			}));
		}
		#endregion

		#region IProfileService members
		public async Task<User> Edit(EditBusinessmanDto arguments, string imagePath)
		{
			var content = new MultipartFormDataContent
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
				}
			};

			if (!string.IsNullOrWhiteSpace(arguments.Password))
			{
				content.Add(new StringContent(arguments.Password), "password");
			}

			if (!string.IsNullOrEmpty(imagePath))
			{
				var byteArrayContent = new ByteArrayContent(File.ReadAllBytes(imagePath));
				content.Add(byteArrayContent, "\"photo\"", $"\"{imagePath.Substring(imagePath.LastIndexOf('/') + 1)}\"");
			}

			if (AuthService.Token != null)
			{
				content.Add(new StringContent("PUT"), "_method");
				content.Add(new StringContent(arguments.WorkTime), "work_time");

				if (!string.IsNullOrEmpty(arguments.VkLink))
				{
					content.Add(new StringContent(arguments.VkLink), "vk");
				}
				if (!string.IsNullOrEmpty(arguments.InstagramLink))
				{
					content.Add(new StringContent(arguments.InstagramLink), "instagram");
				}
				if (!string.IsNullOrEmpty(arguments.FacebookLink))
				{
					content.Add(new StringContent(arguments.FacebookLink), "facebook");
				}
				if (!string.IsNullOrEmpty(arguments.Odnoklassniki))
				{
					content.Add(new StringContent(arguments.Odnoklassniki), "odnoklassniki");
				}
				if (!string.IsNullOrEmpty(arguments.Email))
				{
					content.Add(new StringContent(arguments.Email), "email");
				}
				if (!string.IsNullOrEmpty(arguments.Name))
				{
					content.Add(new StringContent(arguments.Name), "name");
				}

				if (await Update(content))
				{
					var user = AuthService.User;
					user.City = arguments.City;
					user.Country = arguments.Country;
					user.Address = arguments.Address;
					user.Contact = arguments.Contact;
					user.Description = arguments.Description;
					user.Phone = string.IsNullOrEmpty(arguments.Phone) ? user.Phone : arguments.Phone;
					user.Email = string.IsNullOrEmpty(arguments.Email) ? user.Email : arguments.Email;
					user.WorkTime = arguments.WorkTime;
					user.PhotoSource = string.IsNullOrEmpty(imagePath) ? user.PhotoSource : imagePath;

					user.FacebookLink = arguments.FacebookLink;
					user.VkLink = arguments.VkLink;
					user.InstagramLink = arguments.InstagramLink;
					user.ClassmatesLink = arguments.Odnoklassniki;

					_userRepository.Update(user);
					return user;
				}
			}
			else
			{
				content.Add(new StringContent(arguments.WorkTime), "worktime");
				return await FillInfo(content);
			}

			return null;
		}

		public async Task<User> Edit(EditCustomerDto arguments, string imagePath)
		{
			var content = new MultipartFormDataContent
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
					new StringContent(arguments.Birthday.ToString("yyyy-MM-dd")), "birthday"
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

			if (!string.IsNullOrEmpty(imagePath))
			{
				var byteArrayContent = new ByteArrayContent(File.ReadAllBytes(imagePath));
				content.Add(byteArrayContent, "\"photo\"", $"\"{imagePath.Substring(imagePath.LastIndexOf('/') + 1)}\"");
			}

			if (AuthService.Token != null)
			{
				content.Add(new StringContent("PUT"), "_method");
				if (await Update(content))
				{
					var user = AuthService.User;
					user.City = arguments.City;
					user.Country = arguments.Country;
					user.Address = arguments.Address;
					user.Car = arguments.Car;
					user.Sex = arguments.Sex;
					user.Phone = arguments.Phone;
					user.Birthday = arguments.Birthday;
					user.PhotoSource = imagePath;

					_userRepository.Update(user);
					return user;
				}
			}
			else
			{
				return await FillInfo(content);
			}

			return null;
		}

		private const string GetPortfolioUri = "http://bonus.itmit-studio.ru/api/portfolio";
		private const string PortfolioUri = "http://bonus.itmit-studio.ru/api/portfolio/{0}";

		public async Task<PortfolioImage> AddImageToPortfolio(string imageSource)
		{
			using (var client = new HttpClient())
			{
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(ApplicationJson));
				client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse(AuthService.Token.ToString());
				var response = await client.PostAsync(PortfolioUri, new MultipartFormDataContent
				{
					{
						new ByteArrayContent(File.ReadAllBytes(imageSource)), "\"photo\"", $"\"{imageSource.Substring(imageSource.LastIndexOf('/') + 1)}\""
					}
				});

				var json = await response.Content.ReadAsStringAsync();
				Debug.WriteLine(json);
				
				if (string.IsNullOrEmpty(json))
				{
					return null;
				}

				var data = JsonConvert.DeserializeObject<ResponseDto<PortfolioImage>>(json);

				if (data.Success)
				{
					return data.Data;
				}

				return null;
			}
		}

		public async Task<List<PortfolioImage>> GetPortfolio()
		{
			var images = await GetAsync<List<PortfolioImage>>(GetPortfolioUri);
			if (images == null)
			{
				return new List<PortfolioImage>();
			}

			foreach (var portfolioImage in images)
			{
				if (string.IsNullOrEmpty(portfolioImage.ImageSource))
				{
					portfolioImage.ImageSource = string.Empty;
					continue;
				}

				portfolioImage.ImageSource = Domain + portfolioImage.ImageSource;
			}

			return images;
		}

		public async Task<User> GetUser(Guid uuid)
		{
			using (var client = new HttpClient())
			{
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(ApplicationJson));
				client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse(AuthService.Token.ToString());

				var response = await client.GetAsync(string.Format(GetUserUri, uuid));

				var jsonString = await response.Content.ReadAsStringAsync();
				Debug.WriteLine(jsonString);

				if (string.IsNullOrEmpty(jsonString))
				{
					return null;
				}

				var data = JsonConvert.DeserializeObject<ResponseDto<UserDto>>(jsonString);

				if (response.IsSuccessStatusCode)
				{
					if (data.Success)
					{
						var user = _mapper.Map<User>(data.Data.Client);
						var userInfo = _mapper.Map<User>(data.Data.ClientInfo);

						userInfo.Role = user.Role;
						userInfo.Uuid = user.Uuid;
						userInfo.Email = user.Email ?? string.Empty;
						userInfo.Phone = user.Phone ?? string.Empty;
						userInfo.Name = user.Name ?? string.Empty;
						userInfo.Login = user.Login ?? string.Empty;

						userInfo.AccessToken = new AccessToken
						{
							Body = data.Data.Body,
							Type = data.Data.Type
						};

						return userInfo;
					}

					return _mapper.Map<User>(data.Data);
				}
				return null;
			}
		}

		public async Task<bool> RemoveImageFromPortfolio(Guid uuid)
		{
			using (var client = new HttpClient())
			{
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(ApplicationJson));
				client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse(AuthService.Token.ToString());

				var response = await client.DeleteAsync(string.Format(GetUserUri, uuid));

				return response.IsSuccessStatusCode;
			}
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
		#endregion

		#region Private
		private async Task<User> FillInfo(HttpContent content)
		{
			using (var client = new HttpClient())
			{
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(ApplicationJson));
				var response = await client.PostAsync(FillInfoUri, content);

				var json = await response.Content.ReadAsStringAsync();
				Debug.WriteLine(json);

				var data = JsonConvert.DeserializeObject<ResponseDto<UserDto>>(json);

				if (data.Success)
				{
					var user = _mapper.Map<User>(data.Data.Client);
					var userInfo = _mapper.Map<User>(data.Data.ClientInfo);

					userInfo.Role = user.Role;
					userInfo.Uuid = user.Uuid;
					userInfo.Email = user.Email ?? string.Empty;
					userInfo.Phone = user.Phone ?? string.Empty;
					userInfo.Name = user.Name ?? string.Empty;
					userInfo.Login = user.Login ?? string.Empty;

					userInfo.AccessToken = new AccessToken
					{
						Body = data.Data.Body,
						Type = data.Data.Type
					};

					if (string.IsNullOrEmpty(userInfo.AccessToken.Body) && userInfo.Uuid != Guid.Empty)
					{
						return userInfo;
					}

					_userRepository.Add(userInfo);

					return userInfo;
				}

				if (data.ErrorDetails != null)
				{
					ErrorDetails = data.ErrorDetails;
				}

				Error = data.Error;

				return null;
			}
		}

		private async Task<bool> Update(HttpContent content)
		{
			using (var client = new HttpClient())
			{
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(ApplicationJson));
				client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse(AuthService.Token.ToString());
				var response = await client.PostAsync(string.Format(UpdateUri, AuthService.User.Uuid), content);

				var json = await response.Content.ReadAsStringAsync();
				Debug.WriteLine(json);

				var data = JsonConvert.DeserializeObject<ResponseDto<object>>(json);

				return data.Success;
			}
		}
		#endregion
	}
}
