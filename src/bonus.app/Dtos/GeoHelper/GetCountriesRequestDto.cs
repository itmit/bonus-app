namespace bonus.app.Core.Dto.GeoHelper
{
	public class GetCountriesRequestDto
	{
		public string ApiKey
		{
			get;
			set;
		}

		public LocaleDto Locale
		{
			get;
			set;
		}

		public CountryFilterDto Filter
		{
			get;
			set;
		}

		public PaginationRequestDto Pagination
		{
			get;
			set;
		}

		public OrderDto Order
		{
			get;
			set;
		}
	}
}
