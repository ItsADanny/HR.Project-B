public class Review : IDBRestaurantClass {
    public int ID {get;}
    public int RestaurantID {get;}
    public readonly int AccountID;
    public readonly int ReservationID;
    public int Rating {get; private set;}
    public string Comment {get; private set;}

    public Review(int restaurantID, int accountID, int reservationID, int rating, string comment) {
        ID = 999999999;
        RestaurantID = restaurantID;
        AccountID = accountID;
        ReservationID = reservationID;
        Rating = rating;
        Comment = comment;
    }

    public Review(int id, int restaurantID, int accountID, int reservationID, int rating, string comment) {
        ID = id;
        RestaurantID = restaurantID;
        AccountID = accountID;
        ReservationID = reservationID;
        Rating = rating;
        Comment = comment;
    }

    public string ToStringDisplay() => $"ID: {ID}\nRestaurantID: {RestaurantID}\nAccountID: {AccountID}\nReservationID: {ReservationID}\nRating: {Rating}\nComment: {Comment}";
}