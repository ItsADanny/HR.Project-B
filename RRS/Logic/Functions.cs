using System.Security.Cryptography;
using System.Text;

public static class Functions {

    public static string PasswordReadLine() {
        string pass = "";
        ConsoleKeyInfo key;

        do {
            key = Console.ReadKey(true);

            // Backspace Should Not Work
            if (!char.IsControl(key.KeyChar)) {
                pass += key.KeyChar;
                Console.Write("*");
            } else {
                if (key.Key == ConsoleKey.Backspace && pass.Length > 0) {
                    pass = pass.Remove(pass.Length - 1);
                    Console.Write("\b \b");
                }
            }
        } while (key.Key != ConsoleKey.Enter);
        
        return HashPassword(pass);
    }

    public static string HashPassword(string input) {
        if (input is not null) {
            using (SHA512 shaM = new SHA512Managed()) {
                return Convert.ToBase64String(shaM.ComputeHash(Encoding.UTF8.GetBytes(input)));
            }
        } else {
            return null;
        }
    }

    public static void VitalSystemsCheck() {
        //Check to see if our database is valid and has the required tables
        Database.DBCheck();
    }

    public static Accounts Login(string email, string password) {
        return Database.SelectAccount(email, password);
    }

    public static void DisplayEnvironment(Accounts account) {
        account.ToString();
    }
}