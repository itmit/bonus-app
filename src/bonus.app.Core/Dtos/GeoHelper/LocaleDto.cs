using Newtonsoft.Json;

namespace bonus.app.Core.Dtos.GeoHelper
{
	public class LocaleDto
	{
		#region Properties
		[JsonProperty("fallbackLang")]
		public string FallbackLang
		{
			get;
			set;
		}

		[JsonProperty("lang")]
		public string Lang
		{
			get;
			set;
		}
		#endregion
	}
}
