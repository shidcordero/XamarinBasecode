using MvvmCross;
using MvvmCross.Forms.Platforms.Uap.Core;
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace XamarinBasecode.UWP
{
    public class Setup : MvxFormsWindowsSetup<Core.MvxApp, Core.FormsApp>
    {
        protected override void InitializeFirstChance()
        {
            base.InitializeFirstChance();

            Mvx.RegisterSingleton<Core.Services.ILocalizeService>(() => new Services.LocalizeService());
            Mvx.RegisterSingleton<ISettings>(() => CrossSettings.Current);
        }
    }
}