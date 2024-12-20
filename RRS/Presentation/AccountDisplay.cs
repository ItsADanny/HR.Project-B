using System.Globalization;
using System.Resources;
namespace RRS.interfaces;

public static class AccountDisplay {
    private ILanguageInterface _languageInterface;

    public void LanguageSet(ILanguageInterface languageInterface)
    {
        _languageInterface = languageInterface;
    }

    public static void AccountMenu(Accounts LoggedInAccount) {
        while (true) {
            string Changepass = _languageInterface.GetString("ChangePassword");
            string Select = _languageInterface.GetString("PleaseSelectAcc");
            string Error = _languageInterface.GetString("ValidOpt");

            Console.Clear();
            Console.WriteLine("====================================================================");
            Console.WriteLine(" ▗▄▖  ▗▄▄▖ ▗▄▄▖ ▗▄▖ ▗▖ ▗▖▗▖  ▗▖▗▄▄▄▖\n" +
                                "▐▌ ▐▌▐▌   ▐▌   ▐▌ ▐▌▐▌ ▐▌▐▛▚▖▐▌  █  \n" + 
                                "▐▛▀▜▌▐▌   ▐▌   ▐▌ ▐▌▐▌ ▐▌▐▌ ▝▜▌  █  \n" +
                                "▐▌ ▐▌▝▚▄▄▖▝▚▄▄▖▝▚▄▞▘▝▚▄▞▘▐▌  ▐▌  █  ");
            Console.WriteLine("====================================================================\n");
            Console.WriteLine(Changepass);     //("1 - Change password\n\n");
            Console.WriteLine("================================================================");
            Console.WriteLine(Select);

            string userInput = Console.ReadLine();
            if (userInput.ToLower() == "q") {
                break;
            } else {
                switch (userInput)
                {
                    case "1":
                        NewPassword(LoggedInAccount);
                        break;
                    default:
                        Console.WriteLine(Error);
                        Thread.Sleep(1500);
                        break;
                }
            }
        }
    }
    
