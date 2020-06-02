using System;
using Newtonsoft.Json;

namespace bonus.app.Core.Models
{
	public class PortfolioImage
	{
		#region Properties
		[JsonProperty("file")]
		public string ImageSource
		{
			get;
			set;
		}

		public Guid Uuid
		{
			get;
			set;
		}

		public string ImageName => ImageSource.Substring(ImageSource.LastIndexOf('/') + 1);
		#endregion
	}
}
