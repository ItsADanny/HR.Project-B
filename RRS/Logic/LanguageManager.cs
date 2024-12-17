using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RRS.Logic;

namespace RRS.Logic
{
    public class LanguageManager
    {
        public string JsonFilePath {get; private set;}
        private Dictionary<string, string> translations;

        public string GetTranslation(string key, params object[] args)
        {
            try
            {
                if (string.IsNullOrEmpty(key))
                {
                    throw new ArgumentNullException($"Missing Translation: {key}");
                }
                else
                {
                    if (translations.TryGetValue(key, out var value))
                    {
                        return string.Format(value, args);
                    } 
                    else 
                    {
                        throw new Exception($"Can't find value for: {key}");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new KeyNotFoundException(key);
            }
        }

        public void AddTranslation(string key, string value) 
        {
            if (string.IsNullOrEmpty(key) || value == null || value.Length == 0)
            {
                throw new ArgumentException("Key or value cannot be empty");

            }

            translations.Add(key, value);
        }

        public void RemoveTranslation(string key)
        {
            try
            {
                Language.RemoveTranslations(translations, key, JsonFilePath);
            }
            catch (KeyNotFoundException ex)
            { 
                throw ex;
            }
        }
    
        public void SetLanguage(string key)
        {
            JsonFilePath = LanguageLoader.GetLanguagePath(key);
            translations = LanguageLoader.LoadLanguageFiles(JsonFilePath);
        }
    }
}
