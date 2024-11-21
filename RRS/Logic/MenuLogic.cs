public static class MenuLogic {
    public static bool AddMenuItem(int restaurantID, string Name,string Description, double Price,string Foodtype)
    {
        var newMenuItem = new Menu(restaurantID, Name, Description, Price, Database.SelectFoodType(restaurantID, Foodtype));

        return Database.Insert(newMenuItem);

    }

    public static bool Deletemenuitem(int restaurantID, string Name)
    {
        return Database.DeleteMenuItem(restaurantID, Name);
    }

    public static bool EditMenuItem(int restaurantID, string choice, string name, string input)
    {
        return Database.UpdateMenuItem(restaurantID, choice, name, input);
    }

    public static List<FoodType> RetrieveFoodType(int restaurantID)
    {
        return Database.SelectFoodType(restaurantID);
    }

    public static List<string> RetrieveFoodTypes(int restaurantID)
    {
        List<FoodType> foodTypes = Database.SelectFoodType(restaurantID);
        List<string> stringsFoodType = [];
        foreach (FoodType foodtype in foodTypes)
        {
            stringsFoodType.Add(foodtype.Name);
        }
        return stringsFoodType;
    }

    public static List<Menu> RetrieveItems(int restaurantID)
    {
        return Database.SelectMenu(restaurantID);
    }

    public static Dictionary<int, string> RetrieveOrderedFoodTypes(int restaurantID)
    {
        List<FoodType> foodTypes = RetrieveFoodType(restaurantID);
        Dictionary<int, string> OrderedFoodTypes = new Dictionary<int, string>();

        foreach (FoodType foodtype in foodTypes)
        {
            if (!OrderedFoodTypes.ContainsKey(foodtype.ID)) {
                OrderedFoodTypes.Add(foodtype.ID, foodtype.Name);
            }
        }

        return OrderedFoodTypes;
    }

    public static Dictionary<string, List<Menu>> RetrieveOrderedItems(int restaurantID) 
    {
        List<Menu> menuItems = RetrieveItems(restaurantID);
        Dictionary<int, string> FoodTypes = RetrieveOrderedFoodTypes(restaurantID);
        Dictionary<string, List<Menu>> OrderedMenu = new Dictionary<string, List<Menu>>();

        foreach (Menu item in menuItems)
        {
            string FoodTypeName = FoodTypes[item.Foodtype];
            if (!OrderedMenu.ContainsKey(FoodTypeName))
            {
                OrderedMenu.Add(FoodTypeName, [item]);
            }
            else
            {
                OrderedMenu[FoodTypeName].Add(item);
            }
        }

        return OrderedMenu;
    }
}