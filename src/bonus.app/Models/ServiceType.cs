using System;
using System.Collections.Generic;

namespace bonus.app.Core.Models
{
	public class ServiceType
	{
		public string Name
		{
			get;
			set;
		}

		public int Id
		{
			get;
			set;
		}

		public Guid Uuid
		{
			get;
			set;
		}

		public List<Service> Services
		{
			get;
			set;
		}
	}
}
