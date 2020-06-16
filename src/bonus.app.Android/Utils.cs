﻿using System;

namespace bonus.app.Droid
{
	public class Utils
	{
		public static DateTimeOffset FromMsDateTime(long? longTimeMillis)
		{
			var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
			return longTimeMillis != null ? epoch.AddMilliseconds(longTimeMillis.Value) : DateTimeOffset.MinValue;
		}
	}
}
