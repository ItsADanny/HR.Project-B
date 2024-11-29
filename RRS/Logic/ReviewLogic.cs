public static class ReviewLogic
{
    public static List<string> ViewReviews(int restaurantID, Accounts LoggedInAccount){
        List<string> reviews = new ();
        foreach (Review review in Database.Reviews(restaurantID, LoggedInAccount)) {   
            Accounts ReviewerAccount = AccountLogic.GetSelectedAccount(review.AccountID);
            if (review.Comment != "") {
                reviews.Add($"review from: {ReviewerAccount.FirstName} {ReviewerAccount.LastName}\n             stars: {review.Rating}\n             comment: {review.Comment}");
            } else {
                reviews.Add($"review from: {ReviewerAccount.FirstName} {ReviewerAccount.LastName}\n             stars: {review.Rating}");
            }
        }
        return reviews;
    }

    public static List<string> ViewReviews(int restaurantID){
        List<string> reviews = new ();
        foreach (Review review in Database.Reviews(restaurantID)) {   
            Accounts ReviewerAccount = AccountLogic.GetSelectedAccount(review.AccountID);
            if (review.Comment != "") {
                reviews.Add($"review from: {ReviewerAccount.FirstName} {ReviewerAccount.LastName}\n             stars: {review.Rating}\n             comment: {review.Comment}");
            } else {
                reviews.Add($"review from: {ReviewerAccount.FirstName} {ReviewerAccount.LastName}\n             stars: {review.Rating}");
            }
        }
        return reviews;
    }

    public static List<Review> GetReviews(int restaurantID, Accounts LoggedInAccount) => Database.Reviews(restaurantID, LoggedInAccount);

    public static List<Review> GetReviews(int restaurantID) => Database.Reviews(restaurantID);

    public static bool LeaveReview(int restaurantID, int accountID, int reservationID, int rating, string comment){
        return Database.Insert(new Review(restaurantID, accountID, reservationID, rating, comment));
    }
    
    public static bool DeleteReview(string ReviewID, Accounts LoggedInAccount) {
        return Database.DeleteReview(ReviewID);
    }

    public static bool DeleteReview(int ReviewID, Accounts LoggedInAccount) {
        return Database.DeleteReview(ReviewID);
    }
}