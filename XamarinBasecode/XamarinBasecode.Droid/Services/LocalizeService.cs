using System.Diagnostics;
using System.Globalization;
using XamarinBasecode.Core.Utilities;

namespace XamarinBasecode.Droid.Services
{
    public class LocalizeService : Core.Services.ILocalizeService
    {
        public CultureInfo GetCurrentCultureInfo()
        {
            var androidLocale = Java.Util.Locale.Default;
            var netLanguage = androidLocale.ToString().Replace(Constants.SpecialCharacters.Underscore, Constants.SpecialCharacters.Dash); // turns pt_BR into pt-BR
            try
            {
                return new CultureInfo(netLanguage);
            }
            catch (CultureNotFoundException e)
            {
                Debug.WriteLine(e.Message);
            }

            return new CultureInfo(Constants.CultureInfo.English);
        }
    }
}