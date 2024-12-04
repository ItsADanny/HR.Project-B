using System.Data.Entity.Migrations.Model;

public static class ReviewDisplay {
    public static void ReviewDisplayCustomer(int restaurantID, Accounts LoggedInAccount) {
        bool exit = false;
        string header = "====================================\nReviews: Please choose an option\n====================================\n";
        while (!exit) {
            switch (Functions.OptionSelector(header, ["View my reviews", "View all reviews", "Leave a review", "Exit"]))
            {
                case 0:
                    ViewReviews(restaurantID, LoggedInAccount, false);
                    break;
                case 1:
                    ViewReviews(restaurantID, LoggedInAccount, true);
                    break;
                case 2:
                    Add(restaurantID, LoggedInAccount);
                    break;
                // case 3:// Temp off
                    // Delete(restaurantID, LoggedInAccount);
                    // break; 
                case 3:
                    exit = true;
                    break;
            }
        }
    }

    public static void ReviewDisplayAdmin(int restaurantID, Accounts LoggedInAccount) {
        bool exit = false;
        string header = "====================================\nReviews: Please choose an option\n====================================\n";
        while (!exit) {
            switch (Functions.OptionSelector(header, ["View reviews", "Exit"]))
            {
                case 0:
                    ViewReviews(restaurantID, LoggedInAccount, true);
                    break;
                // case 2:
                    // Delete(restaurantID, LoggedInAccount);
                    // break;
                case 1:
                    exit = true;
                    break;
            }
        }
    }

    private static void Add(int restaurantID, Accounts LoggedInAccount) {
        Console.Clear();
        Console.WriteLine("====================================================================");
        Console.WriteLine("Reviews");
        Console.WriteLine("====================================================================\n\n");
        Console.WriteLine("Enter the rating you want to give (1-5): ");
        int rating = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine("Please add a comment about you experience (This is not required)");
        string comment = Console.ReadLine();

        if (ReviewLogic.LeaveReview(restaurantID, LoggedInAccount.ID, 1, rating, comment)) {
            Console.WriteLine("Your review has been added!, returning to review menu");
            Thread.Sleep(1500);
        } else {
            Console.WriteLine("There was an error while trying to add your review, please try later, returning to review menu");
            Thread.Sleep(1500);
        }
    }

    // private static void Delete(int restaurantID, Accounts LoggedInAccount) {
    //     List<Review> reviews;
    //     if (Functions.IsAccountAdmin(LoggedInAccount)) {
    //         reviews = ReviewLogic.GetReviews(restaurantID);
    //     } else {
    //         reviews = ReviewLogic.GetReviews(restaurantID, LoggedInAccount);
    //     }

    //     string header = "";
    //     Dictionary<string, bool> slotsToDelete = Functions.CheckBoxSelector(header, ReviewLogic.ToDisplayString(reviews));
        
    //     foreach (KeyValuePair<string, bool> row in slotsToDelete) {
    //         if (row.Value) {
                
    //         }
    //     }
    // }

    private static void ViewReviews (int restaurantID, Accounts LoggedInAccount, bool viewAll) {
        Console.Clear();
        Console.WriteLine("====================================================================");
        Console.WriteLine("Reviews");
        Console.WriteLine("====================================================================\n\n");
        List<string> reviews;
        if (viewAll) {
            reviews = ReviewLogic.ViewReviews(restaurantID);
        } else {
            reviews = ReviewLogic.ViewReviews(restaurantID, LoggedInAccount);
        }
        
        foreach (string review in reviews) {
            Console.WriteLine(review + "\n");
        }
        Console.WriteLine("====================================================================\n\n");
        Console.WriteLine("Press ENTER to exit");
        Console.ReadLine();
    }
}