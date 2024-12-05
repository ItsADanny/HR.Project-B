using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;

namespace RRS.Logic;
public class MenuDisplay
{ 
    //-{18/11/2024}-{MICK}
    //added all language interface fields and constructors
    private ILanguageInterface _languageInterface;

    public void LanguageSet(ILanguageInterface languageInterface)
    {
        _languageInterface = languageInterface;
    }

    //-{18/10/2024}-{MICK}
    //created the menu display methods setup
    //-{21/10/2024}-{MICK}
    //added some logic connections 

    public static void MenuItemEditorMenu(int restaurantID, Accounts LoggedinUser)
    {
        while (true)
        {
            //{MICK}- Resource/EN/NL insertion on all strings
            string Header = languageInterface.GetString("MenuEditor");
            string Opt1 = languageInterface.GetString("MenuEditorOptionCreate");
            string Opt2 = languageInterface.GetString("MenuEditorOptionEdit");
            string Opt3 = languageInterface.GetString("MenuEditorOptionDelete");
            string Opt4 = languageInterface.GetString("MenuEditorOptionView");

            Console.WriteLine("====================================");
            Console.WriteLine(Header); //Menu Editor: Please choose an option
            Console.WriteLine("====================================");
            Console.WriteLine(Opt1); //create new item
            Console.WriteLine(Opt2);//edit item
            //edit item, what item would you like to edit?
            //item sleected, what info box would you like to edit?
            //call method
            Console.WriteLine(Opt3);//delete existing item
            Console.WriteLine(Opt4);//view full menu
            string optionChoice = Console.ReadLine().ToUpper();
            string switchText = "";
            try
            {
                switch (optionChoice)
                {
                    case "N":
                        switchText = languageInterface.GetString("SwitchCreate");
                        Console.WriteLine(switchText);
                        Thread.Sleep(1500);
                        CreateItem(restaurantID, LoggedinUser);
                        break;

                    case "E":
                        switchText = languageInterface.GetString("SwitchEdit");
                        Console.WriteLine(switchText);
                        Thread.Sleep(1500);
                        EditItem(restaurantID, LoggedinUser);
                        break;
            
                    case "D":
                        switchText = languageInterface.GetString("SwitchDelete");
                        Console.WriteLine(switchText);
                        Thread.Sleep(1500);
                        DeleteItem(restaurantID, LoggedinUser);
                        break;

                    case "V":
                        switchText = languageInterface.GetString("SwitchView");
                        Console.WriteLine(switchText);
                        Thread.Sleep(1500);
                        ViewMenuList(restaurantID);
                        break;
                    case "Q":
                        switchText = languageInterface.GetString("SwitchExit");
                        Console.WriteLine(switchText);
                        Thread.Sleep(1500);
                        break;

                    default:
                        switchText = languageInterface.GetString("SwitchError");
                        Console.WriteLine(switchText);
                        Thread.Sleep(1500);
                        break;
                };

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Program error, {ex.Message}. Try again.");
            }
        }
    }

    public static void CreateItem(int restaurantID, Accounts LoggedinUser)
    {
        //{27-11}-{MICK}- Resource/EN/NL insertion
        string Header = languageInterface.GetString("FillMenuFields");
        string Name = languageInterface.GetString("FillNameItem");
        string Desc = languageInterface.GetString("FillDescItem");
        string Price = languageInterface.GetString("FillPriceItem");



        Console.WriteLine("====================================");
        Console.WriteLine(Header);
        Console.WriteLine("====================================");

        Console.WriteLine(Name);
        string Name = Console.ReadLine();

        Console.WriteLine(Desc);
        string Description = Console.ReadLine();
        
        Console.WriteLine(Price);
        double Price = Convert.ToDouble(Console.ReadLine());

        //{MICK}- Resource/EN/NL insertion
        string FTHeader = languageInterface.GetString("FoodtypesHeader");
        string EntFood = languageInterface.GetString("FillFoodType");
        string success = languageInterface.GetString("ItemCreateSuccess");
        string error = languageInterface.GetString("ItemCreateError");

        Console.WriteLine("              " + FTHeader + "            ");
        Console.WriteLine("====================================");
        foreach (string foodtype in MenuLogic.RetrieveFoodTypes(restaurantID)) {
            Console.WriteLine(foodtype);
        }

        Console.WriteLine(EntFood);
        string foodType = Console.ReadLine();

        if (MenuLogic.AddMenuItem(restaurantID, Name, Description, Price, foodType))
        {
            Console.WriteLine(success);
        }
        else
        {
            Console.WriteLine(error);
           
        }
    }

