using System;
using Newtonsoft.Json;

namespace bonus.app.Core.Dtos
{
	public class DialogDto
	{
		#region Properties
		[JsonProperty("created_at")]
		public DateTime? CreatedAt
		{
			get;
			set;
		}

		public int Id
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

		[JsonProperty("updated_at")]
		public DateTime? UpdatedAt
		{
			get;
			set;
		}

		public string UserLogin
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

		public Guid UserUuid
		{
			get;
			set;
		}
		#endregion
	}
}
