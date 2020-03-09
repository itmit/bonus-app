using Newtonsoft.Json;

namespace bonus.app.Core.Dtos.GeoHelper
{
	public class OrderDto
	{
		[JsonProperty("by")]
		public string By
		{
			get;
			set;
		}

		[JsonProperty("dir")]
		public string Dir
		{
			get;
			set;
		}
	}
}
