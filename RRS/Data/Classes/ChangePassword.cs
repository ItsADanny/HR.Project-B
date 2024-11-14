public class ChangePassword{
   public static void NewPassword(Accounts LoggedInAccount){
    string currentPassword = Functions.PasswordReadLine();
    Database.CheckAccountPassword(LoggedInAccount.ID, currentPassword); // CheckAccountPassword(Int Id, String input)
    if (Database.CheckAccountPassword(LoggedInAccount.ID, currentPassword) == true){
      System.Console.WriteLine("Please enter your new password:");
      string newPassword = Functions.PasswordReadLine();
      System.Console.WriteLine("Please confirm your new password:");
      string confirmPassword = Functions.PasswordReadLine();
      if (newPassword == confirmPassword){
        Database.UpdatePasswordForAccount(LoggedInAccount, currentPassword, newPassword);
 

    }
    }}}
   //    
    


    


   