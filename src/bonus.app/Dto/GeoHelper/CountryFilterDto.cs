using Newtonsoft.Json;

namespace bonus.app.Core.Dto.GeoHelper
{
	public class CountryFilterDto
	{
		[JsonProperty("name")]
		public string Name
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
		
		[JsonProperty("fips")]
		public string Fips
		{
			get;
			set;
		}
	}
}
