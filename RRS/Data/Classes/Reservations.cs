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

    public Reservations(string id, string restaurantID, string timeSlotID, string tableID, string accountID, string status) {
        try {
            ID = Convert.ToInt32(id);
            RestaurantID = Convert.ToInt32(restaurantID);
            TimeSlotID = Convert.ToInt32(timeSlotID);
            TableID = Convert.ToInt32(tableID);
            AccountID = Convert.ToInt32(accountID);
            Status = Convert.ToInt32(status);
        } catch {
            throw new Exception("Invalid input, Can't create reservation from invalid input, please only input numerics that can be converted to integers");
        }
    }
}