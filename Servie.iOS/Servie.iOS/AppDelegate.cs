using System;
using System.Collections.Generic;
using System.Linq;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Servie.iOS
{
	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		UIWindow window;
		ServieRootViewController rootViewController;
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			window = new UIWindow (UIScreen.MainScreen.Bounds);
			
			rootViewController = new ServieRootViewController ();
			window.RootViewController = rootViewController;
			window.MakeKeyAndVisible ();
			
			return true;
		}
	}
}

