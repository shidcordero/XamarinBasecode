using MvvmCross.Forms.Platforms.Uap.Views;
using XamarinBasecode.Core.Services;
using XamarinBasecode.UWP;

[assembly: Xamarin.Forms.Dependency(typeof(App.CloseApplication))]
namespace XamarinBasecode.UWP
{
    public sealed partial class App
    {
        public App()
        {
            InitializeComponent();
        }

        public class CloseApplication : ICloseApplication
        {
            public void ExitApplication()
            {
                Current.Exit();
            }
        }
    }

    public abstract class UwpApp : MvxWindowsApplication<Setup, Core.MvxApp, Core.FormsApp, MainPage>
    {
    }
}