﻿namespace bonus.app.Core.Dto.GeoHelper
{
	public class PaginationResponseDto
	{
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

		public int CurrentPage
		{
			get;
			set;
		}

		public int TotalPageCount
		{
			get;
			set;
		}
	}
}