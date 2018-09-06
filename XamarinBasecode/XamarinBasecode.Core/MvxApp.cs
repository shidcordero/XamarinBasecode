using System.Reflection;
using Acr.UserDialogs;
using MvvmCross;
using MvvmCross.Base;
using MvvmCross.IoC;
using MvvmCross.Plugin.Json;
using MvvmCross.ViewModels;
using PCLAppConfig;
using XamarinBasecode.Core.Services;
using XamarinBasecode.Core.Utilities;

namespace XamarinBasecode.Core
{
    public class MvxApp : MvxApplication
    {
        public override void Initialize()
        {
            CreatableTypes()
                .EndingWith(Constants.Common.Service)
                .AsInterfaces()
                .RegisterAsLazySingleton();

            CreatableTypes().
                EndingWith(Constants.Common.Repository)
                .AsTypes()
                .RegisterAsLazySingleton();

            Mvx.RegisterType<IWebService, WebService>();
            Mvx.RegisterType<IAppSettings, AppSettings>();
            Mvx.RegisterType<IMvxJsonConverter, MvxJsonConverter>();
            Mvx.RegisterSingleton(() => UserDialogs.Instance);

            Resources.AppResources.Culture = Mvx.Resolve<ILocalizeService>().GetCurrentCultureInfo();

            var assembly = typeof(MvxApp).GetTypeInfo().Assembly;
            ConfigurationManager.Initialise(assembly.GetManifestResourceStream(Constants.AppSettings.AppConfig));

            RegisterAppStart<ViewModels.MainViewModel>();
        }
    }
}
