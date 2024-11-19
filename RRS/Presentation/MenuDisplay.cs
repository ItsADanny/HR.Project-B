using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;

namespace RRS.Logic;

public class MenuDisplay
{ 
    // public static void MenuItemEditorMenu(int restaurantID, Accounts LoggedinUser)
    // {
    //     while (true)
    //     {
    //         Console.WriteLine("====================================");
    //         Console.WriteLine("Menu Editor: Please choose an option");
    //         Console.WriteLine("====================================");
    //         Console.WriteLine("N - create new item");
    //         Console.WriteLine("E - edit existing item");
    //         //edit item, what item would you like to edit?
    //         //item sleected, what info box would you like to edit?
    //         //call method
    //         Console.WriteLine("D - delete existing item");
    //         Console.WriteLine("V - view full menu");
    //         string optionChoice = Console.ReadLine().ToUpper();

    //         try
    //         {
    //             switch (optionChoice)
    //             {
    //                 case "N":
    //                     Console.WriteLine("Creating new item...");
    //                     CreateItem(restaurantID, LoggedinUser);
    //                     break;

    //                 case "E":
    //                     Console.WriteLine("Editing existing item...");
    //                     DeleteItem();
    //                     break;
            
    //                 case "D":
    //                     Console.WriteLine("Deleting existing item...");
    //                     DeleteItem();
    //                     break;

    //                 case "V":
    //                     Console.WriteLine("Viewing Menu...");
    //                     ViewMenuList();
    //                     break;
    //                 case "Q":
    //                     Console.WriteLine("Exiting...");
    //                     break;

    //                 default:
    //                     Console.WriteLine("Invalid option, try again");
    //                     break;
                       
    //             };

    //         }
    //         catch (Exception ex)
    //         {
    //             Console.WriteLine($"Program error, {ex.Message}. Try again.");
    //         }
    //     }
    // }

    // public static void CreateItem(int restaurantID, Accounts LoggedinUser)
    // {
    //     Console.WriteLine("====================================");
    //     Console.WriteLine("    Fill in the Menu item fields    ");
    //     Console.WriteLine("====================================");

    //     Console.WriteLine("Enter Name: ");
    //     var Name = Console.ReadLine();

    //     Console.WriteLine("Enter Description: ");
    //     var Description = Console.ReadLine();
        
    //     Console.WriteLine("Enter Price: ");
    //     var Price = Convert.ToDouble(Console.ReadLine());

    //     Console.WriteLine("Enter foodtype: ");
    //     var Foodtype = Console.ReadLine();

    //     if (MenuLogic.AddItem(restaurantID, Name, Description, Price, FoodType))
    //     {
    //         Console.WriteLine("Menu Item has been added successfully");
    //     }
    //     else
    //     {
    //         Console.WriteLine("Menu Item could not be added, try again");
           
    //     }
    // }

    // public static void DeleteItem(string Name, string Description)
    // {
    //     Console.WriteLine("====================================");
    //     Console.WriteLine("           Deleting an item         ");
    //     Console.WriteLine("====================================");

    //     Console.WriteLine("\nEnter name of item to delete: ");
    //     var name = Console.ReadLine();

    //     Console.WriteLine("Searching Item....");

    //     Console.WriteLine("Is this the item you want to delete? Confirm Y/N");
    //     Console.WriteLine($"Name: {Name}, Description: {Description}");
    //     var Choice = Console.ReadLine().ToUpper();

    //     if (Choice == "Y")
    //     {
    //         if(menu.Deletemenuitem(Name))
    //         {
    //             Console.WriteLine("Item deleted Succesfully.");
    //         }
    //         else
    //         {
    //             Console.WriteLine("Item could not be deleted");               
    //         }
    //     }
    //     else
    //     {
    //         Console.WriteLine("Deletion Cancelled");
    //     }

    // }

    
    // public static void EditItem()
    // {
    //     Console.WriteLine("====================================");
    //     Console.WriteLine("           Editing an item          ");
    //     Console.WriteLine("====================================");

    //     Console.WriteLine("What field would you like to edit?\n");
    //     Console.WriteLine("N - Name");
    //     Console.WriteLine("D - Description");
    //     Console.WriteLine("P - Price");
    //     Console.WriteLine("F - FoodType");
    //     string Choice = Console.ReadLine().ToUpper();

    //     switch (Choice)
    //     {
    //         case "N":
    //             EditName();
    //             break;

    //         case "D":
    //             EditDescription();
    //             break;
            
    //         case "P":
    //             EditPrice();
    //             break;

    //         case "F":
    //             EditType();
    //             break;
    //         default:
    //             Console.WriteLine("Invalid option, try again");
    //             break;       
    //     };        
    // }

    // public static void EditName()
    // {
    //     Console.WriteLine("====================================");
    //     Console.WriteLine("       Editing an item: name        ");
    //     Console.WriteLine("====================================");

    //     Console.WriteLine("Enter the name of the item: ");
    //     var name = Console.ReadLine();
    //     Console.WriteLine("Enter the new name of the item: ");
    //     var NewName = Console.ReadLine();

    // }
    
    // public static void EditDescription()
    // {
    //     Console.WriteLine("====================================");
    //     Console.WriteLine("    Editing an item: description    ");
    //     Console.WriteLine("====================================");

    //     Console.WriteLine("Enter the name of the item: ");
    //     var name = Console.ReadLine();
    //     Console.WriteLine("Enter the new description of the item: ");
    //     var newDescription = Console.ReadLine();

    // }

    // public static void EditPrice()
    // {
    //     Console.WriteLine("====================================");
    //     Console.WriteLine("       Editing an item: price       ");
    //     Console.WriteLine("====================================");

    //     Console.WriteLine("Enter the name of the item: ");
    //     var name = Console.ReadLine();
    //     Console.WriteLine("Enter the new price of the item: ");
    //     var newPrice = Console.ReadLine();

    // }

    // public static void EditType()
    // {
    //     Console.WriteLine("====================================");
    //     Console.WriteLine("       Editing an item: Type        ");
    //     Console.WriteLine("====================================");

    //     Console.WriteLine("Enter the name of the item: ");
    //     var name = Console.ReadLine();
    //     Console.WriteLine("Enter the new type of the item: ");
    //     var newFoodtype = Console.ReadLine();

    // }

    // public static void ViewMenuList(string Name, string Description, double price, string Foodtype)
    // {
    //     var allitems = menu.RetrieveItems();

    //     Console.WriteLine("====================================");
    //     Console.WriteLine("            Menu Preview            ");
    //     Console.WriteLine("====================================");

    //     if (allitems.Count == 0)
    //     {
    //         Console.WriteLine("No items found.");
    //     }
    //     else
    //     {
    //         var FoodTypes = allitems.Select(item => item.Foodtype).Distinct();
    //         foreach (var type in Foodtype)
    //         {
    //             Console.WriteLine($"\n {type}\n");

    //             foreach (var item in allitems.Where(item => item.Foodtype == type))
    //             {
    //                 Console.WriteLine($"- {item.Name}\n {item.Description} \n{item.Price}");
    //             }
    //         }
    //     }


    // }
}