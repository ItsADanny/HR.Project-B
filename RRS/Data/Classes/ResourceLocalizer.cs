using System;
using System.Resources;
using System.Globalization;
using System.Threading;

//use culture class
//data access
public class ResourceLocalizer
{
    private static ResourceManager _resourceManager = new ResourceManager("Resources", typeof(ResourceLocalizer).Assembly);

    public Dictionary<string, string> GetLanguageResource(string Culture)
    {
        CultureInfo culture = new CultureInfo(Culture);
        ResourceSet? resourceSet = _resourceManager.GetResourceSet(culture, true, true);

        Dictionary<string, string> resourceDictionary = new Dictionary<string, string>();

        if (resourceSet != null)
        {
            foreach (DictionaryEntry entry in resourceSet)
            {
                string key = entry.Key.ToString() ?? string.Empty;
                string value = entry.Value?.ToString() ?? string.Empty;

                if(key != null)
                {
                    resourceDictionary[key] = value;
                }
            }

        }
        return resourceDictionary;
    }

    public string GetString(string key, params object[] args)
    {
        string? rawString = _resourceManager.GetString(key, Thread.CurrentThread.CurrentCulture);

        if (rawString == null)
        {
            return $"EMPTY: {key}";
        }

        return string.Format(rawString, args);
    }
}