    public static void AdminAccountMenu(Accounts LoggedInAccount) {
        bool CanCreateAdminAccounts = AccountLogic.CanDisplay_CreateAdminAccounts(LoggedInAccount);

        while (true) {
            string Opt1 = _languageInterface.GetString("SuperAdminMenuOpt1");
            string Opt2 = _languageInterface.GetString("SuperAdminMenuOpt2");
            string Opt3 = _languageInterface.GetString("SuperAdminMenuOpt3");
            string Opt4 = _languageInterface.GetString("SuperAdminMenuOpt4");
            string Opt5 = _languageInterface.GetString("SuperAdminMenuOpt5");
            string Opt6 = _languageInterface.GetString("SuperAdminMenuOpt6");
            string Opt2Ad = _languageInterface.GetString("AdminMenuOpt2");
            string Opt3Ad = _languageInterface.GetString("AdminMenuOpt3");
            string Please = _languageInterface.GetString("PleaseSelectAccChange");
            string Error = _languageInterface.GetString("ValidOpt");

            Console.Clear();
            Console.WriteLine("====================================================================");
            Console.WriteLine(" ▗▄▖ ▗▄▄▄ ▗▖  ▗▖▗▄▄▄▖▗▖  ▗▖     ▗▄▖  ▗▄▄▖ ▗▄▄▖ ▗▄▖ ▗▖ ▗▖▗▖  ▗▖▗▄▄▄▖\n" +
                                "▐▌ ▐▌▐▌  █▐▛▚▞▜▌  █  ▐▛▚▖▐▌    ▐▌ ▐▌▐▌   ▐▌   ▐▌ ▐▌▐▌ ▐▌▐▛▚▖▐▌  █  \n" + 
                                "▐▛▀▜▌▐▌  █▐▌  ▐▌  █  ▐▌ ▝▜▌    ▐▛▀▜▌▐▌   ▐▌   ▐▌ ▐▌▐▌ ▐▌▐▌ ▝▜▌  █  \n" +
                                "▐▌ ▐▌▐▙▄▄▀▐▌  ▐▌▗▄█▄▖▐▌  ▐▌    ▐▌ ▐▌▝▚▄▄▖▝▚▄▄▖▝▚▄▞▘▝▚▄▞▘▐▌  ▐▌  █  ");
            Console.WriteLine("====================================================================\n");
            Console.WriteLine(Opt1); //1 - Create a new customer account
            if (CanCreateAdminAccounts) {
                Console.WriteLine(Opt2); //2 - Create a new admin account
                Console.WriteLine(Opt3); //3 - Lookup account
                Console.WriteLine(Opt4); //4 - Change accountlevels for an account
                Console.WriteLine(Opt5); //5 - Delete account
                Console.WriteLine(Opt6);//6 - Change password\n\n
            } else {
                Console.WriteLine(Opt2Ad); //2 - Lookup account
                Console.WriteLine(Opt3Ad);//3 - Change password\n\n
            }
            Console.WriteLine("================================================================");
            Console.WriteLine(Please); //Please select an account to change (enter Q to exit):
            
            string userInput = Console.ReadLine();
            if (userInput.ToLower() == "q") {
                break;
            } else {
                if (CanCreateAdminAccounts) {
                    switch (userInput)
                    {
                        case "1":
                            CreateCustomerAccount(LoggedInAccount);
                            break;
                        case "2":
                            CreateAdminAccount(LoggedInAccount);
                            break;
                        case "3":
                            LookupAccounts(LoggedInAccount);
                            break;
                        case "4":
                            ChangeAccountLevels(LoggedInAccount);
                            break;
                        case "5":
                            DeleteAccount(LoggedInAccount);
                            break;
                        case "6":
                            NewPassword(LoggedInAccount);
                            break;
                        default:
                            Console.WriteLine(Error); //Invalid input, please select a valid option
                            Thread.Sleep(1500);
                            break;
                    }
                } else {
                    switch (userInput)
                    {
                        case "1":
                            CreateCustomerAccount(LoggedInAccount);
                            break;
                        case "2":
                            LookupAccounts(LoggedInAccount);
                            break;
                        case "3":
                            NewPassword(LoggedInAccount);
                            break;
                        default:
                            Console.WriteLine(Error);
                            Thread.Sleep(1500);
                            break;
                    }
                }
            }
        }
    }

    public static void ChangeAccountLevels(Accounts LoggedInAccount) {
        Accounts selectedAccount = null;
        AccountLevel selectedAccountLevel = null;
        bool WantsToExit = false;
        do {
            string Please = _languageInterface.GetString("PleaseSelectAccChange");
            AccountLogic.PrintAccounts(LoggedInAccount);
            Console.WriteLine("================================================================");
            Console.WriteLine(Please); //Please select an account to change (enter Q to exit):
            string userInput = Console.ReadLine();
            if (userInput.ToLower() == "q") {
                WantsToExit = true;
            } else {
                selectedAccount = AccountLogic.GetSelectedAccount(LoggedInAccount, userInput);
                //DEBUG
                // Console.WriteLine(selectedAccount.ToString());
            }
        } while (selectedAccount == null & !WantsToExit);

        if (!WantsToExit) {
            do {
                string Please = _languageInterface.GetString("PleaseSelectAccChange");
                AccountLogic.PrintAccountLevels();
                Console.WriteLine("================================================================");
                Console.WriteLine(Please); //Please select an account to change (enter Q to exit):
                string userInput = Console.ReadLine();
                if (userInput.ToLower() == "q") {
                    WantsToExit = true;
                } else {
                    selectedAccountLevel = AccountLogic.GetSelectedAccountlevel(userInput);
                }
            } while (selectedAccountLevel == null & !WantsToExit);

            if (!WantsToExit) {
                AccountLogic.ChangeAccountLevel(selectedAccount, selectedAccountLevel, LoggedInAccount);
            }
        }
    }

