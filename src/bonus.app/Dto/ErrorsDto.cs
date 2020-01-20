using Newtonsoft.Json;

namespace bonus.app.Core.Dto
{
	public class ErrorsDto<T>
	{
		public T Errors
		{
			get;
			set;
		}

		[JsonProperty("error")]
		public string Message
		{
			get;
			set;
		} = "";
	}
}
