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

<<<<<<< Updated upstream
            if (attempts == 0) {
                Console.WriteLine("All attempts have been used, Program shutting down.");
                break;
=======
    private static void DisplayCreateNewCustomerAccount() 
    {
        Console.Clear(); //empty screen
        Console.WriteLine("       MATCHMAKING RESTAURANT       ");
        Console.WriteLine("====================================\n");
        Console.WriteLine("Create an account:");

        //request needed information from user to create new account
        string FirstName = Functions.RequestValidString("First name");
        string LastName = Functions.RequestValidString("Last name");
        string Email = Functions.RequestValidEmail();
        string PhoneNumber = Functions.RequestValidPhonenumber("Phonenumber");
        Console.WriteLine("Password:");
        string Password = Functions.PasswordReadLine_WithValidCheck();
        
        //account success or error message
        if (AccountLogic.CreateNewCustomerAccount(Email, Password, FirstName, LastName, PhoneNumber)) 
        {
            Console.WriteLine("New account created!\n You will be sent back to the start screen");
        } 
        else 
        {
            Console.WriteLine("There was an error while trying to create your account\nplease try again later\nyou will be sent back to the start screen");
        }

        //1.5 sec wait
        Thread.Sleep(1500);
    }

    private static void Display_Admin_Environment() {
        bool Logout = false;
        string Header = $"====================================================================\n ▗▄▖ ▗▄▄▄  ▗▖  ▗▖▗▄▄▄▖▗▖  ▗▖\n▐▌ ▐▌▐▌  █ ▐▛▚▞▜▌  █  ▐▛▚▖▐▌\n▐▛▀▜▌▐▌  █ ▐▌  ▐▌  █  ▐▌ ▝▜▌\n▐▌ ▐▌▐▙▄▄▀ ▐▌  ▐▌▗▄█▄▖▐▌  ▐▌\n====================================================================\n\nWelcome {LoggedInAccount.FirstName} {LoggedInAccount.LastName}\nWhat would you like to do?\n\n";
        while (!Logout) {
            List<string> menuOptions = ["Reservation menu", "Timeslot menu", "Dining menu", "Account menu"];
            if (AccountLogic.CanDisplay("logs", LoggedInAccount)) {
                menuOptions.Add("View database application logs");
            }
            menuOptions.Add("Logout");

            switch (Functions.OptionSelector(Header, menuOptions)) {
                case "Reservation menu":
                    ReservationDisplay.DisplayForRestaurant(SelectedRestaurant);
                    break;
                case "Timeslot menu":
                    TimeSlotDisplay.Menu(SelectedRestaurant, LoggedInAccount);
                    break;
                case "Dining menu":
                    MenuDisplay.MenuItemEditorMenu(SelectedRestaurant, LoggedInAccount);
                    break;
                case "Account menu":
                    AccountDisplay.AdminAccountMenu(LoggedInAccount);
                    break;
                case "View database application logs":
                    //TODO: IMPLEMENT AN ENVIRONMENT TO DISPLAY THESE LOGS
                    // LogsDisplay.Menu();
                    break;
                case "Logout":
                    Logout = true;
                    break;
>>>>>>> Stashed changes
            }
        }

        //Shutsdown the program
        Environment.Exit(0);
    }

<<<<<<< Updated upstream
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
=======
    private static void DisplayCustomerEnvironment() {
        bool Logout = false;
        string Header = $"====================================================================\n▗▄▄▖ ▗▄▄▄▖ ▗▄▄▖▗▄▄▄▖▗▄▖ ▗▖ ▗▖▗▄▄▖  ▗▄▖ ▗▖  ▗▖▗▄▄▄▖\n▐▌ ▐▌▐▌   ▐▌     █ ▐▌ ▐▌▐▌ ▐▌▐▌ ▐▌▐▌ ▐▌▐▛▚▖▐▌  █  \n▐▛▀▚▖▐▛▀▀▘ ▝▀▚▖  █ ▐▛▀▜▌▐▌ ▐▌▐▛▀▚▖▐▛▀▜▌▐▌ ▝▜▌  █  \n▐▌ ▐▌▐▙▄▄▖▗▄▄▞▘  █ ▐▌ ▐▌▝▚▄▞▘▐▌ ▐▌▐▌ ▐▌▐▌  ▐▌  █  \n====================================================================\nWelcome {LoggedInAccount.FirstName} {LoggedInAccount.LastName}\nWhat would you like to do?\n====================================================================\n\n";
        while (!Logout) {
            switch (Functions.OptionSelector(Header, ["See current reservations", "Make a reservation", "Account options", "Logout"])) {
                case "See current reservations":
                    ReservationDisplay.DisplayForRestaurantCustomer(SelectedRestaurant, LoggedInAccount);
                    break;
                case "Make a reservation":
                    ReservationDisplay.DisplayCreateReservation_Customer(SelectedRestaurant, LoggedInAccount);
                    break;
                case "Account options":
                    AccountDisplay.AccountMenu(LoggedInAccount);
                    break;
                case "Logout":
                    Logout = true;
                    break;
>>>>>>> Stashed changes
            }
        }
    }
}