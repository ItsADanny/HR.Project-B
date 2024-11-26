public class LanguageManager
{
    
    
    public class ENInterface : ILanguageManager
    {
        private readonly ResourceManager _resourcemanager;

        public ENInterface()
        {
            _resourcemanager = new ResourceManager();
        }

        string selectedCulture = cultures[selectedOption];
        CultureInfo originalCulture = Thread.CurrentThread.CurrentCulture;

        try
        {
            CultureInfo newCulture = new CultureInfo(SelectedCulture);
            Thread.CurrentThread.CurrentCulture = newCulture;
            Thread.CurrentThread.CurrentUICulture = newCulture;

            ResourceManager _resourcemanager = new ResourceManager("Resources.en.resx", typeof(ILanguageInterface).Assembly);
            Console.WriteLine("Current Language has been set to English Succesfully");
        }
        catch (CultureNotFoundException ex)
        {
            
            Console.WriteLine("Unable to change current language to {0}", ex.InvalidCultureName);        }
        }
        finally
        {
            Thread.CurrentThread.CurrentCulture = originalCulture;
            Thread.CurrentThread.CurrentUICulture = originalCulture;
        }
    }

        public class NLInterface : ILanguageManager
    {
        private readonly ResourceManager _resourcemanager;

        public NLInterface()
        {
            _resourcemanager = new ResourceManager();
        }

        string selectedCulture = cultures[selectedOption];
        CultureInfo originalCulture = Thread.CurrentThread.CurrentCulture;

        try
        {
            CultureInfo newCulture = new CultureInfo(SelectedCulture);
            Thread.CurrentThread.CurrentCulture = newCulture;
            Thread.CurrentThread.CurrentUICulture = newCulture;

            ResourceManager _resourcemanager = new ResourceManager("Resources.nl.resx", typeof(ILanguageInterface).Assembly);
            Console.WriteLine("Current Language has been set to Dutch Succesfully");
        }
        catch (CultureNotFoundException ex)
        {
            
            Console.WriteLine("Unable to change current language to {0}", ex.InvalidCultureName);        }
        }
        finally
        {
            Thread.CurrentThread.CurrentCulture = originalCulture;
            Thread.CurrentThread.CurrentUICulture = originalCulture;
        }
    }


    //{23/11/2024}-{MICK}-created a language display skeleton
    //TO STILL IMPLEMENT: 
    //actual resource strings called in here 
    //calling the logic methods
    public static void LanguageSelectScreen()
    {
        resourcelogic ResourceLogic = new RRS.Logic.ResourceLogic();
        while (true)
        {
            Console.Clear();
            Console.WriteLine("      PLEASE SELECT A LANGUAGE      ");
            Console.WriteLine("====================================\n");
            Console.WriteLine("1 - English");
            Console.WriteLine("2 - Nederlands\n\n");
            int selectedOption = 0;
            string selectedCulture = "";
            string userInput = Console.ReadLine();
            if (userInput != null)
            {
                switch(userInput.ToLower())
                {
                    case "1":
                        selectedOption = 1;
                        break;
                    case "2":
                        selectedOption = 2;
                        break;
                     default:
                        Console.WriteLine("Invalid option, Please try again.");
                        break;
                }
            } 
            else
            {
                Console.WriteLine("Please enter a choice");
            } 

            switch (selectedOption)
            {
                case 1:
                   selectedCulture = "en-EN";
                    break;
                case 2:
                    selectedCulture = "nl-NL";
                    break;
               default:
                    Console.WriteLine("Default Language is set to English");
                    break;
            }      

        }
    }
}