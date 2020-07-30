using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using bonus.app.Core.Dtos;
using bonus.app.Core.Models;
using bonus.app.Core.Models.ChatModels;
using bonus.app.Core.Models.UserModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace bonus.app.Core.Services
{
	public class ChatsService : BaseService, IChatsService
	{
		#region Data
		#region Consts
		private const string DialogsUri = "http://bonus.itmit-studio.ru/api/dialogs";

		private const string GetMessagesUri = "http://bonus.itmit-studio.ru/api/dialogs/{0}";

		private const string SendMessageUri = "http://bonus.itmit-studio.ru/api/sendMessage";
		#endregion

		#region Fields
		private Guid _guid;
		private readonly Mapper _mapper;
		private List<Dialog> _userDialogs = new List<Dialog>();
		#endregion
		#endregion

		#region .ctor
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
		#endregion

		#region IChatsService members
		public async Task<Dialog> CreateDialog(User user)
		{
			var response = await HttpClient.PostAsync(new Uri(DialogsUri), new StringContent($"{{\"client_uuid\":\"{user.Uuid}\"}}", Encoding.UTF8, ApplicationJson));

			var json = await response.Content.ReadAsStringAsync();
			Debug.WriteLine(json);

			if (string.IsNullOrEmpty(json))
			{
				return null;
			}

			var data = JsonConvert.DeserializeObject<ResponseDto<Dialog>>(json);
			if (!data.Success)
			{
				return null;
			}

			var dialog = new Dialog
			{
				Id = data.Data.Id,
				UserTo = user
			};
			SavedDialogs.Add(dialog);
			return dialog;

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

		public async Task<List<Message>> GetMessages(int dialogId)
		{
			var messages = await GetAsync<List<Message>>(string.Format(GetMessagesUri, dialogId));
			if (messages == null)
			{
				return new List<Message>();
			}

			foreach (var message in messages)
			{
				if (!string.IsNullOrWhiteSpace(message.ImageSource))
				{
					message.ImageSource = Domain + message.ImageSource;
				}
			}

			return messages;
		}

		public List<Dialog> SavedDialogs
		{
			get
			{
				if (_guid.Equals(AuthService.User.Uuid))
				{
					return _userDialogs;
				}

				_userDialogs = new List<Dialog>();
				_guid = AuthService.User.Uuid;

				return _userDialogs;
			}
			private set => _userDialogs = value;
		}

		public async Task<Message> SendMessage(int dialogId, string content, string file)
		{
			var base64 = string.Empty;
			if (!string.IsNullOrWhiteSpace(file))
			{
				base64 = Convert.ToBase64String(File.ReadAllBytes(file));
			}

			var response = await HttpClient.PostAsync(new Uri(SendMessageUri),
												  new StringContent(
													  new JObject(new JProperty("dialog_id", dialogId), new JProperty("content", content), new JProperty("file", base64))
														  .ToString(),
													  Encoding.UTF8,
													  ApplicationJson));

			var json = await response.Content.ReadAsStringAsync();
			Debug.WriteLine(json);

			if (string.IsNullOrEmpty(json))
			{
				return null;
			}

			var data = JsonConvert.DeserializeObject<ResponseDto<Message>>(json);
			if (!data.Success)
			{
				return null;
			}

			if (!string.IsNullOrWhiteSpace(data.Data.ImageSource))
			{
				data.Data.ImageSource = Domain + data.Data.ImageSource;
			}

			return data.Data;
		}
		#endregion
	}
}
