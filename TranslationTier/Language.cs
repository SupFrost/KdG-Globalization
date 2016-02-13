using System.Collections.Generic;
using System.Globalization;
using System.Web.Mvc;

namespace SC.UI.Web.MVC
{
    public static class Language
    {
        public static string language;
      
       
        public static CultureInfo currentCulture = CultureInfo.CurrentUICulture;

        public static void setCulture(string languageCode)
        {
            setCulture(CultureInfo.GetCultureInfo(languageCode));
        }

        public static void setCulture(CultureInfo culture)
        {
            currentCulture = culture;
        }


    }
}