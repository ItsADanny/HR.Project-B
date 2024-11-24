using RRS.Logic;

public static class ProgramDisplay
{
    private static int SelectedRestaurant = 0;
    private static Accounts LoggedInAccount = null;

    public static void Display() 
    {
        string Header = "       MATCHMAKING RESTAURANT       \n====================================\n\nWelcome to the Matchmaking restaurant\n\n";
        string Footer = "\n\n A restaurant reservation solution \n     by Black Dawg International    ";
        while (true) 
        {
            switch (Functions.OptionSelector(Header, Footer, ["Login", "Create a new account", "Exit program"])) {
                case "Login":
                    DisplayLogin();
                    break;
                case "Create a new account":
                    DisplayCreateNewCustomerAccount();
                    break;
                case "Exit program":
                    Environment.Exit(0);
                    break;
            }
        }
    }

    private static void DisplayLogin() {
        int usedAttempts = 0;
        int allowedAttempts = 3;
        bool ExitLogin = false;

        do {
            Console.Clear();
            Console.WriteLine("       MATCHMAKING RESTAURANT       \n====================================\n\nLogin\n------------------------------------\n(Type: Q to exit the login)");
            Console.WriteLine("\x1b[35mE-mail: \x1b[39m");
            string usrInput_Email = Console.ReadLine();
            if (usrInput_Email.ToLower() == "q") {
                ExitLogin = true;
            } else {
                Console.WriteLine("\x1b[35mPassword: \x1b[39m");
                string usrInput_password = Functions.PasswordReadLine();

                Accounts account = Functions.Login(usrInput_Email, usrInput_password);

                if (account is not null) {
                    LoggedInAccount = account;
                } else {
                    usedAttempts++;
                    if (AccountLogic.DoesAccountEmailExist(usrInput_Email)) {
                        Console.WriteLine($"\nInvalid password, You have {allowedAttempts - usedAttempts} left");
                    } else {
                        Console.WriteLine($"\nNo account found with this E-mail, You have {allowedAttempts - usedAttempts} left");
                    }
                    Thread.Sleep(1500);
                }
            }

            if (usedAttempts == allowedAttempts) {
                break;
            }
            if (LoggedInAccount is not null) {
                break;
            }
            if (ExitLogin) {
                break;
            }
        } while (true);

        if (usedAttempts != allowedAttempts) {
            if (Functions.IsAccountAdmin(LoggedInAccount)) {
                Display_Admin_Environment();
                LoggedInAccount = null;
            } else {
                DisplayCustomerEnvironment();
                LoggedInAccount = null;
            }
        } else {
            Console.WriteLine("\nAll attempts have been used, Returning to start menu");
            Thread.Sleep(1500);
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
        bool Logout = false;
        string Header = $"====================================================================\n ▗▄▖ ▗▄▄▄  ▗▖  ▗▖▗▄▄▄▖▗▖  ▗▖\n▐▌ ▐▌▐▌  █ ▐▛▚▞▜▌  █  ▐▛▚▖▐▌\n▐▛▀▜▌▐▌  █ ▐▌  ▐▌  █  ▐▌ ▝▜▌\n▐▌ ▐▌▐▙▄▄▀ ▐▌  ▐▌▗▄█▄▖▐▌  ▐▌\n====================================================================\n\nWelcome {LoggedInAccount.FirstName} {LoggedInAccount.LastName}\nWhat would you like to do?\n\n";
        while (!Logout) {
            switch (Functions.OptionSelector(Header, ["Reservation menu", "Timeslot menu", "Dining menu", "Account menu", "Logout"])) {
                case "Reservation menu":
                    ReservationDisplay.DisplayForRestaurant(SelectedRestaurant);
                    break;
                case "Timeslot menu":
                    TimeSlotDisplay.Menu(LoggedInAccount);
                    break;
                case "Dining menu":
                    MenuDisplay.MenuItemEditorMenu(SelectedRestaurant, LoggedInAccount);
                    break;
                case "Account menu":
                    AccountDisplay.AdminAccountMenu(LoggedInAccount);
                    break;
                case "Logout":
                    Logout = true;
                    break;
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