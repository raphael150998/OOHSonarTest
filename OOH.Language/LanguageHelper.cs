using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;

namespace OOH.Language
{
    public class LanguageHelper
    {

        public static List<string> ImplementedLanguages = new List<string>()
        {
            "es",
            "en"
        };

        public LanguageHelper()
        {
        }

        /// <summary>
        /// Get all languages supported that can be implemented
        /// </summary>
        /// <returns></returns>
        public static List<LanguageDto> GetAllLanguage() => CultureInfo.GetCultures(CultureTypes.NeutralCultures).Select(x => new LanguageDto() { CultureName = x.Name, FullName = x.DisplayName }).ToList();

        /// <summary>
        /// Get a list of the languages which are implemented
        /// </summary>
        /// <returns></returns>
        public static List<LanguageDto> GetAvailableLanguages() => GetAllLanguage().Where(x => ImplementedLanguages.Contains(x.CultureName)).ToList();

        /// <summary>
        /// Verify if a language is implemented into the systema by its culture neutral name
        /// </summary>
        /// <param name="lang"></param>
        /// <returns></returns>
        public static bool IsLanguageAvailable(string lang) => GetAvailableLanguages().Select(x => x.CultureName).Contains(lang);

        /// <summary>
        /// Get the default implemented language 
        /// </summary>
        /// <returns></returns>
        public static string GetDefaultLanguage() => ImplementedLanguages.FirstOrDefault();

        public void SetLanguage(string lang)
        {
            try
            {
                if (!IsLanguageAvailable(lang)) lang = GetDefaultLanguage() ;
                var cultureInfo = new CultureInfo(lang);
                Thread.CurrentThread.CurrentUICulture = cultureInfo;
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(cultureInfo.Name);
            }
            catch (Exception ex)
            { 
            } 
        }
    }
}
