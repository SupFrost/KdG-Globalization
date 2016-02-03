using System.Globalization;

namespace TranslationTier
{
    public class Language
    {
        public static CultureInfo currentCulture;

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