public static class AccountDisplay {

    public static void AccountMenu(Accounts LoggedInAccount) {
        while (true) {
            Console.Clear();
            Console.WriteLine("====================================================================");
            Console.WriteLine(" ▗▄▖  ▗▄▄▖ ▗▄▄▖ ▗▄▖ ▗▖ ▗▖▗▖  ▗▖▗▄▄▄▖\n" +
                                "▐▌ ▐▌▐▌   ▐▌   ▐▌ ▐▌▐▌ ▐▌▐▛▚▖▐▌  █  \n" + 
                                "▐▛▀▜▌▐▌   ▐▌   ▐▌ ▐▌▐▌ ▐▌▐▌ ▝▜▌  █  \n" +
                                "▐▌ ▐▌▝▚▄▄▖▝▚▄▄▖▝▚▄▞▘▝▚▄▞▘▐▌  ▐▌  █  ");
            Console.WriteLine("====================================================================\n");
            Console.WriteLine("1 - Change password\n\n");
            Console.WriteLine("================================================================");
            Console.WriteLine("Please select an account to change (enter Q to exit):");

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
                        Console.WriteLine("Invalid input, please select a valid option");
                        Thread.Sleep(1500);
                        break;
                }
            }
        }
    }

    public static void ContactSharingDisplay(int restaurantID, Accounts LoggedInAccount) {
        string header = "====================================================================\n ▗▄▄▖ ▗▄▖ ▗▖  ▗▖▗▄▄▄▖▗▄▖  ▗▄▄▖▗▄▄▄▖     ▗▄▄▖▗▖ ▗▖ ▗▄▖ ▗▄▄▖ ▗▄▄▄▖▗▖  ▗▖ ▗▄▄▖\n▐▌   ▐▌ ▐▌▐▛▚▖▐▌  █ ▐▌ ▐▌▐▌     █      ▐▌   ▐▌ ▐▌▐▌ ▐▌▐▌ ▐▌  █  ▐▛▚▖▐▌▐▌   \n▐▌   ▐▌ ▐▌▐▌ ▝▜▌  █ ▐▛▀▜▌▐▌     █       ▝▀▚▖▐▛▀▜▌▐▛▀▜▌▐▛▀▚▖  █  ▐▌ ▝▜▌▐▌▝▜▌\n▝▚▄▄▖▝▚▄▞▘▐▌  ▐▌  █ ▐▌ ▐▌▝▚▄▄▖  █      ▗▄▄▞▘▐▌ ▐▌▐▌ ▐▌▐▌ ▐▌▗▄█▄▖▐▌  ▐▌▝▚▄▞▘\n====================================================================\n\n";
        
        bool Exit = false;

        while (!Exit) {
            List<Reservations> previousReservations = ReservationLogic.RetrievePreviousReservations(restaurantID, LoggedInAccount.ID);
            List<string> options = ReservationLogic.ConvertToDisplayString(previousReservations);
            options.Add("Exit");
            int selectedOption = Functions.OptionSelector(header, options);

            if (selectedOption == previousReservations.Count()) {
                Exit = true;
            } else {
                ReservationContactsDisplay(restaurantID, LoggedInAccount, previousReservations[selectedOption]);
            }
        }
    }

    private static void ReservationContactsDisplay(int restaurantID, Accounts LoggedInAccount, Reservations reservations) {
        List<Reservations> reservationsForTimeslot = ReservationLogic.RetrieveReservations(restaurantID, reservations);
        //Remove the users reservation from the list
        reservationsForTimeslot.RemoveAll(x => x.ID == reservations.ID);
        List<Accounts> accounts = [];
        List<string> options = [];

        foreach (Reservations reservationForAccount in reservationsForTimeslot) {
            Accounts userAccount = AccountLogic.GetSelectedAccount(reservationForAccount.ID - 10);

            accounts.Add(userAccount);
            string infoSharedText = "";
            //Based on the integer we saved in our database we can see in which stage the sharing currentlty is
            switch (AccountLogic.GetInfoSharedCode(LoggedInAccount, userAccount)) {
                case 0:
                    infoSharedText = "Not shared yet";
                    break;
                case 1:
                    infoSharedText = "Request send";
                    break;
                case 2:
                    infoSharedText = "Request received";
                    break;
                case 3:
                    infoSharedText = "Shared";
                    break;
                default:
                    infoSharedText = "Not shared yet";
                    break;
            }

            options.Add($"{userAccount.FirstName} {userAccount.LastName} - {infoSharedText}");
        }

        options.Add("Exit");

        string header = "====================================================================\n ▗▄▄▖ ▗▄▖ ▗▖  ▗▖▗▄▄▄▖▗▄▖  ▗▄▄▖▗▄▄▄▖     ▗▄▄▖▗▖ ▗▖ ▗▄▖ ▗▄▄▖ ▗▄▄▄▖▗▖  ▗▖ ▗▄▄▖\n▐▌   ▐▌ ▐▌▐▛▚▖▐▌  █ ▐▌ ▐▌▐▌     █      ▐▌   ▐▌ ▐▌▐▌ ▐▌▐▌ ▐▌  █  ▐▛▚▖▐▌▐▌   \n▐▌   ▐▌ ▐▌▐▌ ▝▜▌  █ ▐▛▀▜▌▐▌     █       ▝▀▚▖▐▛▀▜▌▐▛▀▜▌▐▛▀▚▖  █  ▐▌ ▝▜▌▐▌▝▜▌\n▝▚▄▄▖▝▚▄▞▘▐▌  ▐▌  █ ▐▌ ▐▌▝▚▄▄▖  █      ▗▄▄▞▘▐▌ ▐▌▐▌ ▐▌▐▌ ▐▌▗▄█▄▖▐▌  ▐▌▝▚▄▞▘\n====================================================================\n\n";
        bool Exit = false;
        while (!Exit) {
            int selectedOption = Functions.OptionSelector(header, options);

            if (selectedOption == accounts.Count()) {
                Exit = true;
            } else {
                ReservationContactsDetailsDisplay(restaurantID, LoggedInAccount, accounts[selectedOption]);
            }
        }
    }

    private static void ReservationContactsDetailsDisplay(int restaurantID, Accounts LoggedInAccount, Accounts SelectedAccount) {
        string header = "====================================================================\n ▗▄▄▖ ▗▄▖ ▗▖  ▗▖▗▄▄▄▖▗▄▖  ▗▄▄▖▗▄▄▄▖     ▗▄▄▖▗▖ ▗▖ ▗▄▖ ▗▄▄▖ ▗▄▄▄▖▗▖  ▗▖ ▗▄▄▖\n▐▌   ▐▌ ▐▌▐▛▚▖▐▌  █ ▐▌ ▐▌▐▌     █      ▐▌   ▐▌ ▐▌▐▌ ▐▌▐▌ ▐▌  █  ▐▛▚▖▐▌▐▌   \n▐▌   ▐▌ ▐▌▐▌ ▝▜▌  █ ▐▛▀▜▌▐▌     █       ▝▀▚▖▐▛▀▜▌▐▛▀▜▌▐▛▀▚▖  █  ▐▌ ▝▜▌▐▌▝▜▌\n▝▚▄▄▖▝▚▄▞▘▐▌  ▐▌  █ ▐▌ ▐▌▝▚▄▄▖  █      ▗▄▄▞▘▐▌ ▐▌▐▌ ▐▌▐▌ ▐▌▗▄█▄▖▐▌  ▐▌▝▚▄▞▘\n====================================================================\n\n";
        header += $"{SelectedAccount.FirstName} {SelectedAccount.LastName}\n";
        List<string> options = [];

        int AccountSharedCode = AccountLogic.GetInfoSharedCode(LoggedInAccount, SelectedAccount);
        switch (AccountSharedCode) {
            case 0:
                header += "Not shared yet";
                options.Add("Send request");
                break;
            case 1:
                header += "Request send";
                break;
            case 2:
                header += "Request received";
                options.Add("Accept request");
                options.Add("Decline request");
                break;
            case 3:
                header += $"";
                break;
            case 4:
                header += "Request declined";
                break;
            default:
                header += "Not shared yet";
                options.Add("Send request");
                break;
        }

        bool Exit = false;
        while (!Exit) {
            int selectedOption = Functions.OptionSelector(header, options);

            if (selectedOption == options.Count()) {
                Exit = true;
            } else {
                switch (AccountSharedCode) {
                    case 0: default:
                        //Send request
                        AccountLogic.UpdateShareInfo(LoggedInAccount.ID, SelectedAccount.ID, 2);
                        Console.WriteLine("Request send...");
                        Thread.Sleep(1500);

                        Exit = true;
                        break;
                    case 2:
                        if (selectedOption == 0) {
                            //Accept the request
                            AccountLogic.UpdateShareInfo(LoggedInAccount.ID, SelectedAccount.ID, 3);
                            Console.WriteLine("Request accepted...");
                            Thread.Sleep(1500);
                        } else {
                            //Decline the request
                            AccountLogic.UpdateShareInfo(LoggedInAccount.ID, SelectedAccount.ID, 4);
                            Console.WriteLine("Request declined...");
                            Thread.Sleep(1500);
                        }

                        Exit = true;
                        break;
                }
            }
        }
    }
    
    public static void AdminAccountMenu(Accounts LoggedInAccount) {
        bool CanCreateAdminAccounts = AccountLogic.CanDisplay("createAdmins", LoggedInAccount);
        bool Exit = false;
        
        string header = "====================================================================\n ▗▄▖ ▗▄▄▄ ▗▖  ▗▖▗▄▄▄▖▗▖  ▗▖     ▗▄▖  ▗▄▄▖ ▗▄▄▖ ▗▄▖ ▗▖ ▗▖▗▖  ▗▖▗▄▄▄▖\n▐▌ ▐▌▐▌  █▐▛▚▞▜▌  █  ▐▛▚▖▐▌    ▐▌ ▐▌▐▌   ▐▌   ▐▌ ▐▌▐▌ ▐▌▐▛▚▖▐▌  █  \n▐▛▀▜▌▐▌  █▐▌  ▐▌  █  ▐▌ ▝▜▌    ▐▛▀▜▌▐▌   ▐▌   ▐▌ ▐▌▐▌ ▐▌▐▌ ▝▜▌  █  \n▐▌ ▐▌▐▙▄▄▀▐▌  ▐▌▗▄█▄▖▐▌  ▐▌    ▐▌ ▐▌▝▚▄▄▖▝▚▄▄▖▝▚▄▞▘▝▚▄▞▘▐▌  ▐▌  █  \n====================================================================\n\n";
        List<string> options = ["Create a new customer account"];
        if (CanCreateAdminAccounts) {
            options.AddRange(["Create a new admin account", "Lookup account", "Change accountlevels for an account", "Delete account", "Change password"]);
        } else {
            options.AddRange(["Lookup account", "Change password"]);
        }
        options.Add("Exit");

        while (!Exit) {
            if (CanCreateAdminAccounts) {
                switch (Functions.OptionSelector(header, options)) {
                    case 0:
                        CreateCustomerAccount(LoggedInAccount);
                        break;
                    case 1:
                        CreateAdminAccount(LoggedInAccount);
                        break;
                    case 2:
                        LookupAccounts(LoggedInAccount);
                        break;
                    case 3:
                        ChangeAccountLevels(LoggedInAccount);
                        break;
                    case 4:
                        DeleteAccount(LoggedInAccount);
                        break;
                    case 5:
                        NewPassword(LoggedInAccount);
                        break;
                    case 6:
                        Exit = true;
                        break;
                }
            } else {
                switch (Functions.OptionSelector(header, options)) {
                    case 0:
                        LookupAccounts(LoggedInAccount);
                        break;
                    case 1:
                        NewPassword(LoggedInAccount);
                        break;
                    case 2:
                        Exit = true;
                        break;
                }
            }
        }
    }

    private static void ChangeAccountLevels(Accounts LoggedInAccount) {
        Accounts selectedAccount = null;
        AccountLevel selectedAccountLevel = null;
        bool WantsToExit = false;
        do {
            AccountLogic.PrintAccounts(LoggedInAccount);
            Console.WriteLine("================================================================");
            Console.WriteLine("Please select an account to change (enter Q to exit):");
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
                AccountLogic.PrintAccountLevels();
                Console.WriteLine("================================================================");
                Console.WriteLine("Please select an accountlevel to change to (enter Q to exit):");
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

    private static void CreateCustomerAccount(Accounts LoggedInAccount) 
    {

    }

    private static void CreateAdminAccount(Accounts LoggedInAccount) 
    {

    }

    private static void LookupAccounts(Accounts LoggedInAccount) {
        bool Exit = false;
        string accounts = AccountLogic.GetAccountsDisplay(LoggedInAccount);
        
        string header = $"====================================================================\n ▗▄▖ ▗▄▄▄ ▗▖  ▗▖▗▄▄▄▖▗▖  ▗▖     ▗▄▖  ▗▄▄▖ ▗▄▄▖ ▗▄▖ ▗▖ ▗▖▗▖  ▗▖▗▄▄▄▖\n▐▌ ▐▌▐▌  █▐▛▚▞▜▌  █  ▐▛▚▖▐▌    ▐▌ ▐▌▐▌   ▐▌   ▐▌ ▐▌▐▌ ▐▌▐▛▚▖▐▌  █  \n▐▛▀▜▌▐▌  █▐▌  ▐▌  █  ▐▌ ▝▜▌    ▐▛▀▜▌▐▌   ▐▌   ▐▌ ▐▌▐▌ ▐▌▐▌ ▝▜▌  █  \n▐▌ ▐▌▐▙▄▄▀▐▌  ▐▌▗▄█▄▖▐▌  ▐▌    ▐▌ ▐▌▝▚▄▄▖▝▚▄▄▖▝▚▄▞▘▝▚▄▞▘▐▌  ▐▌  █  \n====================================================================\n\n{accounts}\n\n====================================================================\n\n";
        List<string> options = ["Select by ID", "Select by Firstname", "Select by Lastname", "Select by E-mail", "Exit"];

        while (!Exit) {
            switch (Functions.OptionSelector(header, options)) {
                    case 0:
                        LookupAccounts_ID(LoggedInAccount);
                        Exit = true;
                        break;
                    case 1:
                        LookupAccounts_Firstname(LoggedInAccount);
                        Exit = true;
                        break;
                    case 2:
                        LookupAccounts_Lastname(LoggedInAccount);
                        Exit = true;
                        break;
                    case 3:
                        LookupAccounts_Email(LoggedInAccount);
                        Exit = true;
                        break;
                    case 4:
                        Exit = true;
                        break;
            }
        }
    }

    private static void LookupAccounts_ID (Accounts LoggedInAccount) {
        Console.WriteLine("Please input an ID for the user you want to search for:");
        string userInput = Console.ReadLine();
        if (userInput is not null && userInput != "") {
            AccountLogic.SearchForUser_ByID(userInput);
        } else {
            Console.WriteLine("Invalid input, please use a valid ID for searching");
        }
        Console.WriteLine("Press enter to continue.");
        Console.ReadLine();
    }

    private static void LookupAccounts_Firstname (Accounts LoggedInAccount) {
        Console.WriteLine("Please input a firstname for the user you want to search for:");
        string userInput = Console.ReadLine();
        if (userInput is not null && userInput != "") {
            AccountLogic.SearchForUser_ByFirstName(userInput);
        } else {
            Console.WriteLine("Invalid input, please use a firstname for searching");
        }
        Console.WriteLine("Press enter to continue.");
        Console.ReadLine();
    }

    private static void LookupAccounts_Lastname (Accounts LoggedInAccount) {
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

    private static void LookupAccounts_Email (Accounts LoggedInAccount) {
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
    private static void DeleteAccount(Accounts LoggedInAccount) 
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
                    Console.WriteLine("Invalid response, please enter a valid option");
                }
            }
        }
    }

    private static int ChooseAccountLevel(){
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
                Console.WriteLine("Invalid option chosen, please select a valid option");
            }
        }
        return selectedAccountLevel;
    }

    private static void NewPassword(Accounts LoggedInAccount){
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
}