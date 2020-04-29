using System.Collections.Generic;

namespace bonus.app.Core.Dtos.GeoHelper
{
	public class GeoHelperResponseDto<T>
	{
		#region Properties
		public ErrorDto Error
		{
			get;
			set;
		}

		public string Language
		{
			get;
			set;
		}

		public PaginationResponseDto Pagination
		{
			get;
			set;
		}

		public List<T> Result
		{
			get;
			set;
		}

		public bool Success
		{
			get;
			set;
		}
		#endregion
	}
}
