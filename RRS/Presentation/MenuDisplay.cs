using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;

namespace RRS.Logic;

public class MenuDisplay
{ 
    public static void MenuItemEditorMenu(int restaurantID, Accounts LoggedinUser)
    {
        bool exit = false;
        string header = "====================================\nMenu Editor: Please choose an option\n====================================\n";
        while (!exit) {
            switch (Functions.OptionSelector(header, ["create new item", "edit existing item", "delete existing item", "view full menu", "Exit"]))
            {
                case 0:
                    Console.WriteLine("Creating new item...");
                    Thread.Sleep(1000);
                    CreateItem(restaurantID, LoggedinUser);
                    break;
                case 1:
                    Console.WriteLine("Editing existing item...");
                    Thread.Sleep(1000);
                    EditItem(restaurantID, LoggedinUser);
                    break;
                case 2:
                    Console.WriteLine("Deleting existing item...");
                    Thread.Sleep(1000);
                    DeleteItem(restaurantID, LoggedinUser);
                    break;
                case 3:
                    Console.WriteLine("Viewing Menu...");
                    Thread.Sleep(1000);
                    ViewMenuList(restaurantID);
                    break;
                case 4:
                    Console.WriteLine("Exiting...");
                    Thread.Sleep(1000);
                    exit = true;
                    break;
            }
        }
    }

    public static void CreateItem(int restaurantID, Accounts LoggedinUser)
    {
        string header = "====================================\n    Fill in the Menu item fields    \n====================================\n";
        Console.Clear();
        Console.WriteLine(header);

        Console.WriteLine("Enter Name: ");
        string Name = Console.ReadLine();

        Console.WriteLine("Enter Description: ");
        string Description = Console.ReadLine();
        
        Console.WriteLine("Enter Price: ");
        double Price = Convert.ToDouble(Console.ReadLine());

        string foodType = Functions.OptionSelector(header + "\n              Foodtypes:            \n====================================\n", MenuLogic.RetrieveFoodTypes(restaurantID), true);

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
        string header = "====================================\n           Deleting an item         \n====================================\n";
        Dictionary<string, bool> slotsToDelete = Functions.CheckBoxSelector(header, MenuLogic.ToDisplayString(MenuLogic.RetrieveItems(restaurantID)));
        
        foreach (KeyValuePair<string, bool> row in slotsToDelete) {
            if (row.Value) {
                int menuItemID = MenuLogic.GetIDFromDisplayString(row.Key, restaurantID);
                if (MenuLogic.Deletemenuitem(restaurantID, menuItemID, LoggedinUser)) {
                    Console.WriteLine($"Timeslot \"{row.Key}\" deleted, associated reservations have been cancelled");
                } else {
                    Console.WriteLine($"There was an error while trying to delete timeslot \"{row.Key}\", please try again later");
                }
            }
        }
    }

    
    public static void EditItem(int restaurantID, Accounts LoggedinUser)
    {
        bool exit = false;
        string header = "====================================\n           Editing an item          \n====================================\n\nWhat field would you like to edit?\n";
        while (!exit) {
            switch (Functions.OptionSelector(header, ["Name", "Description", "Price", "FoodType", "Exit"]))
            {
                case 0:
                    Edit(restaurantID, "name");
                    exit = true;
                    break;
                case 1:
                    Edit(restaurantID, "description");
                    exit = true;
                    break;
                case 2:
                    Edit(restaurantID, "price");
                    exit = true;
                    break;
                case 3:
                    Edit(restaurantID, "foodtype");
                    exit = true;
                    break;
                case 4:
                    Console.WriteLine("Exiting...");
                    Thread.Sleep(1000);
                    exit = true;
                    break;
            }
        }
    }

    public static void Edit(int restaurantID, string choice)
    {
        Console.Clear();
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

        Console.Clear();
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

            // DANNY NOTE: Waarom wordt dit op deze manier gedaan?, ik snap niet helemaal waarom je het op deze manier zou doen?
            // var FoodTypes = allitems.Select(item => item.Foodtype).Distinct();
            // foreach (var type in Foodtype)
            // {
            //     Console.WriteLine($"\n {type}\n");

            //     foreach (var item in allitems.Where(item => item.Foodtype == type))
            //     {
            //         Console.WriteLine($"- {item.Name}\n {item.Description} \n{item.Price}");
            //     }
            // }
        }
    }
}