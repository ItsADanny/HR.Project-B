public static class AccountDisplay {

    public static void AccountMenu(Accounts LoggedInAccount) {
        while (true) {
            Console.WriteLine("====================================================================");
            Console.WriteLine(" ▗▄▖  ▗▄▄▖ ▗▄▄▖ ▗▄▖ ▗▖ ▗▖▗▖  ▗▖▗▄▄▄▖\n" +
                                "▐▌ ▐▌▐▌   ▐▌   ▐▌ ▐▌▐▌ ▐▌▐▛▚▖▐▌  █  \n" + 
                                "▐▛▀▜▌▐▌   ▐▌   ▐▌ ▐▌▐▌ ▐▌▐▌ ▝▜▌  █  \n" +
                                "▐▌ ▐▌▝▚▄▄▖▝▚▄▄▖▝▚▄▞▘▝▚▄▞▘▐▌  ▐▌  █  ");
            Console.WriteLine("====================================================================\n");
        }
    }
    
    public static void AdminAccountMenu(Accounts LoggedInAccount) {
        bool CanCreateAdminAccounts = AccountLogic.CanDisplay_CreateAdminAccounts(LoggedInAccount);

        while (true) {
            Console.WriteLine("====================================================================");
            Console.WriteLine(" ▗▄▖ ▗▄▄▄ ▗▖  ▗▖▗▄▄▄▖▗▖  ▗▖     ▗▄▖  ▗▄▄▖ ▗▄▄▖ ▗▄▖ ▗▖ ▗▖▗▖  ▗▖▗▄▄▄▖\n" +
                                "▐▌ ▐▌▐▌  █▐▛▚▞▜▌  █  ▐▛▚▖▐▌    ▐▌ ▐▌▐▌   ▐▌   ▐▌ ▐▌▐▌ ▐▌▐▛▚▖▐▌  █  \n" + 
                                "▐▛▀▜▌▐▌  █▐▌  ▐▌  █  ▐▌ ▝▜▌    ▐▛▀▜▌▐▌   ▐▌   ▐▌ ▐▌▐▌ ▐▌▐▌ ▝▜▌  █  \n" +
                                "▐▌ ▐▌▐▙▄▄▀▐▌  ▐▌▗▄█▄▖▐▌  ▐▌    ▐▌ ▐▌▝▚▄▄▖▝▚▄▄▖▝▚▄▞▘▝▚▄▞▘▐▌  ▐▌  █  ");
            Console.WriteLine("====================================================================\n");
            Console.WriteLine("1 - Create an new customer account");
            if (CanCreateAdminAccounts) {
                Console.WriteLine("2 - Create an new admin account");
                Console.WriteLine("3 - Lookup account");
                Console.WriteLine("4 - Change accountlevels for a account");
                Console.WriteLine("5 - Delete account\n\n");
            } else {
                Console.WriteLine("2 - Lookup account\n\n");
            }
            Console.WriteLine("================================================================");
            Console.WriteLine("Please select an account to change (enter Q to exit):");
            
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
                        default:
                            Console.WriteLine("Invalid input, please select an valid option");
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
                        default:
                            Console.WriteLine("Invalid input, please select an valid option");
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
                Console.WriteLine("Please select an accountlevel to change too (enter Q to exit):");
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

    public static void CreateCustomerAccount(Accounts LoggedInAccount) {

    }

    public static void CreateAdminAccount(Accounts LoggedInAccount) {

    }

    public static void LookupAccounts(Accounts LoggedInAccount) {
        while (true) {
            Console.Clear();
            AccountLogic.PrintAccounts(LoggedInAccount);
            Console.WriteLine("====================================================================\n");
            Console.WriteLine("Selection options:");
            Console.WriteLine("1 - Select by ID");
            Console.WriteLine("2 - Select by Firstname");
            Console.WriteLine("3 - Select by Lastname");
            Console.WriteLine("4 - Select by E-mail\n\n");
            Console.WriteLine("====================================================================");
            Console.WriteLine("Please select an selection method (enter Q to exit):");

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
                        Console.WriteLine("Invalid input, please select an valid option");
                        break;
                }
            }
        }
    }

    public static void LookupAccounts_ID (Accounts LoggedInAccount) {
        Console.WriteLine("Please input an ID for the user you want to search for:");
        string userInput = Console.ReadLine();
        if (userInput is not null && userInput != "") {
            AccountLogic.SearchForUser_ByID(userInput);
        } else {
            Console.WriteLine("Invalid input, please use an valid ID for searching");
        }
        Console.WriteLine("Press enter to continue.");
        Console.ReadLine();
    }

    public static void LookupAccounts_Firstname (Accounts LoggedInAccount) {
        Console.WriteLine("Please input an First for the user you want to search for:");
        string userInput = Console.ReadLine();
        if (userInput is not null && userInput != "") {
            AccountLogic.SearchForUser_ByFirstName(userInput);
        } else {
            Console.WriteLine("Invalid input, please use an firstname for searching");
        }
        Console.WriteLine("Press enter to continue.");
        Console.ReadLine();
    }

    public static void LookupAccounts_Lastname (Accounts LoggedInAccount) {
        Console.WriteLine("Please input an Lastname for the user you want to search for:");
        string userInput = Console.ReadLine();
        if (userInput is not null && userInput != "") {
            AccountLogic.SearchForUser_ByLastName(userInput);
        } else {
            Console.WriteLine("Invalid input, please use an lastname for searching");
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

    public static void DeleteAccount(Accounts LoggedInAccount) {
        Accounts selectedAccount = null;
        bool WantsToExit = false;
        do {
            AccountLogic.PrintAccounts(LoggedInAccount);
            Console.WriteLine("================================================================");
            Console.WriteLine("Please select an account to delete (enter Q to exit):");
            string userInput = Console.ReadLine();
            if (userInput.ToLower() == "q") {
                WantsToExit = true;
            } else {
                selectedAccount = AccountLogic.GetSelectedAccount(LoggedInAccount, userInput);
            }
        } while (selectedAccount == null & !WantsToExit);

        if (!WantsToExit & selectedAccount is not null) {
            while (true) {
                Console.WriteLine("================================================================");
                Console.WriteLine("Are you sure, you want to delete this account (Y = yes, N = no)");
                string userInput = Console.ReadLine();
                if (userInput.ToLower() == "y") {
                    AccountLogic.RemoveAccountFromSystem(selectedAccount.ID);
                } else if (userInput.ToLower() == "n") {
                    break;
                } else {
                    Console.WriteLine("Invalid response, please enter a valid awnser");
                }
            }
        }
    }
}