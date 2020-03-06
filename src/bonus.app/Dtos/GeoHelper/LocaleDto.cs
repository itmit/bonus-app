using Newtonsoft.Json;

namespace bonus.app.Core.Dtos.GeoHelper
{
	public class LocaleDto
	{
		[JsonProperty("lang")]
		public string Lang
		{
			get;
			set;
		}

		[JsonProperty("fallbackLang")]
		public string FallbackLang
		{
			get;
			set;
		}
	}
}
