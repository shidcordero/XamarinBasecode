using Xamarin.Essentials;
using XamarinBasecode.Core.Utilities;

namespace XamarinBasecode.Core.Services
{
    public class AppSettings : IAppSettings
    {
        public string PersistentText
        {
            get => Preferences.Get(Constants.AppSettings.PersistentTextKey, Constants.AppSettings.PersistentTextDefaultValue);
            set => Preferences.Set(Constants.AppSettings.PersistentTextKey, value);
        }

        public string AccessToken
        {
            get => Preferences.Get(Constants.AppSettings.AccessToken, null);
            set => Preferences.Set(Constants.AppSettings.AccessToken, value);
        }

        public string RefreshToken
        {
            get => Preferences.Get(Constants.AppSettings.RefreshToken, null);
            set => Preferences.Set(Constants.AppSettings.RefreshToken, value);
        }
    }
}
