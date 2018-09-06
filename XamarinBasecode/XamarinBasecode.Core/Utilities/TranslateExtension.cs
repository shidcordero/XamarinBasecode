using XamarinBasecode.Core.Resources;
using XamarinBasecode.Core.Services;

namespace XamarinBasecode.Core.Utilities
{
    public static class TranslateExtension
    {
        public static string Translate(this ILocalizeService localizeService, string str)
        {
            var tranlation = AppResources.ResourceManager.GetString(str, localizeService.GetCurrentCultureInfo());
            return string.IsNullOrEmpty(tranlation) ? str : tranlation;
        }
    }
}