    public static void DeleteItem(int restaurantID, Accounts LoggedinUser)
    {
        //{MICK}- Resource/EN/NL insertion
        string Header = languageInterface.GetString("DeleteItemHeader");
        string DeleteInput = languageInterface.GetString("FillItemDelete");
        string Thread = languageInterface.GetString("SearchItemThread");
        string Confirm = languageInterface.GetString("ItemDelConfirm");

        string Success = languageInterface.GetString("ItemDelSuccess");
        string Error = languageInterface.GetString("ItemDelError");
        string ErrorCancel = languageInterface.GetString("ItemDelError2");
        string ErrorNotFound = languageInterface.GetString("ItemDelNotFound");



        Console.WriteLine("====================================");
        Console.WriteLine("           " + Header + "         ");
        Console.WriteLine("====================================");

        List<Menu> menuItems =  MenuLogic.RetrieveItems(restaurantID);
        foreach (Menu item in menuItems)
        {
            Console.WriteLine(item.ToStringDisplay());
        }

        Console.WriteLine(DeleteInput);
        string name = Console.ReadLine();

        Console.WriteLine(Thread);
        Thread.Sleep(1500);
        string description = "";
        foreach (Menu item in menuItems)
        {
            if (item.Name == name)
            {
                description = item.Description;
            }
        }

        if (description != "")
        {
            //{MICK}- Resource/EN/NL insertion
            string FormatString = languageInterface.GetString("ItemDeleteInfo");
            string FormattedString = string.Format(FormatString, name, description);

            Console.WriteLine(Confirm);
            Console.WriteLine(FormattedString);
            string Choice = Console.ReadLine().ToUpper();

            if (Choice == "Y")
            {
                if(MenuLogic.Deletemenuitem(restaurantID, name))
                {
                    Console.WriteLine(Success);
                }
                else
                {
                    Console.WriteLine(Error);               
                }
            }
            else
            {
                Console.WriteLine(ErrorCancel);
            }
        } else {
            Console.WriteLine(ErrorNotFound);
        }
    }

    
    public static void EditItem(int restaurantID, Accounts LoggedinUser)
    {
        //{MICK}- Resource/EN/NL insertion
        string Header = languageInterface.GetString("EditItemHeader");
        string Question = languageInterface.GetString("ItemFieldEditQ");
        string Opt1 = languageInterface.GetString("ItemFieldEditOpt1");
        string Opt2 = languageInterface.GetString("ItemFieldEditOpt2");
        string Opt3 = languageInterface.GetString("ItemFieldEditOpt3");
        string Opt4 = languageInterface.GetString("ItemFieldEditOpt4");
        string Error = languageInterface.GetString("InvalidPleaseAgain");



        Console.WriteLine("====================================");
        Console.WriteLine("           " + Header + "          ");
        Console.WriteLine("====================================");

        Console.WriteLine(Question);
        Console.WriteLine(Opt1);
        Console.WriteLine(Opt2);
        Console.WriteLine(Opt3);
        Console.WriteLine(Opt4);
        int Choice = Convert.ToInt32(Console.ReadLine());

        switch (Choice)
        {
            case 1:
                Edit(restaurantID, "name");
                break;

            case 2:
                Edit(restaurantID, "description");
                break;
            
            case 3:
                Edit(restaurantID, "price");
                break;

            case 4:
                Edit(restaurantID, "foodtype");
                break;
            default:
                Console.WriteLine(Error);
                break;       
        };        
    }

    public static void Edit(int restaurantID, string choice)
    {
        //{MICK}- Resource/EN/NL insertion
        string FormatString = languageInterface.GetString("EditItemHeader");
        string FormattedString = string.Format(FormatString, choice);
        string EnterInput = languageInterface.GetString("EnterNameItem");
        string FormatString2 = languageInterface.GetString("EnterNewChItem");
        string FormattedString2 = string.Format(FormatString2, choice);
        string FormatString3 = languageInterface.GetString("EditItemSuccess");
        string FormattedString3 = string.Format(FormatString3, name);
        string FormatString4 = languageInterface.GetString("EditItemError");
        string FormattedString4 = string.Format(FormatString4, name);

        Console.WriteLine("====================================");
        Console.WriteLine("    " + FormattedString + "    ");
        Console.WriteLine("====================================");

        List<Menu> menuItems =  MenuLogic.RetrieveItems(restaurantID);
        foreach (Menu item in menuItems)
        {
            Console.WriteLine(item.ToStringDisplay());
        }

        Console.WriteLine(EnterInput);
        string name = Console.ReadLine();
        Console.WriteLine(FormattedString2);
        string input = Console.ReadLine();

        
        if (MenuLogic.EditMenuItem(restaurantID, choice, name, input))
        {
            Console.WriteLine(FormattedString3);
        }
        else
        {
            Console.WriteLine(FormattedString4);
           
        }
    }

    public static void ViewMenuList(int restaurantID)
    {
        Dictionary<string, List<Menu>> menuItems = MenuLogic.RetrieveOrderedItems(restaurantID);
        
        string Header = languageInterface.GetString("MenuViewHeader");
        string NotFound = languageInterface.GetString("NoItemsFound");
        string Exit = languageInterface.GetString("ExitMenuPreview");

        Console.WriteLine("====================================");
        Console.WriteLine("         " + Header + "         ");
        Console.WriteLine("====================================");

        if (menuItems.Count == 0)
        {
            Console.WriteLine(NotFound);
        }
        else
        {
            bool firstCat = true;
            foreach(KeyValuePair<string, List<Menu>> entry in menuItems)
            {
                if (!firstCat)
                {
                    Console.WriteLine("\n");
                }
                else
                {
                    firstCat = false;
                }

                Console.WriteLine($"{entry.Key}");
                Console.WriteLine("====================================");
                foreach (Menu item in entry.Value)
                {
                    Console.WriteLine($"- {item.Name}\n {item.Description} \n{item.Price}");
                }
            }

        Console.WriteLine(Exit);
        Console.ReadLine();

        }
    }
}