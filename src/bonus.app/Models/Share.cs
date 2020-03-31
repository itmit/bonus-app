using System;

namespace bonus.app.Core.Models
{
	public class Share
	{
		public string ImageSource
		{
			get;
			set;
		}

		public string Name
		{
			get;
			set;
		}

		public string ShortDescription
		{
			get;
			set;
		}

		public string Description
		{
			get;
			set;
		}

		public string Status
		{
			get;
			set;
		}

		public DateTimeOffset ShareTime
		{
			get;
			set;
		}
	}
}
