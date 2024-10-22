public class FoodType {
    public int ID;
    public int RestaurantID;
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
}