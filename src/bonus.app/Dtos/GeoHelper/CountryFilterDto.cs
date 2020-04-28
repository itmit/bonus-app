using Newtonsoft.Json;

namespace bonus.app.Core.Dtos.GeoHelper
{
	public class CountryFilterDto
	{
		#region Properties
		[JsonProperty("fips")]
		public string Fips
		{
			get;
			set;
		}

		[JsonProperty("iso")]
		public string Iso
		{
			get;
			set;
		}

		[JsonProperty("iso3")]
		public string Iso3
		{
			get;
			set;
		}

		[JsonProperty("name")]
		public string Name
		{
			get;
			set;
		}
		#endregion
	}
}
