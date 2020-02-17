namespace bonus.app.Core.Models
{
	public class Country
	{
		public int Id
		{
			get;
			set;
		}

		public string Name
		{
			get;
			set;
		}

		public string Iso
		{
			get;
			set;
		}

		public string Iso3
		{
			get;
			set;
		}

		public string IsoNumeric
		{
			get;
			set;
		}

		public string Fips
		{
			get;
			set;
		}

		public string Continent
		{
			get;
			set;
		}

		public string CurrencyCode
		{
			get;
			set;
		}

		public string[] PhonePrefix
		{
			get;
			set;
		}

		public string PostalCodeFormat
		{
			get;
			set;
		}

		public string[] Languages
		{
			get;
			set;
		}

		public LocalizedName LocalizedNames
		{
			get;
			set;
		}
	}
}
