public class ChangePassword{
   public static void NewPassword(){
    System.Console.WriteLine("You have selected: Change password");
    System.Console.WriteLine("Please enter your current password:");
    string currentPassword = Functions.PasswordReadLine();
    Database.CheckAccountPassword(Database, currentPassword);// CheckAccountPAssword(Int Id, String input)

    


   }
}