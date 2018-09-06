using Foundation;
using System.Globalization;
using XamarinBasecode.Core.Utilities;

namespace XamarinBasecode.iOS.Services
{
    public class LocalizeService : Core.Services.ILocalizeService
    {
        public CultureInfo GetCurrentCultureInfo()
        {
            //default language is english
            var netLanguage = Constants.CultureInfo.EnglishUs;
            var prefLanguageOnly = Constants.CultureInfo.EnglishUs;

            if (NSLocale.PreferredLanguages.Length > 0)
            {
                var pref = NSLocale.PreferredLanguages[0];
                prefLanguageOnly = pref.Substring(0, 2);

                //setup other language here
                switch (prefLanguageOnly)
                {
                    case "pt" when pref == "pt":
                        pref = "pt-BR"; // get the correct Brazilian language strings from the PCL RESX (note the local iOS folder is still "pt")
                        break;
                    case "pt":
                        pref = "pt-PT"; // Portugal
                        break;
                }



                netLanguage = pref.Replace("_", "-");
            }

            CultureInfo currentCultureInfo;
            try
            {
                currentCultureInfo = new CultureInfo(netLanguage);
            }
            catch
            {
                // iOS locale not valid .NET culture (eg. "en-ES" : English in Spain)
                // fallback to first characters, in this case "en"
                currentCultureInfo = new CultureInfo(prefLanguageOnly);
            }
            return currentCultureInfo;
        }
    }
}
