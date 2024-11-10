using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;

namespace RRS.Logic;

public class MenuDisplay
{ 

    public static void PrintAccountID(Accounts LoggedInAccount) {
        Console.WriteLine($"Account ID: {LoggedInAccount.ID}");
    }
    // private Menu MenuManager = new Menu();

//     public static string MenuItemEditorMenu()
//     {
//         while (true)
//         {
//             Console.WriteLine("====================================");
//             Console.WriteLine("Menu Editor: Please choose an option");
//             Console.WriteLine("====================================");
//             Console.WriteLine("N - create new item");
//             Console.WriteLine("E - edit existing item");
//             //edit item, what item would you like to edit?
//             //item sleected, what info box would you like to edit?
//             //call method
//             Console.WriteLine("D - delete existing item");
//             Console.WriteLine("V - view full menu");
//             string optionChoice = Console.ReadLine().ToUpper();

//             try
//             {
//                 switch (optionChoice)
//                 {
//                     case "N":
//                         Console.WriteLine("Creating new item...");
//                         CreateItem();
//                         break;

//                     case "E":
//                         Console.WriteLine("Editing existing item...");
//                         EditMenuItem();
//                         break;
            
//                     case "D":
//                         Console.WriteLine("Deleting existing item...");
//                         DeleteItem();
//                         break;

//                     case "V":
//                         Console.WriteLine("Viewing Menu...");
//                         ViewMenuList();
//                         break;
//                     case "Q":
//                         Console.WriteLine("Exiting...");
//                         break;

//                     default:
//                         Console.WriteLine("Invalid option, try again");
//                         break;
                       
//                 };

//             }

//         }
        







//     public string CreateItem(int ID, string Name, string Description, double price, string Foodtype)
//     {
//         Menu Item = new Menu();

//         Console.WriteLine("====================================");
//         Console.WriteLine("    Fill in the Menu item fields    ");
//         Console.WriteLine("====================================");

//         Console.WriteLine("Enter Name: ");
//         AddMenuItem.Name = Console.ReadLine();

//         Console.WriteLine("Enter Description: ");
//         AddMenuItem.Description = Console.ReadLine();
        
//         Console.WriteLine("Enter Price: ");
//         AddMenuItem.Price = double.Parse(Console.ReadLine());

//         Console.WriteLine("Enter foodtype: ");
//         AddMenuItem.Foodtype = Console.ReadLine();
        

//         //ask for a name put in var
//         //ask for a desc put in var
//         //ask for a price put in var
//         //ask for a type put in var
//     }
    
//     public string DeleteItem(string Name, string description)
//     {
//         Console.WriteLine("====================================");
//         Console.WriteLine("           Deleting an item         ");
//         Console.WriteLine("====================================");

//         Console.WriteLine("Enter name of item to delete: ");
//         Name = Console.ReadLine();

//         Console.WriteLine("Searching Item....");

//         Console.WriteLine("Is this the item you want to delete? Confirm Y/N");
//         Console.WriteLine($"Name: {Name}, Description: {description}");
//         string Choice = Console.ReadLine().ToUpper();

//         if (Choice == "Y")
//         {
//             DeleteMenuItem();
//         }
//         else
//         {
//             Console.WriteLine("Deletion Cancelled");
//         }

//         // ask an id to retrieve an item
//         //ask if theyre really sure they want to delete
//         //add method deletion
//     }

    
//     public string EditItem(int ID, string Name, string Description, double price, string Foodtype)
//     {
//         Console.WriteLine("====================================");
//         Console.WriteLine("           Editing an item          ");
//         Console.WriteLine("====================================");

//         Console.WriteLine("What field would you like to edit?\n");
//         Console.WriteLine("N - Name");
//         Console.WriteLine("D - Description");
//         Console.WriteLine("P - Price");
//         Console.WriteLine("F - FoodType");
//         string Choice = Console.ReadLine().ToUpper();

//         switch (Choice)
//         {
//             case "N":
//                 Console.WriteLine("Editing Name...");
//                 EditMenuItemName();
//                 break;

//             case "D":
//                 Console.WriteLine("Editing Description...");
//                 EditMenuItemDescription();
//                 break;
            
//             case "P":
//                 Console.WriteLine("Editing Price...");
//                 EditMenuItemPrice();
//                 break;

//             case "F":
//                 Console.WriteLine("Editing Foodtype...");
//                 EditMenuItemType();
//                 break;
//         case default:
                
                
                       
//         };        







//         //in presentation layer ask the info which info needs to be edited
//         //add a switch case for what info will be edited 
//         //for each case add the method needed

//     }

//     public string ViewMenuList(int ID, string Name, string Description, double price, string Foodtype)
//     {
//         Console.WriteLine("====================================");
//         Console.WriteLine("            Menu Preview            ");
//         Console.WriteLine("====================================");

//         //Print Menu by foodtype  


//     }



// }


// //as an admin I want my menu items to be stored so I can remove and delete any menu items as wished
//  //have a menu class
//  //add functions to remove and add items
//  //add function to view the list

//  //addmenuitem method
//  //deletemenuitem method
//  //editmenuiteminfo
//  //editmenuitemname
//  //viewmenulist
// }

// public class MenuDisplay
// { 
//     private Menu MenuManager = new Menu();

//     public static string MenuItemEditorMenu()
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

//         switch (optionChoice)
//         {
//             case "N":
//                 Console.WriteLine("Creating new item...");
//                 CreateItem();
//                 break;

//             case "E":
//                 Console.WriteLine("Editing existing item...");
//                 EditMenuItem();
//                 break;
            
//             case "D":
//                 Console.WriteLine("Deleting existing item...");
//                 DeleteItem();
//                 break;

//             case "V":
//                 Console.WriteLine("Viewing Menu...");
//                 ViewMenuList();
//                 break;
                       
//         };


//     public string CreateItem(int ID, string Name, string Description, double price, string Foodtype)
//     {
//         Menu Item = new Menu();

//         Console.WriteLine("====================================");
//         Console.WriteLine("    Fill in the Menu item fields    ");
//         Console.WriteLine("====================================");

//         Console.WriteLine("Enter Name: ");
//         AddMenuItem.Name = Console.ReadLine();

//         Console.WriteLine("Enter Description: ");
//         AddMenuItem.Description = Console.ReadLine();
        
//         Console.WriteLine("Enter Price: ");
//         AddMenuItem.Price = double.Parse(Console.ReadLine());

//         Console.WriteLine("Enter foodtype: ");
//         AddMenuItem.Foodtype = Console.ReadLine();
        

//         //ask for a name put in var
//         //ask for a desc put in var
//         //ask for a price put in var
//         //ask for a type put in var
//     }
    
//     public string DeleteItem(string Name, string description)
//     {
//         Console.WriteLine("====================================");
//         Console.WriteLine("           Deleting an item         ");
//         Console.WriteLine("====================================");

//         Console.WriteLine("Enter name of item to delete: ");
//         Name = Console.ReadLine();

//         Console.WriteLine("Searching Item....");

//         Console.WriteLine("Is this the item you want to delete? Confirm Y/N");
//         Console.WriteLine($"Name: {Name}, Description: {description}");
//         string Choice = Console.ReadLine().ToUpper();

//         if (Choice == "Y")
//         {
//             DeleteMenuItem();
//         }
//         else
//         {
//             Console.WriteLine("Deletion Cancelled");
//         }

//         // ask an id to retrieve an item
//         //ask if theyre really sure they want to delete
//         //add method deletion
//     }

    
//     public string EditItem(int ID, string Name, string Description, double price, string Foodtype)
//     {
//         Console.WriteLine("====================================");
//         Console.WriteLine("           Editing an item          ");
//         Console.WriteLine("====================================");

//         Console.WriteLine("What field would you like to edit?\n");
//         Console.WriteLine("N - Name");
//         Console.WriteLine("D - Description");
//         Console.WriteLine("P - Price");
//         Console.WriteLine("F - FoodType");
//         string Choice = Console.ReadLine().ToUpper();

//         switch (Choice)
//         {
//             case "N":
//                 Console.WriteLine("Editing Name...");
//                 EditMenuItemName();
//                 break;

//             case "D":
//                 Console.WriteLine("Editing Description...");
//                 EditMenuItemDescription();
//                 break;
            
//             case "P":
//                 Console.WriteLine("Editing Price...");
//                 EditMenuItemPrice();
//                 break;

//             case "F":
//                 Console.WriteLine("Editing Foodtype...");
//                 EditMenuItemType();
//                 break;
//         case default:
                
                
                       
//         };        







//         //in presentation layer ask the info which info needs to be edited
//         //add a switch case for what info will be edited 
//         //for each case add the method needed

//     }

//     public string ViewMenuList(int ID, string Name, string Description, double price, string Foodtype)
//     {
//         Console.WriteLine("====================================");
//         Console.WriteLine("            Menu Preview            ");
//         Console.WriteLine("====================================");

//         //Print Menu by foodtype  


//     }

}


//as an admin I want my menu items to be stored so I can remove and delete any menu items as wished
 //have a menu class
 //add functions to remove and add items
 //add function to view the list

 //addmenuitem method
 //deletemenuitem method
 //editmenuiteminfo
 //editmenuitemname
 //viewmenulist

/*
reservations dashboard (presentation layer)

- [ ]  reservation name (timeslot, people amount, language)
- [ ] look through the list and input if you wanna join them
- [ ] if not add a table for others to join
*/

