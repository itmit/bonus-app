using System;
using System.Collections.Generic;
using Microsoft.AppCenter.Crashes;
using UIKit;

namespace bonus.app.iOS
{
	public class Application
	{
		#region Private
		// This is the main entry point of the application.
		private static void Main(string[] args)
		{
			// if you want to use a different Application Delegate class from "AppDelegate"
			// you can specify it here.
			try
			{
				UIApplication.Main(args, null, "AppDelegate");
			}
			catch (Exception e)
			{
				Crashes.TrackError(e);
				throw;
			}
		}
		#endregion
	}
}
