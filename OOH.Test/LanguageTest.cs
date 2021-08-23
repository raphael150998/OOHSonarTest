using NUnit.Framework;
using OOH.Language;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOH.Test
{
    public class LanguageTest
    {
        [Test]
        public async Task GetAllLanguage_Ok()
        {
            var list = LanguageHelper.GetAllLanguage();

            Assert.IsTrue(list.Count > 0);
        }

        [Test]
        public async Task GetImplementedLanguages_Ok()
        {
            var list = LanguageHelper.GetAllLanguage();
            list = list.Where(x => LanguageHelper.ImplementedLanguages.Contains(x.CultureName)).ToList();
            Assert.IsTrue(list.Count == LanguageHelper.ImplementedLanguages.Count());
        }

        [Test]
        public async Task GetAvailableLanguages_Ok()
        {
            var list = LanguageHelper.GetAvailableLanguages();
            Assert.IsTrue(list.Count == LanguageHelper.ImplementedLanguages.Count());
        }
    }
}
