using System;
using System.Resources;
using System.Globalization;
using System.Threading;

//use culture class
public class ResourceLocalizer
{
    private static ResourceManager _resourceManager = new ResourceManager

    public Dictionary<string, string> GetLanguageResource(string Culture)
    {
        CultureInfo culture = new CultureInfo(CultureID);
        ResourceSet? resourceSet = _resourceManager.GetResourceSet(culture, true, true);

        Dictionary<string, string> resourceDictionary = new Dictionary<string, string>();

        if (resourceSet != null)
        {
            foreach(DictionaryEntry entry in resourceSet)
            {
                string? key = entry.Key.ToString();
                string value = entry.Value?.ToString() ?? string.Empty;

                if(key != null)
                {
                    resourceDictionary[key] = value;
                }
            }

        }
        return resourceDictionary;

    }

    public string GetFormattedString(string key, string value)
    {
        string? rawString = _resourceManager.GetString(key);

        if (rawString == null)
        {
            return $"MISSING/EMPTY: {key}";
        }

        return string.Format(rawString, value);
    }


}
