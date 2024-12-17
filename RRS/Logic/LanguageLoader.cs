using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RRS.Logic;

namespace RRS.Logic 
{
    public static class LanguageLoader
    {

        public static Dictionary<string, string> LanguageFiles = new()
        {
            { "EN", $"{Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent}/resources/english.json" },
            { "NL", $"{Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent}/resources/dutch.json" }
        };

        public static string GetLanguagePath(string language)
        {
            if (LanguageFiles.ContainsKey(language))
            {
                return LanguageFiles[language];
            }
            else
            {
                Console.WriteLine("Unsupported Language, default language will be loaded (English)");
                return LanguageFiles["EN"];
            }

        }
        public static Dictionary<string, string> LoadLanguageFiles(string language)
        {
            string JsonFilePath = GetLanguagePath(language);
            return Language.LoadLanguageFile(JsonFilePath);
        }
    }
}
