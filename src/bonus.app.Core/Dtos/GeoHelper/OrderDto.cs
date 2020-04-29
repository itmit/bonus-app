using Newtonsoft.Json;

namespace bonus.app.Core.Dtos.GeoHelper
{
	public class OrderDto
	{
		#region Properties
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
		#endregion
	}
}
