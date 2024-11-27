public class Review {
    public readonly int ID;
    public readonly int RestaurantID;
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
}