using MvvmCross;
using MvvmCross.Forms.Platforms.Ios.Core;

namespace XamarinBasecode.iOS
{
    public class Setup : MvxFormsIosSetup<Core.MvxApp, Core.FormsApp>
    {
        protected override void InitializeFirstChance()
        {
            base.InitializeFirstChance();

            Mvx.RegisterSingleton<Core.Services.ILocalizeService>(() => new Services.LocalizeService());
        }
    }
}