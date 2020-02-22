using System.Collections.Generic;
using bonus.app.Core.Models;

namespace bonus.app.Core.Dtos.BusinessmanDtos
{
	public class ServicesDto
	{
		public ServiceType Type
		{
			get;
			set;
		}

		public List<Service> Items
		{
			get;
			set;
		}
	}
}
