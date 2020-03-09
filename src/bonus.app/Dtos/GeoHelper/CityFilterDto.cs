using Newtonsoft.Json;

namespace bonus.app.Core.Dtos.GeoHelper
{
	public class CityFilterDto
	{
		[JsonProperty("countryIso")]
		public string CountryIso
		{
			get;
			set;
		}

		[JsonProperty("id")]
		public int Id
		{
			get;
			set;
		}

		[JsonProperty("ids")]
		public int[] Ids
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

		[JsonProperty("nameStrictLanguage")]
		public string NameStrictLanguage
		{
			get;
			set;
		}

		[JsonProperty("regionId")]
		public int RegionId
		{
			get;
			set;
		}

		[JsonProperty("regionCodes")]
		public string[] RegionCodes
		{
			get;
			set;
		}
	}
}
