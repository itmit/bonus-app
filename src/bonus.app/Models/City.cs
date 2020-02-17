namespace bonus.app.Core.Models
{
	public class City
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

		public string Area
		{
			get;
			set;
		}

		public string PostCode
		{
			get;
			set;
		}

		public int RegionId
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
