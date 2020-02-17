namespace bonus.app.Core.Dto.GeoHelper
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

		public T[] Result
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
