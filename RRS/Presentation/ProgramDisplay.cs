using RRS.Logic;

public static class ProgramDisplay
{
    private static int SelectedRestaurant = 0;
    private static Accounts LoggedInAccount = null;

    public static void Display() 
    {
        while (true) 
        {
            Console.Clear();
            Console.WriteLine("       MATCHMAKING RESTAURANT       ");
            Console.WriteLine("====================================\n");
            Console.WriteLine("\nWelcome to the Matchmaking restaurant");
            Console.WriteLine("1 - Login");
            Console.WriteLine("2 - Create a new account\n\n");
            Console.WriteLine("====================================");
            Console.WriteLine("Please select an option of what you\nwant to do (enter Q to exit):");
            int selectedOption = 0;
            while (true) 
            {
                string userInput = Console.ReadLine();
                switch (userInput.ToLower()) 
                {
                    case "1":
                        selectedOption = 1;
                        break;
                    case "2":
                        selectedOption = 2;
                        break;
                    case "q":
                        selectedOption = 3;
                        break;
                    default:
                        Console.WriteLine("Invalid choice, please select a valid option");
                        Thread.Sleep(1500);
                        break;
                }

                if (selectedOption != 0) 
                {
                    break;
                }
            }

            if (selectedOption == 1) 
            {
                //Displays the login page
                DisplayLogin();
            }

            if (selectedOption == 2) 
            {
                DisplayCreateNewCustomerAccount();
            }

            if (selectedOption == 3) 
            {
                //Exits the program
                Environment.Exit(0);
            }
        }
    }



    private static void DisplayLogin() 
    {
        int attempts = 0;
        while (true) 
        {
            Console.Clear();
            Console.WriteLine("       MATCHMAKING RESTAURANT       ");
            Console.WriteLine("====================================\n");
            Console.WriteLine("Login");
            Console.WriteLine("------------------------------------");
            Console.WriteLine("E-mail:");
            string input_email = Console.ReadLine();
            Console.WriteLine("Password:");
            string input_password = Functions.PasswordReadLine();

            Accounts account = Functions.Login(input_email, input_password);

            if (account is not null) 
            {
                attempts = 4;
                Console.WriteLine(Functions.IsAccountAdmin(account));
                if (Functions.IsAccountAdmin(account)) {
                    LoggedInAccount = account;
                    Display_Admin_Environment();
                    LoggedInAccount = null;
                } else {
                    LoggedInAccount = account;
                    DisplayCustomerEnvironment();
                    LoggedInAccount = null;
                }
            }
            else 
            {
                attempts--;
                Console.WriteLine($"Incorrect login: you have {attempts} left.");
                Thread.Sleep(1500);
            }

            if (attempts == 0) 
            {
                Console.WriteLine("All attempts have been used, Program shutting down.");
                break;
            }
        }
    }

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
        

