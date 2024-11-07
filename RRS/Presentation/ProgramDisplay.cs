public static class ProgramDisplay {
    private static int SelectedRestaurant = 0;
    private static Accounts LoggedInAccount = null;

    public static void Display() {

        int attempts = 4;
        while (true) {
            Console.Clear();
            Console.WriteLine("[TEMP] MATCHMAKING RESTAURANT [TEMP]");
            Console.WriteLine("====================================\n");
            Console.WriteLine("Login");
            Console.WriteLine("------------------------------------");
            Console.WriteLine("E-mail:");
            string input_email = Console.ReadLine();
            Console.WriteLine("Password:");
            string input_password = Functions.PasswordReadLine();

            Accounts account = Functions.Login(input_email, input_password);

            if (account is not null) {
                attempts = 4;
                Console.WriteLine(Functions.IsAccountAdmin(account));
                if (Functions.IsAccountAdmin(account)) {
                    LoggedInAccount = account;
                    Display_Admin_Environment();
                    LoggedInAccount = null;
                }
                //Currently Disabled - Application has not been published to the public yet, so there will be no customer accounts yet.
                // } else {
                //     LoggedInAccount = account;
                //     Display_Customer_Environment();
                //     LoggedInAccount = null;
                // }
            } else {
                attempts--;
                Console.WriteLine($"Incorrect login: you have {attempts} left.");
            }

            if (attempts == 0) {
                Console.WriteLine("All attempts have been used, Program shutting down.");
                break;
            }
        }

        //Shutsdown the program
        Environment.Exit(0);
    }

    public static void Display_Admin_Environment() {
        while (true) {
            Console.Clear();
            Console.WriteLine("====================================================================");
            Console.WriteLine(" ▗▄▖ ▗▄▄▄  ▗▖  ▗▖▗▄▄▄▖▗▖  ▗▖\n" +
                                "▐▌ ▐▌▐▌  █ ▐▛▚▞▜▌  █  ▐▛▚▖▐▌\n" + 
                                "▐▛▀▜▌▐▌  █ ▐▌  ▐▌  █  ▐▌ ▝▜▌\n" +
                                "▐▌ ▐▌▐▙▄▄▀ ▐▌  ▐▌▗▄█▄▖▐▌  ▐▌");
            Console.WriteLine("====================================================================\n");
            Console.WriteLine($"Welcome {LoggedInAccount.FirstName} {LoggedInAccount.LastName}\nWhat would you like to do?\n");

            Console.WriteLine("Reservation options");
            Console.WriteLine("1 - Create reservation");
            Console.WriteLine("2 - View reservations");
            Console.WriteLine("3 - Cancel reservation\n");
            Console.WriteLine("Customer account options");
            Console.WriteLine("4 - Create Customer account");
            Console.WriteLine("5 - Lookup Customer\n");
            Console.WriteLine("Account options");
            Console.WriteLine("6 - Create new account");
            Console.WriteLine("7 - Account lookup");
            Console.WriteLine("8 - Create account level");
            Console.WriteLine("9 - Account levels lookup");
            Console.WriteLine("\n\nQ - Logout");
            Console.WriteLine("====================================================================\n");
            Console.WriteLine("Choice:");
            string choice = Console.ReadLine();

            if (choice is not null) {
                switch (choice.ToLower())
                {
                    case "1":
                        ReservationDisplay.DisplayCreateReservation(SelectedRestaurant);
                        break;
                    case "2":
                        ReservationDisplay.DisplayForRestaurant(SelectedRestaurant);
                        break;
                    // case "3":
                    //     break;
                    case "q":
                            Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Please input valid choice");
                        Thread.Sleep(2000);
                        break;
                }
            } else {
                Console.WriteLine("Please input valid choice");
                Thread.Sleep(2000);
            }
        }
    }

}