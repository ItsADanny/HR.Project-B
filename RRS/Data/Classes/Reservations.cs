public class Reservations : IDBRestaurantClass {
    public int ID {get;}
    public int RestaurantID {get;}
    public int TimeSlotID;
    public int TableID;
    public int AccountID;
    public int Status;

    public Reservations(int restaurantID, int timeSlotID, int tableID, int accountID, int status) {
        ID = 999999999; //This will be for just created reservations since they don't have an assigned ID
                        //from the database
        RestaurantID = restaurantID;
        TimeSlotID = timeSlotID;
        TableID = tableID;
        AccountID = accountID;
        Status = status;
    }

    public Reservations(int id, int restaurantID, int timeSlotID, int tableID, int accountID, int status) {
        ID = id;
        RestaurantID = restaurantID;
        TimeSlotID = timeSlotID;
        TableID = tableID;
        AccountID = accountID;
        Status = status;
    }
}