namespace bonus.app.Core.Dtos.GeoHelper
{
	public class ErrorDto
	{
		#region Properties
		public int Code
		{
			get;
			set;
		}

		public string[] Details
		{
			get;
			set;
		}

		public string Message
		{
			get;
			set;
		}
		#endregion
	}
}
