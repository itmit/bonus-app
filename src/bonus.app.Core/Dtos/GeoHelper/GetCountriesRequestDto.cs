namespace bonus.app.Core.Dtos.GeoHelper
{
	public class GetCountriesRequestDto
	{
		#region Properties
		public string ApiKey
		{
			get;
			set;
		}

		public CountryFilterDto Filter
		{
			get;
			set;
		}

		public LocaleDto Locale
		{
			get;
			set;
		}

		public OrderDto Order
		{
			get;
			set;
		}

		public PaginationRequestDto Pagination
		{
			get;
			set;
		}
		#endregion
	}
}
