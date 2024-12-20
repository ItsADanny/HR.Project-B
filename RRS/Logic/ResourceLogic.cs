using System.Globalization;
using System.Resources;

//{21-11-2024}-{MICK}---->
//Resource manager and localization logic class for everything language interface
//logic layer
public class ENInterface : ILanguageInterface
{
    private readonly ResourceManager _resourcemanager;

    public ENInterface()
    {
        _resourcemanager = new ResourceManager("Resources.Resources",Assembly.GetExecutingAssembly());
    }

    public void SetCulture(string culture)
    {
        string LanguagePreference = cultures[selectedOption];
        CultureInfo originalCulture = Thread.CurrentThread.CurrentCulture;

        try
        {
            CultureInfo newCulture = new CultureInfo(LanguagePreference);
            Thread.CurrentThread.CurrentCulture = newCulture;
            Thread.CurrentThread.CurrentUICulture = newCulture;
            Console.WriteLine("Current Language has been set to English Succesfully");
        }
        catch (CultureNotFoundException ex)
        {    
            Console.WriteLine("Unable to change current language to English", ex.InvalidCultureName);
        }
    }

    public string GetString(string key)
    {
        return _resourcemanager.GetString(key, Thread.CurrentThread.CurrentCulture) ?? key;
    }

}

public class NLInterface : ILanguageInterface
{
    private readonly ResourceManager _resourcemanager;

    public NLInterface()
    {
        _resourcemanager = new ResourceManager("Resources.Resources", Assembly.GetExecutingAssembly());
    }

    public void SetCulture(string culture)
    {
        string LanguagePreference = cultures[selectedOption];
        CultureInfo originalCulture = Thread.CurrentThread.CurrentCulture;

        try
        {
            CultureInfo newCulture = new CultureInfo(LanguagePreference);
            Thread.CurrentThread.CurrentCulture = newCulture;
            Thread.CurrentThread.CurrentUICulture = newCulture;
            Console.WriteLine("Current Language has been set to Dutch Succesfully");
        }
        catch (CultureNotFoundException ex)
        {
            Console.WriteLine("Unable to change current language to Dutch", ex.InvalidCultureName);
        }
    }


    public string GetString(string key)
    {
        return _resourcemanager.GetString(key, Thread.CurrentThread.CurrentCulture) ?? key;
    }
}

