using System.Threading;
using UIKit;
using XamarinBasecode.Core.Services;
using XamarinBasecode.Core.Utilities;
using XamarinBasecode.iOS;

[assembly: Xamarin.Forms.Dependency(typeof(Application.CloseApplication))]
namespace XamarinBasecode.iOS
{
    public class Application
    {
        // This is the main entry point of the application.
        private static void Main(string[] args)
        {
            // if you want to use a different Application Delegate class from "AppDelegate"
            // you can specify it here.
            UIApplication.Main(args, null, Constants.Common.AppDelegate);
        }

        public class CloseApplication : ICloseApplication
        {
            public void ExitApplication()
            {
                Thread.CurrentThread.Abort();
            }
        }
    }
}