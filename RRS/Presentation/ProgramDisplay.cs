using System.Data.Entity.Core.Metadata.Edm;
using System.Globalization;
using System.Resources;
using RRS.Logic;
namespace RRS.Presentation;
public static class ProgramDisplay
{
    private static int SelectedRestaurant = 0;
    private static Accounts LoggedInAccount = null;

    private ILanguageInterface _languageInterface;

    public void LanguageSet(ILanguageInterface languageInterface)
    {
        _languageInterface = languageInterface;
    }

    public static void Display() 
    {
        string Opt1 = _languageInterface.GetString("Login");
        string Opt2 = _languageInterface.GetString("CreateNewAccount");
        string Opt3 = _languageInterface.GetString("ExitProg");
        string Title =  _languageInterface.GetString("TitleRest");
        string Welcome1 =  _languageInterface.GetString("WelcomeTo");
        string Welcome2 =  _languageInterface.GetString("RestSolution");
        string Welcome3 =  _languageInterface.GetString("BlkDwg");
        //Console.WriteLine("       " + Title + "       ");
        //Console.WriteLine("====================================");
        //Console.WriteLine("Welcome to the Matchmaking restaurant")
        //Console.WriteLine("A restaurant reservation solution")
        //Console.WriteLine("By Black Dawg International")


        string Header = $"       {Title}       \n====================================\n{Welcome1}";
        string Footer = $"{Welcome2}\n{Welcome3}";
        
        while (true) 
        {
            switch (Functions.OptionSelector(Header, Footer)) {
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
        string Attempts = languageInterface.GetString("AllAttempts");
        string Title = languageInterface.GetString("TitleRest");
        string Login = languageInterface.GetString("Login");
        string Exit = languageInterface.GetString("ExitLogin");

        do {
            Console.Clear();
            Console.WriteLine("       " + Title + "       "); //MATCHMAKING RESTAURANT
            Console.WriteLine("====================================");
            Console.WriteLine(Login); //Login
            Console.WriteLine("------------------------------------");
            Console.WriteLine(Exit); //(Type: Q to exit the login)
            Console.WriteLine("\x1b[35mE-mail: \x1b[39m");
            string usrInput_Email = Console.ReadLine();
            if (usrInput_Email.ToLower() == "q") {
                ExitLogin = true;
            } else {
                Console.WriteLine("\x1b[35m Password: \x1b[39m");
                string usrInput_password = Functions.PasswordReadLine();

                Accounts account = Functions.Login(usrInput_Email, usrInput_password);

                if (account is not null) {
                    LoggedInAccount = account;
                } else {
                    usedAttempts++;
                    if (AccountLogic.DoesAccountEmailExist(usrInput_Email)) {
                        Console.WriteLine($"\nInvalid password, You have {allowedAttempts - usedAttempts} left"); //EDIT MATH TO ONE VARIABLE STR
                    } else {
                        Console.WriteLine($"\nNo account found with this E-mail, You have {allowedAttempts - usedAttempts} left"); //EDIT MATH TO ONE AVAILABLE STRING
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

        string MaxAttempts = languageInterface.GetString("MaxAttempts");
   
        if (usedAttempts != allowedAttempts) {
            if (Functions.IsAccountAdmin(LoggedInAccount)) {
                Display_Admin_Environment();
                LoggedInAccount = null;
            } else {
                DisplayCustomerEnvironment();
                LoggedInAccount = null;
            }
        } else {
            Console.WriteLine(MaxAttempts);
            Thread.Sleep(1500);
        }
    }

    private static void DisplayCreateNewCustomerAccount() 
    {
        string Title = languageInterface.GetString("TitleRest");
        string Prompt = languageInterface.GetString("CreaAcc");
        string Pass = languageInterface.GetString("PW");
        string Success = languageInterface.GetString("CreateNewCustSuccess");
        string Error = languageInterface.GetString("CreateNewCustError");

        Console.Clear(); //empty screen
        Console.WriteLine("       " + Title + "       "); //MATCHMAKING RESTAURANT
        Console.WriteLine("====================================\n");
        Console.WriteLine(Prompt);

        //request needed information from user to create new account
        string FirstName = Functions.RequestValidString("First name");
        string LastName = Functions.RequestValidString("Last name");
        string Email = Functions.RequestValidEmail();
        string PhoneNumber = Functions.RequestValidPhonenumber("Phonenumber");
        Console.WriteLine(Pass);
        string Password = Functions.PasswordReadLine_WithValidCheck();
        

        //danny NOTE: THIS WILL ONLY BE REQUIRED FOR WHEN A ADMIN NEEDS TO CREATE AN ACCOUNT, AN CUSTOMER ACCOUNT IS ALWAYS 3
        // int Accountlevel = AccountLevelDisplay.ChooseAccountLevel();


        //account success or error message
        if (AccountLogic.CreateNewCustomerAccount(Email, Password, FirstName, LastName, PhoneNumber)) 
        {
            Console.WriteLine(Success);
        } 
        else 
        {
            Console.WriteLine(Error);
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


}