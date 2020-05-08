using System;
using bonus.app.Core.Models;
using Newtonsoft.Json;

namespace bonus.app.Core.Dtos
{
	public class DialogDto
	{
		public int Id
		{
			get;
			set;
		}

		public string UserName
		{
			get;
			set;
		}

		public string UserPhoto
		{
			get;
			set;
		}

		public bool IsRead
		{
			get;
			set;
		}

		public bool IsReadShown
		{
			get;
			set;
		}

		[JsonProperty("lastMsg")]
		public string LastMessage
		{
			get;
			set;
		} = string.Empty;

		[JsonProperty("created_at")]
		public DateTime? CreatedAt
		{
			get;
			set;
		}

		[JsonProperty("updated_at")]
		public DateTime? UpdatedAt
		{
			get;
			set;
		}

		public Guid UserUuid
		{
			get;
			set;
		}

		public string UserLogin
		{
			get;
			set;
		}
	}
}
