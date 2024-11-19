public static class MenuLogic {
//     public static bool AddMenuItem(int restaurantID, string Name,string Description, double Price,string Foodtype)
//     {
//         var newMenuItem = new Menu(restaurantID, Name, Description, Price, Foodtype);

//         return Database.Insert(newMenuItem);

//     }

//     public static bool Deletemenuitem(string Name)
//     {
//         var item = menuItems.FirstOrDefault(i => Name.Equals(Name,StringComparison.OrdinalIgnoreCase));

//         if(item == null)
//         {
//             Console.WriteLine("No item has been found");
//             return false;
//         }

//         menuItems.Remove(item);
//         Console.WriteLine("Menu item deleted successfully");
//         return true;

//     }

//     public static bool EditmenuitemName(string Name, string newName)
//     {
//         var item = menuItems.FirstOrDefault(i => Name.Equals(Name,StringComparison.OrdinalIgnoreCase));
//         if(item == null)
//         {
//             Console.WriteLine("No item has been found");
//             return false;
//         }

//         menuItems.Remove(item);
//         Console.WriteLine("Menu item name has been edited successfully to: \n" + newName);
//         return true;

//     }

//     public static bool EditmenuitemDescription(string Name, string newDescription)
//     {
//         var item = menuItems.FirstOrDefault(i => Name.Equals(Name,StringComparison.OrdinalIgnoreCase));
//         if(item == null)
//         {
//             Console.WriteLine("No item has been found");
//             return false;
//         }

//         item.Description = newDescription;
//         Console.WriteLine("Menu item description has been edited successfully to: \n" + newDescription);
//         return true;
//     }

//     public static bool EditmenuitemPrice(string Name, double newPrice)
//     {
//         var item = menuItems.FirstOrDefault(i => Name.Equals(Name,StringComparison.OrdinalIgnoreCase));
//         if(item == null)
//         {
//             Console.WriteLine("No item has been found");
//             return false;
//         }

//         item.Price = newPrice;
//         Console.WriteLine("Menu item price has been edited successfully to: \n" + newPrice);
//         return true;
//     }

//     public static bool EditmenuitemType(string Name, string newFoodtype)
//     {
//         var item = menuItems.FirstOrDefault(i => Name.Equals(Name,StringComparison.OrdinalIgnoreCase));
//         if(item == null)
//         {
//             Console.WriteLine("No item has been found");
//             return false;
//         }

//         item.Foodtype = newFoodtype;
//         Console.WriteLine("Menu item Type has been edited successfully to: \n" + newFoodtype);
//         return true;
//     }


// //method in logic to retrieve the menu items in the menuitems list above

//     public static List<Menu> RetrieveItems()
//     {
//         return menuItems; 
//     }
}