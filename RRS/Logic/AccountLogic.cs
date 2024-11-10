
public static class AccountLogic {
    public static void PrintAccounts(Accounts LoggedInAccount) {
        foreach (Accounts accounts in Database.SelectAccount()) {
            if (accounts.ID != LoggedInAccount.ID) {
                Console.WriteLine($"{accounts.ID} - {accounts.FirstName} {accounts.LastName} - Account level: {accounts.AccountLevel} - {Database.SelectAccountLevel(accounts.AccountLevel).Name}");
            }
        } 
    }
    

    public static Accounts GetSelectedAccount(Accounts LoggedInAccount, string input) {
        if (int.TryParse(input, out int output)) {
            foreach (Accounts accounts in Database.SelectAccount()) {
                if (accounts.ID == output && accounts.ID != LoggedInAccount.ID) {
                    return accounts;
                }
            }
        }
        return null;
    }

    public static void PrintAccountLevels() {
        foreach (AccountLevel accountlevel in Database.SelectAccountLevel()) {
            Console.WriteLine($"{accountlevel.ID} - {accountlevel.Name}");
        }
    }

    public static void PrintAccounts() {
        foreach (Accounts accounts in Database.SelectAccount()) {
            Console.WriteLine($"{accounts.ID} - {accounts.FirstName} {accounts.LastName}");
        }
    }

    public static AccountLevel GetSelectedAccountlevel(string input) {
        if (int.TryParse(input, out int output)) {
            foreach (AccountLevel accountslevel in Database.SelectAccountLevel()) {
                if (accountslevel.ID == output) {
                    return accountslevel;
                }
            }
        }
        return null;
    }

    public static void ChangeAccountLevel(Accounts selectedAccount, AccountLevel selectedAccountLevel, Accounts LoggedInAccount) {
        if (Database.UpdateAccountLevelForAccount(selectedAccount, selectedAccountLevel, LoggedInAccount)) {
            Console.WriteLine($"Accountlevel for the account of {selectedAccount.FirstName} {selectedAccount.LastName} to level {selectedAccountLevel.ID} - {selectedAccountLevel.Name}, has succeeded");
        } else {
            Console.WriteLine($"There was an error trying to change the accountlevel for the account of {selectedAccount.FirstName} {selectedAccount.LastName}, please try it again later");
        }
    }

    public static bool CanDisplay_CreateAdminAccounts(Accounts LoggedInAccount) {
        AccountLevel LoggedInAccountsAccountLevel = Database.SelectAccountLevel(LoggedInAccount.AccountLevel);
        return LoggedInAccountsAccountLevel.CanCreateAdmins;
    }

    public static bool CanDisplay_CancelReservations(Accounts LoggedInAccount) {
        AccountLevel LoggedInAccountsAccountLevel = Database.SelectAccountLevel(LoggedInAccount.AccountLevel);
        return LoggedInAccountsAccountLevel.CanCancelReservations;
    }

    public static bool CanDisplay_ChangeReservations(Accounts LoggedInAccount) {
        AccountLevel LoggedInAccountsAccountLevel = Database.SelectAccountLevel(LoggedInAccount.AccountLevel);
        return LoggedInAccountsAccountLevel.CanChangeReservation;
    }

    public static bool CanDisplay_ChangeTimeSlots(Accounts LoggedInAccount) {
        AccountLevel LoggedInAccountsAccountLevel = Database.SelectAccountLevel(LoggedInAccount.AccountLevel);
        return LoggedInAccountsAccountLevel.CanChangeTimeSlots;
    }

    public static void SearchForUser_ByID(string userInput) {
        if (int.TryParse(userInput, out int output)) {
            Accounts returnedAccount = Database.SelectAccount(output);
            if (returnedAccount is not null) {
                Console.WriteLine(returnedAccount.FancyToString_WithAccountLevelName());
            } else {
                Console.WriteLine("No results, There were no accounts found with this ID.");
            }
        } else {
            Console.WriteLine("Invalid input, An ID only consists of number. Please try it again with a valid number");
        }
    }

    public static void SearchForUser_ByFirstName(string userInput) {
        List<Accounts> returnedAccounts = Database.SelectAccount(userInput, "firstname", true);
        if (returnedAccounts is not null) {
            foreach (Accounts returnedAccount in returnedAccounts) {
                Console.WriteLine("---------------------------------------------------------------");
                Console.WriteLine(returnedAccount.FancyToString_WithAccountLevelName_NoBorder());
                Console.WriteLine("---------------------------------------------------------------");
            }
        } else {
            Console.WriteLine("No results, There were no accounts found with this firstname.");
        }
    }

    public static void SearchForUser_ByLastName(string userInput) {
        List<Accounts> returnedAccounts = Database.SelectAccount(userInput, "lastname", true);
        if (returnedAccounts is not null) {
            foreach (Accounts returnedAccount in returnedAccounts) {
                Console.WriteLine("---------------------------------------------------------------");
                Console.WriteLine(returnedAccount.FancyToString_WithAccountLevelName_NoBorder());
                Console.WriteLine("---------------------------------------------------------------");
            }
        } else {
            Console.WriteLine("No results, There were no accounts found with this lastname.");
        }
    }

    public static void SearchForUser_ByEmail(string userInput) {
        Accounts returnedAccount = Database.SelectAccount(userInput, "email");
        if (returnedAccount is not null) {
            Console.WriteLine(returnedAccount.FancyToString_WithAccountLevelName());
        } else {
            Console.WriteLine("No results, There were no accounts found with this e-mail.");
        }
    }

    public static void RemoveAccountFromSystem(int AccountLevelID) {
        string resultFromAction = Database.DeleteAccount(AccountLevelID);
        if (resultFromAction != "invalid input") {
            Console.WriteLine(resultFromAction);
        } else {
            Console.WriteLine("There was an error while trying to delete the account, please try it again later");
        }
    }

}
