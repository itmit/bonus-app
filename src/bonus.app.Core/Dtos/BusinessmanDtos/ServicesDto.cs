using System.Collections.Generic;
using bonus.app.Core.Models;

namespace bonus.app.Core.Dtos.BusinessmanDtos
{
	public class ServicesDto
	{
		#region Properties
		public List<ServiceTypeItem> Items
		{
			get;
			set;
		}

		public ServiceType Type
		{
			get;
			set;
		}
		#endregion
	}
}
