public class Table : IDBRestaurantClass {
    public int ID {get;}
    public int RestaurantID {get;}
    public string Name {get; private set;}
    public readonly int MaxSize;

    public Table (int id, int restaurantID, string name, int maxSize) {
        ID = id;
        RestaurantID = restaurantID;
        Name = name;
        MaxSize = maxSize;
    }

    public string ToStringDisplay() => $"ID: {ID}\nRestaurantID: {RestaurantID}\nName: {Name}\nMaxSize: {MaxSize}";
}