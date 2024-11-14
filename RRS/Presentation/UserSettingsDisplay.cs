using System;

namespace RRS.Presentation;

public class UserSettingsDisplay
{
public static void UserSettingsMenu(Accounts LoggedInAccount){
    Console.Clear();
    Console.WriteLine("You have selected: User settings");
    Console.WriteLine("1. Update Profile Information");
    Console.WriteLine("2. Change Password");
    Console.WriteLine("3. Language Settings");
    Console.WriteLine("4. Delete Account");
    Console.Write("Enter your choice (1-4): ");
    
    int choice = Convert.ToInt32(Console.ReadLine());
    switch (choice){
        case 1:
            UpdateProfile();
            break;
        case 2:
            AccountChangePasswordDisplay(LoggedInAccount);
            break;
    
        case 3:
            // Languagesettings();
            break;
        case 4:
            DeleteAccountDisplay(LoggedInAccount);
            break;
        default:
            Console.WriteLine("Invalid choice. Please try again.");
            break;}}
    
    public static void UpdateProfile(Accounts LoggedInAccount){
        Console.Clear();
        Console.WriteLine("Update Profile Information");
        Console.WriteLine("1. Change name");
        Console.WriteLine("2. Update Phone Number");
        Console.WriteLine("3. Exit");
        
        Console.Write("Choose an option (1-4): ");
        
        string choice = Console.ReadLine();
        
        switch (choice)
        {
            case "1":
                Console.WriteLine("You want to change your Name? y to confirm n to exit ");
                string userinput = Console.ReadLine().ToLower();
                if (userinput == "y"){
                    Console.WriteLine("Enter your new first name");
                    string newfirstname = Console.ReadLine();
                    Console.WriteLine("Enter your new last name");
                    string newlastname = Console.ReadLine();
                    Database.UpdateFirstNameForAccount(Accounts LoggedInAccount, newfirstname)
                    Database.UpdateLastNameForAccount(Accounts LoggedInAccount, newlastname)
                    Console.WriteLine("Name updated");}

                
                break;

            case "2":
                System.Console.WriteLine("Enter your new phonenumber: ");
                string newphonenumber = Console.ReadLine();
                while (!newphonenumber.All(char.IsDigit)){
                System.Console.WriteLine("Invalid phonenumber. Please enter a valid phonenumber.");
                }
                Database.UpdatePhoneNumberForAccount(Accounts LoggedInAccount, newphonenumber);
                System.Console.WriteLine("Phonenumber updated");

                break;

           

            case "3":
                Console.WriteLine("Exitting usersettings");
                
                break;

            default:
                Console.WriteLine("Invalid choice. Please try again.");
                break;
        }
    }
    public AccountChangePasswordDisplay(){
    System.Console.WriteLine("You have selected: Change password");
    System.Console.WriteLine("Please enter your current password:");
    ChangePassword.NewPassword(Accounts LoggedInAccount);

    public DeleteAccountDisplay(){
        System.Console.WriteLine("You have selected: Delete account");
        System.Console.WriteLine("Are you sure you want to delete your account? (y to delete n to cancel)");
        string userinput = Console.ReadLine().ToLower();
        if (userinput == "y"){
            Database.DeleteAccount();
    }
    if (userinput == "n"){
        System.Console.WriteLine("Account has not been deleted");
        UserSettingsMenu(Accounts LoggedInAccount);
    }
    else{
        System.Console.WriteLine("Invalid input try again");
        DeleteAccountDisplay();
    }


    }
}


