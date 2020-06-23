﻿
using System.Collections.Generic;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace bonus.app.Core.Models.Statistic
{
	public class Line
	{
		[JsonProperty("views")]
		public List<Point> Points
		{
			get;
			set;
		}

		public string Name
		{
			get;
			set;
		}

		public Color Color
		{
			get;
			set;
		}
	}
}
