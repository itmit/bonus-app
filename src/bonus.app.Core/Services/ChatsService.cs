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
using bonus.app.Core.Dtos.BusinessmanDtos;
using bonus.app.Core.Models;
using Newtonsoft.Json;

namespace bonus.app.Core.Services
{
	public class ChatsService : BaseService, IChatsService
	{
		private List<Dialog> _userDialogs = new List<Dialog>();
		private Guid _guid;
		private Mapper _mapper;

		public ChatsService(IAuthService authService)
			: base(authService)
		{
			_guid = authService.User.Uuid;
			_mapper = new Mapper(new MapperConfiguration(cfg =>
			{
				cfg.CreateMap<DialogDto, Dialog>()
				   .ForPath(model => model.UserTo.Name, m => m.MapFrom(dto => dto.UserName))
				   .ForPath(model => model.UserTo.Login, m => m.MapFrom(dto => dto.UserLogin))
				   .ForPath(model => model.UserTo.Uuid, m => m.MapFrom(dto => dto.UserUuid))
				   .ForPath(model => model.UserTo.PhotoSource, m => m.MapFrom(dto => string.IsNullOrWhiteSpace(dto.UserPhoto) ? null : Domain + dto.UserPhoto))
				   .ForPath(model => model.LastMessage.IsTextIn, m => m.MapFrom(dto => !dto.IsReadShown))
				   .ForPath(model => model.LastMessage.Text, m => m.MapFrom(dto => dto.LastMessage));
			}));
		}

		private const string DialogsUri = "http://bonus.itmit-studio.ru/api/dialogs";

		public async Task<Dialog> CreateDialog(User user)
		{
			using (var client = new HttpClient())
			{
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(ApplicationJson));
				client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse(AuthService.Token.ToString());

				var response = await client.PostAsync(new Uri(DialogsUri), new StringContent($"{{\"client_uuid\":\"{user.Uuid}\"}}", Encoding.UTF8, ApplicationJson));

				var json = await response.Content.ReadAsStringAsync();
				Debug.WriteLine(json);

				if (string.IsNullOrEmpty(json))
				{
					return null;
				}

				var data = JsonConvert.DeserializeObject<ResponseDto<Dialog>>(json);
				if (data.Success)
				{
					var dialog = new Dialog
					{
						Id = data.Data.Id,
						UserTo = user
					};
					SavedDialogs.Add(dialog);
					return dialog;
				}

				return null;
			}
		}

		private const string SendMessageUri = "http://bonus.itmit-studio.ru/api/sendMessage";

		public async Task<Message> SendMessage(int dialogId, string content, string file)
		{
			using (var client = new HttpClient())
			{
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(ApplicationJson));
				client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse(AuthService.Token.ToString());

				var response = await client.PostAsync(new Uri(SendMessageUri), new StringContent($"{{\"dialog_id\":\"{dialogId}\",\"content\":\"{content}\"}}", Encoding.UTF8, ApplicationJson));

				var json = await response.Content.ReadAsStringAsync();
				Debug.WriteLine(json);

				if (string.IsNullOrEmpty(json))
				{
					return null;
				}

				var data = JsonConvert.DeserializeObject<ResponseDto<Message>>(json);
				if (data.Success)
				{
					return data.Data;
				}

				return null;
			}
		}

		private const string GetMessagesUri = "http://bonus.itmit-studio.ru/api/dialogs/{0}";

		public async Task<List<Message>> GetMessages(int dialogId)
		{
			var messages = await GetAsync<List<Message>>(string.Format(GetMessagesUri, dialogId));
			if (messages == null)
			{
				return new List<Message>();
			}

			return messages;
		}

		public List<Dialog> SavedDialogs
		{
			get
			{
				if (!_guid.Equals(AuthService.User.Uuid))
				{
					_userDialogs = new List<Dialog>();
					_guid = AuthService.User.Uuid;
				}

				return _userDialogs;
			}
			private set => _userDialogs = value;
		}

		public async Task<List<Dialog>> GetDialogs()
		{
			var dialogs = await GetAsync<List<DialogDto>>(DialogsUri);
			if (dialogs == null)
			{
				return new List<Dialog>();
			}

			SavedDialogs = _mapper.Map<List<Dialog>>(dialogs);
			return SavedDialogs;
		}
	}
}
