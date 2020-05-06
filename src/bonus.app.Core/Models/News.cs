using System;
using Newtonsoft.Json;

namespace bonus.app.Core.Models
{
	public class News
	{
		#region Properties

		public Guid Uuid
		{
			get;
			set;
		}

		[JsonProperty("created_at")]
		public string CreatedAt
		{
			get;
			set;
		}

		[JsonProperty("preview_image")]
		public string ImageSource
		{
			get;
			set;
		}

		public string Name
		{
			get;
			set;
		}

		[JsonProperty("description")]
		public string Text
		{
			get;
			set;
		}
		#endregion
	}
}
