using System;
using Newtonsoft.Json;

namespace bonus.app.Core.Models
{
	public class Message
	{
		#region Prop
		[JsonProperty("content")]
		public string Text
		{
			get;
			set;
		}

		public bool IsTextIn
		{
			get;
			set;
		}

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

		[JsonProperty("is_read")]
		public bool IsRead
		{
			get;
			set;
		}
		#endregion
	}
}
