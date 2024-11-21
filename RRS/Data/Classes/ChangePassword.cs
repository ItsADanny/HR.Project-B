using RRS.Presentation;

public class ChangePassword{
   public static void NewPassword(string input, string newInput, Accounts LoggedInAccount){
    Database.CheckAccountPassword(LoggedInAccount.ID, input); // CheckAccountPassword(Int Id, String input)
      string confirmPassword = Functions.PasswordReadLine();
      if (Database.CheckAccountPassword(LoggedInAccount.ID, input) == true){
      System.Console.WriteLine("Please enter your new password:");
      if (newInput == confirmPassword){
        Database.UpdatePasswordForAccount(LoggedInAccount, input, newInput);

 

    }
    }}
    public static bool CorrectPassword(){
      if (Database.CheckAccountPassword(LoggedInAccount.ID, input)


    }}
   //    
    


    


   