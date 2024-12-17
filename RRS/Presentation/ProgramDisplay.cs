using RRS.Logic;
using System.Drawing;
using Colorful;
//If you want to use the normal console tool, then you can just call the regular console
using Console = System.Console;
//If you want to use the colorful console tools, then you can ColorfulConsole instead of Console
using ColorfulConsole = Colorful.Console;


public static class ProgramDisplay
{
    private static int SelectedRestaurant = 0;
    private static Accounts LoggedInAccount = null;

    public static void Display()
    {
        string Header = "       MATCHMAKING RESTAURANT       \n====================================\n\nWelcome to the Matchmaking restaurant\n\n";
        string Footer = "\n\n An restaurant reservation solution \n     by Black Dawg International    ";
        while (true) 
        {
            switch (Functions.OptionSelector(Header, Footer, ["Login", "Create a new account", "Exit program"])) {
                case 0:
                    DisplayLogin();
                    break;
                case 1:
                    DisplayCreateNewCustomerAccount();
                    break;
                case 2:
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
            List<string> menuOptions = ["Reservation menu", "Timeslot menu", "Dining menu", "Review menu", "Account menu"];
            // if (AccountLogic.CanDisplay("logs", LoggedInAccount)) {
            //     menuOptions.Add("View database application logs");
            // }
            menuOptions.Add("Logout");

            switch (Functions.OptionSelector(Header, menuOptions)) {
                case 0:
                    ReservationDisplay.ReservationMenu_Admin(SelectedRestaurant, LoggedInAccount);
                    break;
                case 1:
                    TimeSlotDisplay.Menu(SelectedRestaurant, LoggedInAccount);
                    break;
                case 2:
                    MenuDisplay.MenuItemEditorMenu(SelectedRestaurant, LoggedInAccount);
                    break;
                case 3:
                    ReviewDisplay.ReviewDisplayAdmin(SelectedRestaurant, LoggedInAccount);
                    break;
                case 4:
                    AccountDisplay.AdminAccountMenu(LoggedInAccount);
                    break;
                // case 5:
                //     //TODO: IMPLEMENT AN ENVIRONMENT TO DISPLAY THESE LOGS
                //     LogsDisplay.Menu();
                //     break;
                case 5:
                    Logout = true;
                    break;
            }
        }
    }

    private static void DisplayCustomerEnvironment() {
        bool Logout = false;
        string Header = $"====================================================================\n▗▄▄▖ ▗▄▄▄▖ ▗▄▄▖▗▄▄▄▖▗▄▖ ▗▖ ▗▖▗▄▄▖  ▗▄▖ ▗▖  ▗▖▗▄▄▄▖\n▐▌ ▐▌▐▌   ▐▌     █ ▐▌ ▐▌▐▌ ▐▌▐▌ ▐▌▐▌ ▐▌▐▛▚▖▐▌  █  \n▐▛▀▚▖▐▛▀▀▘ ▝▀▚▖  █ ▐▛▀▜▌▐▌ ▐▌▐▛▀▚▖▐▛▀▜▌▐▌ ▝▜▌  █  \n▐▌ ▐▌▐▙▄▄▖▗▄▄▞▘  █ ▐▌ ▐▌▝▚▄▞▘▐▌ ▐▌▐▌ ▐▌▐▌  ▐▌  █  \n====================================================================\nWelcome {LoggedInAccount.FirstName} {LoggedInAccount.LastName}\nWhat would you like to do?\n====================================================================\n\n";
        while (!Logout) {
            switch (Functions.OptionSelector(Header, ["See current reservations", "Make a reservation", "View dining menu", "Reviews", "View restaurant layout", "Account options", "Logout"])) {
                case 0:
                    ReservationDisplay.DisplayForRestaurantCustomer(SelectedRestaurant, LoggedInAccount);
                    break;
                case 1:
                    ReservationDisplay.DisplayCreateReservation_Customer(SelectedRestaurant, LoggedInAccount);
                    break;
                case 2:
                    MenuDisplay.ViewMenuList(SelectedRestaurant);
                    break;
                case 3:
                    ReviewDisplay.ReviewDisplayCustomer(SelectedRestaurant, LoggedInAccount);
                    break;
                case 4:
                    ReservationDisplay.PrintFloorPlan();
                    break;
                case 5:
                    AccountDisplay.AccountMenu(LoggedInAccount);
                    break;
                case 6:
                    Logout = true;
                    break;
            }
        }
    }

    public static void Bootup() {
        Console.Clear();

        UIColor[] logoColors = [
            new UIColor(255, 0, 0), new UIColor(0, 255, 0),
            new UIColor(0, 0, 255), new UIColor(255, 0, 255),
            new UIColor(255, 255, 255), new UIColor(255, 255, 255),
            new UIColor(255, 255, 255), new UIColor(0, 0, 0),
            new UIColor(255, 255, 255), new UIColor(68, 68, 68),
            new UIColor(255, 255, 255), new UIColor(68, 68, 68),
            new UIColor(0, 0, 0), new UIColor(255, 255, 255)
        ];

        string[] logoStrings = [
            "▗▄▄▖ ▗▖    ▗▄▖  ▗▄▄▖▗▖ ▗▖    ▗▄▄▄  ▗▄▖ ▗▖ ▗▖ ▗▄▄▖",
            "▐▌ ▐▌▐▌   ▐▌ ▐▌▐▌   ▐▌▗▞▘    ▐▌  █▐▌ ▐▌▐▌ ▐▌▐▌   ",
            "▐▛▀▚▖▐▌   ▐▛▀▜▌▐▌   ▐▛▚▖     ▐▌  █▐▛▀▜▌▐▌ ▐▌▐▌▝▜▌",
            "▐▙▄▞▘▐▙▄▄▖▐▌ ▐▌▝▚▄▄▖▐▌ ▐▌    ▐▙▄▄▀▐▌ ▐▌▐▙█▟▌▝▚▄▞▘",
            "=================================================",
            "Black Dawg International, copyright (2024-2024)",
            "RRS Application version 1.0.2 - December build",
            " ",
            $"Machine operating system:",
            $"{System.Runtime.InteropServices.RuntimeInformation.OSDescription}",
            $"Machine operating system Architecture:",
            $"{System.Runtime.InteropServices.RuntimeInformation.OSArchitecture}",
            " ",
            "Loading application"
        ];
        
        for (int i = 0; i != logoStrings.Length; i++) {
            UIColor UIC = logoColors[i];
            ColorfulConsole.WriteLine(logoStrings[i], Color.FromArgb(UIC.R, UIC.G, UIC.B));
        }

        Thread.Sleep(2500);
    }
}