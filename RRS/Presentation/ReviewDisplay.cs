using System.Data.Entity.Migrations.Model;

public static class ReviewDisplay {
    public static void ReviewDisplayCustomer(int restaurantID, Accounts LoggedInAccount) {
        bool exit = false;
        string header = "====================================\nReviews: Please choose an option\n====================================\n";
        while (!exit) {
            switch (Functions.OptionSelector(header, ["View my review(s)", "View all review(s)", "Leave a review", "Delete review(s)", "Exit"]))
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
                case 3:
                    Delete(restaurantID, LoggedInAccount);
                    break; 
                case 4:
                    exit = true;
                    break;
            }
        }
    }

    public static void ReviewDisplayAdmin(int restaurantID, Accounts LoggedInAccount) {
        bool exit = false;
        string header = "====================================\nReviews: Please choose an option\n====================================\n";
        while (!exit) {
            switch (Functions.OptionSelector(header, ["View review(s)", "Delete review(s)", "Exit"]))
            {
                case 0:
                    ViewReviews(restaurantID, LoggedInAccount, true);
                    break;
                case 1:
                    Delete(restaurantID, LoggedInAccount);
                    break;
                case 2:
                    exit = true;
                    break;
            }
        }
    }

    private static void Add(int restaurantID, Accounts LoggedInAccount) {
        Console.Clear();
        string header = "====================================================================\nReviews\n====================================================================\n\n";
        int rating = Functions.IntSelector(header + "Enter the rating you want to give (1-5): ", 1, 5, 3, "⭐️");
        string stars = "";
        for (int i = 0; i != rating; i++) {
            stars += "⭐️";
        }
        Console.Clear();
        Console.WriteLine($"{header}Enter the rating you want to give (1-5):\n{stars}");
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

    private static void Delete(int restaurantID, Accounts LoggedInAccount) {
        bool isAdmin = Functions.IsAccountAdmin(LoggedInAccount);

        List<Review> reviews;
        if (isAdmin) {
            reviews = ReviewLogic.GetReviews(restaurantID);
        } else {
            reviews = ReviewLogic.GetReviews(restaurantID, LoggedInAccount);
        }

        string header = "====================================================================\nReviews\n====================================================================\n\n";
        if (reviews.Count() > 0) {
            header += "Please select the reviews you want to delete (or press ENTER without selecting reviews to cancel):\n\n";
            Dictionary<string, bool> slotsToDelete = Functions.CheckBoxSelector(header, ReviewLogic.ToDisplayString(reviews));
            
            int succes = 0;
            int failed = 0;

            foreach (KeyValuePair<string, bool> row in slotsToDelete) {
                if (row.Value) {
                    if (ReviewLogic.DeleteReview(ReviewLogic.ReviewFromDisplayString(row.Key, reviews).ID, LoggedInAccount)) {
                        succes++;
                    } else {
                        failed++;
                    }
                }
            }

            if (succes == 0 && failed == 0) {
                Console.WriteLine($"\nExiting");
            } else if (failed == 0) {
                Console.WriteLine($"\n{succes} review(s) succefully deleted");
            } else {
                Console.WriteLine($"\n{succes} review(s) succefully deleted, But failed to delete {failed} review(s)");
            }
        } else {
            Console.Clear();
            if (isAdmin) {
                header += "There are 0 reviews, It's only possible to delete reviews when there are any reviews\n\nExiting";
                Console.WriteLine(header);
            } else {
                header += "You have 0 reviews, you can only delete reviews if you have reviews.\n\nExiting";
                Console.WriteLine(header);
            }  
        }

        Thread.Sleep(1500);
    }

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