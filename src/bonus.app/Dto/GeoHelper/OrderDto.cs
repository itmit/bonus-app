using Newtonsoft.Json;

namespace bonus.app.Core.Dto.GeoHelper
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
