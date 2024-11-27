using System;
using System.Data.Entity.Core.Common.EntitySql;

namespace RRS.Presentation;

public class AccountDisplay
{
private static int AccountId = 0;
public static void CreateAccountDisplay(){
    System.Console.WriteLine("Create an account:");
    System.Console.WriteLine("Enter your first name: ");
    string FirstName = Console.ReadLine();

    System.Console.WriteLine("Enter your last name: ");
    string LastName = Console.ReadLine();

    System.Console.WriteLine("Enter your Email: ");
    string Email = Console.ReadLine();
    while (!Email.Contains("@")){
        System.Console.WriteLine("Invalid Email. Please enter a valid email.");
        Email = Console.ReadLine();
    }
    System.Console.WriteLine("Enter your phonenumber: ");
    string PhoneNumber = Console.ReadLine();
    while (!PhoneNumber.All(char.IsDigit)){
        System.Console.WriteLine("Invalid phonenumber. Please enter a valid phonenumber.");
    }
    System.Console.WriteLine("Enter your password: ");
    string Password = Functions.PasswordReadLine();

    int Accountlevel = AccountLevelDisplay.ChooseAccountLevel();

    int ID = AccountId;

    
<<<<<<< Updated upstream
=======
    public static void AdminAccountMenu(Accounts LoggedInAccount) {
        bool CanCreateAdminAccounts = AccountLogic.CanDisplay("createAdmins", LoggedInAccount);
>>>>>>> Stashed changes




    
    Accounts NewAccount = new Accounts(Email, FirstName, LastName, PhoneNumber, Accountlevel);
    AccountId ++;
    System.Console.WriteLine("Account has been made!");
    System.Console.WriteLine($"{FirstName}\n{LastName}\n{PhoneNumber}\n{Email}");
    
}
public AccountChangePasswordDisplay(){
    System.Console.WriteLine("You have selected: Change password");
    System.Console.WriteLine("Please enter your current password:");
    ChangePassword.NewPassword(Accounts LoggedInAccount);
}
}

