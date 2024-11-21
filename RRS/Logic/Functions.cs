using System.Security.Cryptography;
using System.Globalization;
using System.Text;
using System.Linq;

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

    public static string PasswordReadLine_WithValidCheck() {
        string pass = "";
        ConsoleKeyInfo key;

        while (true) {
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
            
            //This checks to see if the password atleast contains a integer/digit
            if (pass.Any(char.IsDigit)) {
                //This checks to see if the password atleast contains a special symbol
                if (pass.Any(c => !char.IsLetterOrDigit(c))) {
                    break;
                } else {
                    Display.PrintText("Password does not contain a special symbol, please use atleast 1 special symbol");
                }
            } else {
                Display.PrintText("Password does not contain a number, please use atleast 1 number");
            }
        }

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

    public static bool IsAccountAdmin(Accounts account) {
        if (Database.SelectAccountLevel(account.AccountLevel).IsAnAdmin) {
            return true;
        }
        return false;
    }

    public static string RequestValidString(string request) {
        while (true) {
            Display.PrintText(request + ":");
            string input = Console.ReadLine();

            if (input is not null && input != "" && input.Count() >= 1) {
                return input;
            }
            Display.PrintText($"Invalid input, please input a valid {request}");
            Thread.Sleep(1500);
        }
    }

    public static string RequestValidString() {
        while (true) {
            Display.PrintText("Firstname:");
            string input = Console.ReadLine();

            if (input is not null && input != "" && input.Count() >= 1) {
                return input;
            }
            Display.PrintText($"Invalid input, please input a valid firstname");
            Thread.Sleep(1500);
        }
    }

    public static int RequestValidInt(string request) {
        while (true) {
            Display.PrintText(request + ":");
            string input = Console.ReadLine();

            if (input is not null && input != "" && input.Count() >= 1) {
                if (int.TryParse(input, out int output)) {
                    return output;
                }
            }
            Display.PrintText($"Invalid input, please input a valid {request}");
            Thread.Sleep(1500);
        }
    }

    public static string RequestValidPhonenumber(string request) {
        while (true) {
            Display.PrintText(request + ":");
            string input = Console.ReadLine();

            if (input is not null && input != "" && input.Count() >= 1 && input[0] == '0' && input[1] == '6') {
                // Console.WriteLine("KOM IK HIER");
                if (int.TryParse(input, out int output)) {
                    return input;
                }
            }
            Display.PrintText($"Invalid input, please input a valid {request}");
            Thread.Sleep(1500);
        }
    }

    public static string RequestValidEmail() {
        while (true) {
            Display.PrintText("E-mail:");
            string input = Console.ReadLine();

            bool containsAt = false;
            bool constainsDot = false;

            foreach (char letter in input) {
                if (letter == '@') {
                    containsAt = true;
                }
                if (letter == '.') {
                    constainsDot = true;
                }
            }

            if (input is not null && input != "" && input.Count() >= 1 && containsAt && constainsDot) {
                if (!Database.DoesEmailAlreadyExist(input)) {
                    return input;
                } else {
                    Display.PrintText("E-mail is already in use, please enter a different e-mail");
                }
            } else {
                Display.PrintText("Invalid input, please input a valid E-mail");
            }
            Thread.Sleep(1500);
        }
    }

    public static string RequestValidEmail(string message) {
        while (true) {
            Display.PrintText(message + ":");
            string input = Console.ReadLine();

            bool containsAt = false;
            bool constainsDot = false;

            foreach (char letter in input) {
                if (letter == '@') {
                    containsAt = true;
                }
                if (letter == '.') {
                    constainsDot = true;
                }
            }

            if (input is not null && input != "" && input.Count() >= 1 && containsAt && constainsDot) {
                return input;
            }
            Display.PrintText($"Invalid input, please input a valid E-mail");
            Thread.Sleep(1500);
        }
    }

    public static string RequestValidDate() {
        while (true) {
            Display.PrintText("Date (dd/mm/yyyy) (example: 20/11/2024):");
            string input = Console.ReadLine();

            if (DateTime.TryParseExact(input, "dd/MM/yyyy", new CultureInfo("fr-FR"), DateTimeStyles.None, out DateTime output)) {
                return input;
            }
            Display.PrintText($"Invalid input, please input a valid Date");
            Thread.Sleep(1500);
        }
    }

    public static string RequestValidTime(string request) {
        while (true) {
            Display.PrintText(request + ":");
            string input = Console.ReadLine();

            if (DateTime.TryParseExact(input, "hh:mm", new CultureInfo("en-US"), DateTimeStyles.None, out DateTime output)) {
                return input;
            }
            Display.PrintText($"Invalid input, please input a valid {request}");
            Thread.Sleep(1500);
        }
    }

    public static string RequestValidTime24(string request) {
        while (true) {
            Display.PrintText(request + ":");
            string input = Console.ReadLine();

            if (DateTime.TryParseExact(input, "HH:mm", System.Globalization.CultureInfo.CurrentCulture, System.Globalization.DateTimeStyles.None, out DateTime output)) {
                return input;
            }
            Display.PrintText($"Invalid input, please input a valid {request}");
            Thread.Sleep(1500);
        }
    }
    
}