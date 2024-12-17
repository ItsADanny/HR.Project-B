using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace RRS.Logic
{

    public static class Language
    {
        public static Dictionary<string, string> LoadLanguageFile(string jsonFilePath)
        {
            Console.WriteLine(jsonFilePath);
            if (!File.Exists(jsonFilePath))
            {
                throw new FileNotFoundException("JSON containing language file does not exist.");
            }

            try
            {
                string jsonContent = File.ReadAllText(jsonFilePath);
                Dictionary<string, string>? translations = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonContent);

                if (translations == null)
                {
                    throw new InvalidDataException("No JSON could be retrieved to deserialize into dictionary");
                }

                return translations;

            }
            catch (JsonException ex)
            {
                throw new InvalidDataException("an Error Occurred, could not process the JSON file", ex);
            }
                
        }

        public static void SaveTranslations(Dictionary<string, string>? translations, string jsonFilePath)
        {
            try
            {
                string JsonContent = JsonConvert.SerializeObject(translations, Formatting.Indented);
                File.WriteAllText(jsonFilePath, JsonContent);
            }
            catch (IOException ex)
            {
                throw new IOException("There was an error while trying to write to the json", ex);

            }
            
        }

        public static void RemoveTranslations(Dictionary<string, string> translations, string key, string jsonFilePath)
        {
            try
            {
                if (translations == null)
                {
                    throw new ArgumentNullException("Translations could not be found");
                }

                if (translations.ContainsKey(key))
                {
                    translations.Remove(key);
                    SaveTranslations(translations, jsonFilePath);
                }
                else
                {
                    throw new KeyNotFoundException("the given key was not found");
                }
            }
            catch (KeyNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
