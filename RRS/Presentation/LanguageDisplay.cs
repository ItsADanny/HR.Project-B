using System.Globalization;
using System.Resources;
using System.Security.Cryptography;
//UI layer
public class LanguageDisplay
{

    //{23/11/2024}-{MICK}-created a language display    private ILanguageInterface _languageInterface;
    private ILanguageInterface _languageInterface;
    

    public void LanguageSet(ILanguageInterface languageInterface)
    {
        _languageInterface = languageInterface;
    }




    public static void LanguageSelectScreen(ILanguageInterface languageInterface)
    {

        while (true)
        {
            Console.Clear();
            Console.WriteLine("      PLEASE SELECT A LANGUAGE      ");
            Console.WriteLine("====================================\n");
            Console.WriteLine("1 - English");
            Console.WriteLine("2 - Nederlands\n\n");

            int selectedOption = 0;
            string? userInput = Console.ReadLine();

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
                   languageInterface.SetCulture();
                    break;
                case 2:
                    languageInterface.SetCulture();
                    break;
               default:
                    Console.WriteLine("Default Language is set to English");
                    break;
            }      

            RedirectWithMessage(languageInterface);

        }
    }

    public static void RedirectWithMessage(ILanguageInterface languageInterface)
    {
        string getInterface = languageInterface.GetString("Welcome");
        Console.WriteLine(getInterface);
    }
}