    public static void CreateCustomerAccount(Accounts LoggedInAccount) 
    {

    }

    public static void CreateAdminAccount(Accounts LoggedInAccount) 
    {

    }

    public static void LookupAccounts(Accounts LoggedInAccount) {
        while (true) {
            string SelectHeader = _languageInterface.GetString("SelectOpt");
            string Opt1 = _languageInterface.GetString("LookAccOpt1");
            string Opt2 = _languageInterface.GetString("LookAccOpt2");
            string Opt3 = _languageInterface.GetString("LookAccOpt3");
            string Opt4 = _languageInterface.GetString("LookAccOpt4");
            string Please = _languageInterface.GetString("PleaseSelectSelection");
            string Error = _languageInterface.GetString("ValidOpt");


            Console.Clear();
            AccountLogic.PrintAccounts(LoggedInAccount);
            Console.WriteLine("====================================================================\n");
            Console.WriteLine(SelectHeader); //Selection options:
            Console.WriteLine(Opt1); //1 - Select by ID
            Console.WriteLine(Opt2); //2 - Select by Firstname
            Console.WriteLine(Opt3); //3 - Select by Lastname
            Console.WriteLine(Opt4); //4 - Select by E-mail\n\n
            Console.WriteLine("====================================================================");
            Console.WriteLine(Please); //Please select a selection method (enter Q to exit):

            string userInput = Console.ReadLine();
            if (userInput.ToLower() == "q") {
                break;
            } else {
                switch (userInput) {
                    case "1":
                        LookupAccounts_ID(LoggedInAccount);
                        break;
                    case "2":
                        LookupAccounts_Firstname(LoggedInAccount);
                        break;
                    case "3":
                        LookupAccounts_Lastname(LoggedInAccount);
                        break;
                    case "4":
                        LookupAccounts_Email(LoggedInAccount);
                        break;
                    default:
                        Console.WriteLine(Error);
                        break;
                }
            }
        }
    }

    public static void LookupAccounts_ID (Accounts LoggedInAccount) {
        string PlsID = _languageInterface.GetString("PlsIDsearch");
        string Error = _languageInterface.GetString("IDSearchError");
        string Enter = _languageInterface.GetString("EnterToCont");

        Console.WriteLine(PlsID);//Please input an ID for the user you want to search for:
        string userInput = Console.ReadLine();
        if (userInput is not null && userInput != "") {
            AccountLogic.SearchForUser_ByID(userInput);
        } else {
            Console.WriteLine(Error); //Invalid input, please use a valid ID for searching
        }
        Console.WriteLine(Enter); //Press enter to continue.
        Console.ReadLine();
    }

    public static void LookupAccounts_Firstname (Accounts LoggedInAccount) {
        string PlsFN = _languageInterface.GetString("PlsIDsearch");
        string Error = _languageInterface.GetString("IDSearchError");
        string Enter = _languageInterface.GetString("EnterToCont");

        Console.WriteLine("Please input a firstname for the user you want to search for:"); //Please input a firstname for the user you want to search for:        string userInput = Console.ReadLine();
        if (userInput is not null && userInput != "") {
            AccountLogic.SearchForUser_ByFirstName(userInput);
        } else {
            Console.WriteLine("Invalid input, please use a firstname for searching");
        }
        Console.WriteLine("Press enter to continue.");
        Console.ReadLine();
    }

    public static void LookupAccounts_Lastname (Accounts LoggedInAccount) {
        Console.WriteLine("Please input a lastname for the user you want to search for:");
        string userInput = Console.ReadLine();
        if (userInput is not null && userInput != "") {
            AccountLogic.SearchForUser_ByLastName(userInput);
        } else {
            Console.WriteLine("Invalid input, please use a lastname for searching");
        }
        Console.WriteLine("Press enter to continue.");
        Console.ReadLine();
    }

