using Android.App;
using Android.Runtime;
using Plugin.CurrentActivity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ThePlaceToBe.Data
{

#if DEBUG
	[Application(Debuggable = true)]
#else
	[Application(Debuggable = false)]
#endif

	class MainApplication : Application
    {
		public MainApplication(IntPtr handle, JniHandleOwnership transer) : base(handle, transer) {

		}

		public override void OnCreate() {

			base.OnCreate();
			CrossCurrentActivity.Current.Init(this);
		}
	}
}
