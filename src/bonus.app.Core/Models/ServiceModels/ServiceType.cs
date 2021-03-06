﻿using System;
using System.Collections.Generic;

namespace bonus.app.Core.Models.ServiceModels
{
	public class ServiceType
	{
		#region Properties
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

		public List<ServiceTypeItem> Services
		{
			get;
			set;
		}

		public Guid Uuid
		{
			get;
			set;
		}

		public List<ServiceType> SubTypes
		{
			get;
			set;
		}
		#endregion
	}
}
