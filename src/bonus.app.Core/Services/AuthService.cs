using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using bonus.app.Core.Dtos;
using bonus.app.Core.Helpers;
using bonus.app.Core.Models;
using bonus.app.Core.Models.UserModels;
using bonus.app.Core.Repositories;
using Microsoft.AppCenter.Crashes;
using MvvmCross;
using Newtonsoft.Json;

namespace bonus.app.Core.Services
{
	public class AuthService : IAuthService
	{
		#region Data
		#region Consts
		private const string LoginUri = "http://bonus.itmit-studio.ru/api/login";

		private const string RegisterUri = "http://bonus.itmit-studio.ru/api/register";
		#endregion

		#region Fields
		private readonly Mapper _mapper;
		private readonly IUserRepository _userRepository;
		private Guid _userUuid = Guid.Empty;
		private readonly HttpClient _httpClient;
		private bool? _userIsAuthorized;
		#endregion
		#endregion

		#region .ctor
		public AuthService(IUserRepository userRepository)
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
			_httpClient = new HttpClient();
			_httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(BaseService.ApplicationJson));
		}
		#endregion

		#region IAuthService members
		public string Error
		{
			get;
			private set;
		}

		public Dictionary<string, string[]> ErrorDetails
		{
			get;
			private set;
		} = new Dictionary<string, string[]>();

		public async Task<User> Login(AuthDto authData)
		{
			try
			{
				var service = Mvx.IoCProvider.Resolve<IFirebaseService>();
				if (service != null)
				{
					authData.DeviceToken = await service.CreateToken(Secrets.SenderId);
				}
			}
			catch (Exception e)
			{
				Crashes.TrackError(e, new Dictionary<string, string>());
				Console.WriteLine(e);
			}

			var json = JsonConvert.SerializeObject(authData);
			Debug.WriteLine(json);
			var response = await _httpClient.PostAsync(LoginUri, new StringContent(json, Encoding.UTF8, BaseService.ApplicationJson));

			var jsonString = await response.Content.ReadAsStringAsync();
			Debug.WriteLine(jsonString);

			if (string.IsNullOrEmpty(jsonString))
			{
				Error = "Нет ответа от сервера";
				return null;
			}

			var data = JsonConvert.DeserializeObject<ResponseDto<UserDto>>(jsonString);

			if (response.IsSuccessStatusCode)
			{
				if (!data.Success)
				{
					return _mapper.Map<User>(data.Data);
				}

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

				_userUuid = userInfo.Uuid;
				_userRepository.RemoveAll();
				_userRepository.Add(userInfo);

				UserIsAuthorized = true;
				TokenUpdated?.Invoke(this, EventArgs.Empty);

				return userInfo;
			}

			if (data.ErrorDetails != null)
			{
				ErrorDetails = data.ErrorDetails;
			}

			Error = data.Error;
			return null;
		}

		private const string SendRecoveryCodeUri = "http://bonus.itmit-studio.ru/api/sendCode";

		public async Task<bool> SendRecoveryCode(string email)
		{
			var response = await _httpClient.PostAsync(SendRecoveryCodeUri, new FormUrlEncodedContent(new Dictionary<string, string>
			{
				{"email", email }
			}));

			var jsonString = await response.Content.ReadAsStringAsync();
			Debug.WriteLine(jsonString);

			if (string.IsNullOrEmpty(jsonString))
			{
				Error = "Нет ответа от сервера";
				return false;
			}
			
			var data = JsonConvert.DeserializeObject<ResponseDto<object>>(jsonString);
			if (data.Success)
			{
				return true;
			}

			if (data.ErrorDetails != null)
			{
				ErrorDetails = data.ErrorDetails;
			}

			Error = data.Error;

			return false;
		}

		private const string RecoveryUri = "http://bonus.itmit-studio.ru/api/resetPassword";

		public async Task<bool> Recovery(string email, string code, string password, string passwordConfirmation)
		{
			var response = await _httpClient.PostAsync(RecoveryUri, new FormUrlEncodedContent(new Dictionary<string, string>
			{
				{"email", email },
				{"code", code },
				{"password", password },
				{"password_confirmation", passwordConfirmation },
			}));

			var jsonString = await response.Content.ReadAsStringAsync();
			Debug.WriteLine(jsonString);
			return !string.IsNullOrEmpty(jsonString) && response.IsSuccessStatusCode;
		}

		private const string LogoutUri = "http://bonus.itmit-studio.ru/api/logout";

		public async Task<bool> Logout(User user)
		{
			_userRepository.RemoveAll();

			_userUuid = Guid.Empty;

			try
			{
				using (var httpClient = new HttpClient())
				{
					httpClient.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse(user.AccessToken.ToString());

					var response = await httpClient.PostAsync(LogoutUri, new StringContent("{}", Encoding.UTF8, BaseService.ApplicationJson));

					var jsonString = await response.Content.ReadAsStringAsync();
					Debug.WriteLine(jsonString);
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}

			UserIsAuthorized = false;
			TokenUpdated?.Invoke(this, EventArgs.Empty);
			try
			{
				Mvx.IoCProvider.Resolve<IFirebaseService>()?.DeleteInstance(Secrets.SenderId);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}

			return true;
		}

		public async Task<User> Register(User user, string password, string confirmPassword)
		{
			var regDto = new RegisterDto
			{
				Email = user.Email,
				Name = user.Name,
				Login = user.Login,
				Password = password,
				PasswordConfirm = confirmPassword
			};
			switch (user.Role)
			{
				case UserRole.Businessman:
					regDto.Type = "businessman";
					break;
				case UserRole.Customer:
					regDto.Type = "customer";
					break;
				case UserRole.Manager:
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}

			var requestBody = JsonConvert.SerializeObject(regDto);

			Debug.WriteLine(requestBody);

			var response = await _httpClient.PostAsync(RegisterUri, new StringContent(requestBody, Encoding.UTF8, BaseService.ApplicationJson));

			var jsonString = await response.Content.ReadAsStringAsync();
			Debug.WriteLine(jsonString);

			var data = JsonConvert.DeserializeObject<ResponseDto<UserDto>>(jsonString);

			if (data.Success)
			{
				user.Uuid = data.Data.Uuid;
				return user;
			}

			if (data.ErrorDetails != null)
			{
				ErrorDetails = data.ErrorDetails;
				Error = data.ErrorDetails.First()
							.Value.FirstOrDefault();
				return null;
			}

			Error = data.Error;

			return null;
		}

		private const string AuthorizationAnExternalServiceUri = "http://bonus.itmit-studio.ru/api/authorizationAnExternalService";

		public async Task<User> AuthorizationAnExternalService(string email, string accessToken, ExternalAuthService authServiceType)
		{
			var request = new Dictionary<string, string>
			{
				{"email", email },
				{"access_token", accessToken },
			};
			switch (authServiceType)
			{
				case ExternalAuthService.Vk:
					request.Add("service", "vk");
					break;
				case ExternalAuthService.Facebook:
					request.Add("service", "facebook");
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(authServiceType), authServiceType, null);
			}

			try
			{
				var service = Mvx.IoCProvider.Resolve<IFirebaseService>();
				if (service != null)
				{
					var deviceToken = await service.CreateToken(Secrets.SenderId);
					if (!string.IsNullOrWhiteSpace(deviceToken))
					{
						request.Add("device_token", deviceToken);

					}
				}
			}
			catch (Exception e)
			{
				Crashes.TrackError(e, new Dictionary<string, string>());
				Console.WriteLine(e);
			}

			
			var response = await _httpClient.PostAsync(AuthorizationAnExternalServiceUri, new FormUrlEncodedContent(request));

			var jsonString = await response.Content.ReadAsStringAsync();
			Debug.WriteLine(jsonString);

			if (string.IsNullOrEmpty(jsonString))
			{
				Error = "Нет ответа от сервера";
				return null;
			}

			var data = JsonConvert.DeserializeObject<ResponseDto<UserDto>>(jsonString);

			if (response.IsSuccessStatusCode)
			{
				if (!data.Success)
				{
					return _mapper.Map<User>(data.Data);
				}

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

				_userUuid = userInfo.Uuid;
				_userRepository.Add(userInfo);
				TokenUpdated?.Invoke(this, EventArgs.Empty);
				return userInfo;
			}

			if (data.ErrorDetails != null)
			{
				ErrorDetails = data.ErrorDetails;
			}

			Error = data.Error;
			return null;
		}

		public AccessToken Token => User?.AccessToken;

		public User User
		{
			get
			{
				try
				{
					if (_userUuid != Guid.Empty)
					{
						var user = _userRepository.Find(_userUuid);
						if (user != null)
						{
							return user;
						}
					}

					var u = _userRepository.GetAll()
										   .SingleOrDefault();
					if (u != null)
					{
						_userUuid = u.Uuid;
					}

					return u;
				}
				catch (Exception e)
				{
					Console.WriteLine(e);
				}

				return null;
			}
		}

		public bool UserIsAuthorized
		{
			get
			{
				if (_userIsAuthorized == null)
				{
					_userIsAuthorized = CheckUserIsAuthorized();
				}

				return _userIsAuthorized.Value;
			}
			private set => _userIsAuthorized = value;
		}

		private bool CheckUserIsAuthorized()
		{
			if (Token == null)
			{
				return false;
			}

			var profile = Mvx.IoCProvider.Resolve<IProfileService>().GetUser();

			if (profile.GetAwaiter()
					   .GetResult() !=
				null)
			{
				return true;
			}

			_userRepository.RemoveAll();

			_userUuid = Guid.Empty;

			return false;
		}

		public event EventHandler TokenUpdated;
		#endregion
	}
}
