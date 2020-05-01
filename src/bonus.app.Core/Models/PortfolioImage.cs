using System;
using Newtonsoft.Json;

namespace bonus.app.Core.Models
{
	public class PortfolioImage
	{
		public Guid Uuid
		{
			get;
			set;
		}

		[JsonProperty("file")]
		public string ImageSource 
		{ 
			get;
			set;
		}

		public string ImageName => ImageSource.Substring(ImageSource.LastIndexOf('/') + 1);
	}
}
