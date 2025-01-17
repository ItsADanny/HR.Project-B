public class FoodType : IDBRestaurantClass {
    public int ID {get;}
    public int RestaurantID {get;}
    public string Name;

    public FoodType(int restaurantID, string name) {
        RestaurantID = restaurantID;
        Name = name;
    }

    public FoodType(int id, int restaurantID, string name) {
        ID = id;
        RestaurantID = restaurantID;
        Name = name;
    }

    public override string ToString()
    {
        return $"ID: {ID}\nRestaurantID: {RestaurantID}\nName: {Name}";
    }

    public string ToStringDisplay() => $"ID: {ID}\nRestaurantID: {RestaurantID}\nName: {Name}";
}