using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;

namespace RRS.Logic;
public class MenuDisplay
{ 
    private ILanguageInterface _languageInterface;

    public void LanguageSet(ILanguageInterface languageInterface)
    {
        _languageInterface = languageInterface;
    }

    public static void MenuItemEditorMenu(int restaurantID, Accounts LoggedinUser)
    {
        while (true)
        {
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
        Console.WriteLine("====================================");
        Console.WriteLine("    Fill in the Menu item fields    ");
        Console.WriteLine("====================================");

        Console.WriteLine("Enter Name: ");
        string Name = Console.ReadLine();

        Console.WriteLine("Enter Description: ");
        string Description = Console.ReadLine();
        
        Console.WriteLine("Enter Price: ");
        double Price = Convert.ToDouble(Console.ReadLine());

        Console.WriteLine("              Foodtypes:            ");
        Console.WriteLine("====================================");
        foreach (string foodtype in MenuLogic.RetrieveFoodTypes(restaurantID)) {
            Console.WriteLine(foodtype);
        }

        Console.WriteLine("Enter foodtype: ");
        string foodType = Console.ReadLine();

        if (MenuLogic.AddMenuItem(restaurantID, Name, Description, Price, foodType))
        {
            Console.WriteLine("Menu Item has been added successfully");
        }
        else
        {
            Console.WriteLine("Menu Item could not be added, try again");
           
        }
    }

    public static void DeleteItem(int restaurantID, Accounts LoggedinUser)
    {
        Console.WriteLine("====================================");
        Console.WriteLine("           Deleting an item         ");
        Console.WriteLine("====================================");

        List<Menu> menuItems =  MenuLogic.RetrieveItems(restaurantID);
        foreach (Menu item in menuItems)
        {
            Console.WriteLine(item.ToStringDisplay());
        }

        Console.WriteLine("\nEnter name of item to delete: ");
        string name = Console.ReadLine();

        Console.WriteLine("Searching Item....");
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
            Console.WriteLine("Is this the item you want to delete? Confirm Y/N");
            Console.WriteLine($"Name: {name}, Description: {description}");
            string Choice = Console.ReadLine().ToUpper();

            if (Choice == "Y")
            {
                if(MenuLogic.Deletemenuitem(restaurantID, name))
                {
                    Console.WriteLine("Item deleted Successfully.");
                }
                else
                {
                    Console.WriteLine("Item could not be deleted");               
                }
            }
            else
            {
                Console.WriteLine("Deletion Cancelled");
            }
        } else {
            Console.WriteLine("No item has been found");
        }
    }

    
    public static void EditItem(int restaurantID, Accounts LoggedinUser)
    {
        Console.WriteLine("====================================");
        Console.WriteLine("           Editing an item          ");
        Console.WriteLine("====================================");

        Console.WriteLine("What field would you like to edit?\n");
        Console.WriteLine("1 - Name");
        Console.WriteLine("2 - Description");
        Console.WriteLine("3 - Price");
        Console.WriteLine("4 - FoodType");
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
                Console.WriteLine("Invalid option, try again");
                break;       
        };        
    }

    public static void Edit(int restaurantID, string choice)
    {

        Console.WriteLine("====================================");
        Console.WriteLine($"       Editing an item: {choice}");
        Console.WriteLine("====================================");

        List<Menu> menuItems =  MenuLogic.RetrieveItems(restaurantID);
        foreach (Menu item in menuItems)
        {
            Console.WriteLine(item.ToStringDisplay());
        }

        Console.WriteLine("Enter the name of the item: ");
        string name = Console.ReadLine();
        Console.WriteLine($"Enter the new {choice} of the item: ");
        string input = Console.ReadLine();

        //TODO: Implement the EditMenuItem function in the Logic layer so that it updates the correct fields in the database
        if (MenuLogic.EditMenuItem(restaurantID, choice, name, input))
        {
            Console.WriteLine($"Menu Item \"{name}\" has been added successfully");
        }
        else
        {
            Console.WriteLine($"Menu Item \"{name}\" could not be changed, try again");
           
        }
    }

    public static void ViewMenuList(int restaurantID)
    {
        Dictionary<string, List<Menu>> menuItems = MenuLogic.RetrieveOrderedItems(restaurantID);

        Console.WriteLine("====================================");
        Console.WriteLine("            Menu Preview            ");
        Console.WriteLine("====================================");

        if (menuItems.Count == 0)
        {
            Console.WriteLine("No items found.");
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

        Console.WriteLine("\nPress enter to exit the menu preview");
        Console.ReadLine();

        }
    }
}