        //danny NOTE: THIS WILL ONLY BE REQUIRED FOR WHEN A ADMIN NEEDS TO CREATE AN ACCOUNT, AN CUSTOMER ACCOUNT IS ALWAYS 3
        // int Accountlevel = AccountLevelDisplay.ChooseAccountLevel();


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
        while (true) {
            Console.Clear();
            Console.WriteLine("====================================================================");
            Console.WriteLine(" ▗▄▖ ▗▄▄▄  ▗▖  ▗▖▗▄▄▄▖▗▖  ▗▖\n" +
                                "▐▌ ▐▌▐▌  █ ▐▛▚▞▜▌  █  ▐▛▚▖▐▌\n" + 
                                "▐▛▀▜▌▐▌  █ ▐▌  ▐▌  █  ▐▌ ▝▜▌\n" +
                                "▐▌ ▐▌▐▙▄▄▀ ▐▌  ▐▌▗▄█▄▖▐▌  ▐▌");
            Console.WriteLine("====================================================================\n");
            Console.WriteLine($"Welcome {LoggedInAccount.FirstName} {LoggedInAccount.LastName}\nWhat would you like to do?\n");

            // Console.WriteLine("Quick actions");
            // Console.WriteLine("1 - View reservations");
            // Console.WriteLine("2 - Customer Lookup");
            // Console.WriteLine("3 - View dining menu");
            // Console.WriteLine("Menu's:");
            // Console.WriteLine("4 - Reservation options");
            // Console.WriteLine("5 - Dining menu options");
            // Console.WriteLine("6 - Account options");

            //TEMP
            Console.WriteLine("Quick actions");
            Console.WriteLine("1 - View reservations");
            Console.WriteLine("2 - Timeslot options");
            Console.WriteLine("3 - Account options");
            Console.WriteLine("4 - Menu options");
            // Console.WriteLine("2 - Customer Lookup");
            // Console.WriteLine("3 - View dining menu");
            // Console.WriteLine("Menu's:");
            // Console.WriteLine("2 - Reservation options");
            // Console.WriteLine("5 - Dining menu options");
            // Console.WriteLine("3 - Account options");
            Console.WriteLine("\n\nQ - Logout");
            Console.WriteLine("====================================================================\n");
            Console.WriteLine("Choice:");
            string choice = Console.ReadLine();

            if (choice is not null) {
                switch (choice.ToLower())
                {
                    case "1":
                        ReservationDisplay.DisplayForRestaurant(SelectedRestaurant);
                        break;
                    case "2":
                        TimeSlotDisplay.Menu(LoggedInAccount);
                        break;
                    // case "3":
                    // NOTE: Need to implement a method to display the current menu items available for dinner
                    //     break;
                    // case "2":
                    //     ReservationDisplay.ReservationMenu_Admin(SelectedRestaurant, LoggedInAccount);
                    //     break;
                    // case "5":
                    // NOTE: Still need to implement this menu in a new Display class for the dining menu
                    //     break;
                    case "3":
                        AccountDisplay.AdminAccountMenu(LoggedInAccount);
                        break;
                    case "4":
                        MenuDisplay.MenuItemEditorMenu(SelectedRestaurant, LoggedInAccount);
                        break;
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

    private static void DisplayCustomerEnvironment() {
        while (true) {
            Console.Clear();
            Console.WriteLine("====================================================================");
            Console.WriteLine("▗▄▄▖ ▗▄▄▄▖ ▗▄▄▖▗▄▄▄▖▗▄▖ ▗▖ ▗▖▗▄▄▖  ▗▄▖ ▗▖  ▗▖▗▄▄▄▖\n" +
                                "▐▌ ▐▌▐▌   ▐▌     █ ▐▌ ▐▌▐▌ ▐▌▐▌ ▐▌▐▌ ▐▌▐▛▚▖▐▌  █  \n" + 
                                "▐▛▀▚▖▐▛▀▀▘ ▝▀▚▖  █ ▐▛▀▜▌▐▌ ▐▌▐▛▀▚▖▐▛▀▜▌▐▌ ▝▜▌  █  \n" +
                                "▐▌ ▐▌▐▙▄▄▖▗▄▄▞▘  █ ▐▌ ▐▌▝▚▄▞▘▐▌ ▐▌▐▌ ▐▌▐▌  ▐▌  █  ");
            Console.WriteLine("====================================================================\n");
            Console.WriteLine($"Welcome {LoggedInAccount.FirstName} {LoggedInAccount.LastName}\nWhat would you like to do?\n");
            Console.WriteLine("====================================================================\n");
            Console.WriteLine("1 - See current reservations");
            Console.WriteLine("2 - Make a reservation");
            Console.WriteLine("3 - Account options");
            Console.WriteLine("\n\nQ - Log out");
            Console.WriteLine("Choice:");
            string choice = Console.ReadLine();

            if (choice is not null) {
                switch (choice.ToLower())
                {
                    case "1":
                        ReservationDisplay.DisplayForRestaurantCustomer(SelectedRestaurant, LoggedInAccount);
                        break;
                    case "2":
                        ReservationDisplay.DisplayCreateReservation_Customer(SelectedRestaurant, LoggedInAccount);
                        break;
                    case "3":
                        AccountDisplay.AccountMenu(LoggedInAccount);
                        break;
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

    public static void LanguageSelectScreen()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("      PLEASE SELECT A LANGUAGE      ");
            Console.WriteLine("====================================\n");
            Console.WriteLine("1 - English");
            Console.WriteLine("2 - Nederlands\n\n");
            int selectedOption = 0;
            wyhile (true)
            {
                string userInput = Console.ReadLine();
                switch(userInput.ToLower())
                {
                    case "1":
                        selectedOption = 1;
                        break;
                    case "2":
                        selectedOption = 2;
                        break;
                     default:
                        Console.WriteLine("Invalid option, Please try again.");
                        break;



                }          

        }
    }

}