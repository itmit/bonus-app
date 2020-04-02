using System.Collections.Generic;

namespace bonus.app.Core.Dtos.GeoHelper
{
	public class GeoHelperResponseDto<T>
	{
		public bool Success
		{
			get;
			set;
		}

		public string Language
		{
			get;
			set;
		}

		public List<T> Result
		{
			get;
			set;
		}

		public PaginationResponseDto Pagination
		{
			get;
			set;
		}

		public ErrorDto Error
		{
			get;
			set;
		}
	}
}
