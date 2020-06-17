using System;
using Android.App;
using Android.Runtime;
using Plugin.CurrentActivity;
using VKontakte;

namespace bonus.app.Droid
{
	#if DEBUG
		[Application(Debuggable = true)]
	#else
		[Application(Debuggable = false)]
	#endif
	public class MainApplication : Application
	{
		public MainApplication(IntPtr handle, JniHandleOwnership transer)
			: base(handle, transer)
		{
		}

		public override void OnCreate()
		{
			base.OnCreate();
			VKSdk.Initialize(this).WithPayments();
			CrossCurrentActivity.Current.Init(this);
		}
    }
}
