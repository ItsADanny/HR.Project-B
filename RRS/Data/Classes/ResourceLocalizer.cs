using System;
using System.Resources;
using System.Globalization;
using System.Threading;

//use culture class
public class ResourceLocalizer
{
    private static ResourceManager resourceManager = new ResourceManager

    public static void SetCultureInfo(string Culture)
    {
        CultureInfo culture = new CultureInfo(Culture);
        Thread.CurrentThread.CurrentUICulture = culture;


    }

}