    public static void LookupAccounts_Email (Accounts LoggedInAccount) {
        Console.WriteLine("Please input an e-mail for the user you want to search for:");
        string userInput = Console.ReadLine();
        if (userInput is not null && userInput != "") {
            AccountLogic.SearchForUser_ByEmail(userInput);
        } else {
            Console.WriteLine("Invalid input, please use an e-mail for searching");
        }
        Console.WriteLine("Press enter to continue.");
        Console.ReadLine();
    }
    
    //mick
    public static void DeleteAccount(Accounts LoggedInAccount) 
    {
        Accounts selectedAccount = null;
        bool WantsToExit = false;

        do 
        {

            Console.WriteLine("================================================================");
            //users that logged in user can delete (usually only themselves if customer)
            AccountLogic.PrintAccounts(LoggedInAccount);
            Console.WriteLine("================================================================");
            Console.WriteLine("Please select an account to delete:");
            Console.WriteLine("or enter Q to exit");

            string userInput = Console.ReadLine();

            //if q then quit program else account selected will be checked
            if (userInput.ToLower() == "q") 
            {
                WantsToExit = true;
                return;
            } 
            else 
            {
                selectedAccount = AccountLogic.GetSelectedAccount(LoggedInAccount, userInput);
            }
        } while (selectedAccount == null & !WantsToExit);
        
        //if account selected ask confirmation and delete
        if (!WantsToExit & selectedAccount is not null) 
        {
            while (true) 
            {
                Console.WriteLine("================================================================");
                AccountLogic.PrintAccounts(selectedAccount);
                Console.WriteLine("================================================================");
                Console.WriteLine("Are you sure you want to delete this account?\n\n");
                Console.WriteLine("Y - yes");
                Console.WriteLine("N - no");

                string userInput = Console.ReadLine();
                
                //if yes acc deleted by ID
                if (userInput.ToLower() == "y") 
                {
                    AccountLogic.RemoveAccountFromSystem(selectedAccount.ID);
                    break;
                } 
                else if (userInput.ToLower() == "n") 
                {
                    break;
                } 
                else 
                {
                    Console.WriteLine("Invalid input, please enter a valid option");
                }
            }
        }
    }

    public static int ChooseAccountLevel(){
        List<AccountLevel> accountLevels = Database.SelectAccountLevel();
        int selectedAccountLevel = 0;
        while (true) {
            foreach (AccountLevel accountLevel in accountLevels) {
                accountLevel.ToString();
            }
            Console.WriteLine("Choose Account Level:");
            if (int.TryParse(Console.ReadLine(), out int result)) {
                foreach (AccountLevel accountLevel in accountLevels) {
                    if (accountLevel.ID == result) {
                        selectedAccountLevel = result;
                    }
                }
            }

            if (selectedAccountLevel != 0) {
                break;
            } else {
                Console.WriteLine("Invalid option, please select a valid option");
            }
        }
        return selectedAccountLevel;
    }

    public static void NewPassword(Accounts LoggedInAccount){
        Console.WriteLine("Please enter your current password");
        string currentPassword = Functions.PasswordReadLine();
        // Database.CheckAccountPassword(LoggedInAccount.ID, currentPassword); // CheckAccountPassword(Int Id, String input)
        if (AccountLogic.CheckCurrPassword(currentPassword, LoggedInAccount)){
            System.Console.WriteLine("\nPlease enter your new password:");
            string newPassword = Functions.PasswordReadLine();
            System.Console.WriteLine("\nPlease confirm your new password:");
            string confirmPassword = Functions.PasswordReadLine();
            if (newPassword == confirmPassword){
                Database.UpdatePasswordForAccount(LoggedInAccount, currentPassword, newPassword);
                Console.WriteLine("Password changed successfully");
                Thread.Sleep(1500);
            }
        }
    }

    public static void NewPhoneNumber(Accounts LoggedInAccount)
    {
        Console.WriteLine("Enter your old phonenumber: ");
        string oldPhoneNumber = Functions.RequestValidPhonenumber();
        
    }
}