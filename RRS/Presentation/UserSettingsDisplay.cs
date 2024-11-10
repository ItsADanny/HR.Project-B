using System;

namespace RRS.Presentation;

public class UserSettingsDisplay
{
public static void UserSettingsMenu(){
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
            // UpdateProfile();
            break;
        case 2:
            // ChangePassword();
            break;
    
        case 3:
            // Languagesettings();
            break;
        case 4:
            // DeleteAccount();
            break;
        default:
            Console.WriteLine("Invalid choice. Please try again.");
            break;}}
    
    public static void UpdateProfile(){
        Console.Clear();
        Console.WriteLine("Update Profile Information");
         Console.WriteLine("1. Change name");
        Console.WriteLine("2. Update email address");
        Console.WriteLine("3. Update Phone Number");
        Console.WriteLine("4. Cancel Update");
        
        Console.Write("Choose an option (1-4): ");
        
        string choice = Console.ReadLine();
        
        switch (choice)
        {
            case "1":
                // Console.WriteLine("You want to change your Name? y to confirm n to exit ");
                // string userinput = Console.ReadLine().ToLower();
                // if (userinput == "y"){
                    


                //     // Console.WriteLine("Enter your new name");
                //     // string newname = Console.ReadLine();
                      
                
                break;

            case "2":
                Console.WriteLine("You chose to update the email address.");
                break;

            case "3":
                Console.WriteLine("You chose to update the phone number.");
                break;

            case "4":
                Console.WriteLine("Update canceled.");
                break;

            default:
                Console.WriteLine("Invalid choice. Please try again.");
                break;
        }
    }

}


