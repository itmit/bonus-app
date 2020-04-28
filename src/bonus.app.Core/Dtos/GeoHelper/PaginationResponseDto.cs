namespace bonus.app.Core.Dtos.GeoHelper
{
	public class PaginationResponseDto
	{
		#region Properties
		public int CurrentPage
		{
			get;
			set;
		}

		public int Limit
		{
			get;
			set;
		}

		public int TotalCount
		{
			get;
			set;
		}

		public int TotalPageCount
		{
			get;
			set;
		}
		#endregion
	}
}
