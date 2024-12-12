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

    public static int RequestValidInt(string request, int minValue, int maxValue) {
        while (true) {
            Display.PrintText(request + ":");
            string input = Console.ReadLine();

            if (input is not null && input != "" && input.Count() >= 1) {
                if (int.TryParse(input, out int output)) {
                    if (output >= minValue && output <= maxValue) {
                        return output;
                    } else {
                        Display.PrintText($"Invalid input, please input a valid {request} between {minValue} and the {maxValue}");
                    }
                }
            }
            Display.PrintText($"Invalid input, please input a valid {request}");
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

    public static double RequestValidDouble(string request) {
        while (true) {
            Display.PrintText(request + ":");
            string input = Console.ReadLine();

            if (input is not null && input != "" && input.Count() >= 1) {
                if (double.TryParse(input, out double output)) {
                    return output;
                }
            }
            Display.PrintText($"Invalid input, please input a valid {request}");
            Thread.Sleep(1500);
        }
    }

    public static double RequestValidDouble(string request, double minValue, double maxValue) {
        while (true) {
            Display.PrintText(request + ":");
            string input = Console.ReadLine();

            if (input is not null && input != "" && input.Count() >= 1) {
                if (double.TryParse(input, out double output)) {
                    if (output >= minValue && output <= maxValue) {
                        return output;
                    } else {
                        Display.PrintText($"Invalid input, please input a valid {request} between {minValue} and the {maxValue}");
                    }
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

    public static string OptionSelector(string Header, List<string> options, bool returnStringValue) {
        string selectedOption = null;
        int pos = 0;

        do {
            Console.Clear();
            Console.WriteLine(Header);
            
            for (int y = 0; y != options.Count(); y++) {
                string row = "";
                if (pos == y) {
                    row += "\x1b[44m>";
                } else {
                    row += " ";
                }

                row += $" {options[y]}\x1b[49m";
                Console.WriteLine(row);
            }

            var input = Console.ReadKey();
            switch (input.Key) {
                case ConsoleKey.UpArrow:
                    if (pos <= 0) {
                        pos = options.Count() - 1;
                    } else {
                        pos -= 1;
                    }
                    break;
                case ConsoleKey.DownArrow:
                    if (pos >= (options.Count() - 1)) {
                        pos = 0;
                    } else {
                        pos += 1;
                    }
                    break;
                case ConsoleKey.Enter:
                    selectedOption = options[pos];
                    break;
            }

        } while (selectedOption is null);
        return selectedOption;
    }

    public static string OptionSelector(string Header, string Footer, List<string> options, bool returnStringValue) {
        string selectedOption = null;
        int pos = 0;

        do {
            Console.Clear();
            Console.WriteLine(Header);
            
            for (int y = 0; y != options.Count(); y++) {
                string row = "";
                if (pos == y) {
                    row += "\x1b[44m>";
                } else {
                    row += " ";
                }

                row += $" {options[y]}\x1b[49m";
                Console.WriteLine(row);
            }

            Console.WriteLine(Footer);

            var input = Console.ReadKey();
            switch (input.Key) {
                case ConsoleKey.UpArrow:
                    if (pos <= 0) {
                        pos = options.Count() - 1;
                    } else {
                        pos -= 1;
                    }
                    break;
                case ConsoleKey.DownArrow:
                    if (pos >= (options.Count() - 1)) {
                        pos = 0;
                    } else {
                        pos += 1;
                    }
                    break;
                case ConsoleKey.Enter:
                    selectedOption = options[pos];
                    break;
            }
        } while (selectedOption is null);
        return selectedOption;
    }

    public static int OptionSelector(string Header, List<string> options) {
        int selectedOption = 999999999;
        int pos = 0;

        do {
            Console.Clear();
            Console.WriteLine(Header);
            
            for (int y = 0; y != options.Count(); y++) {
                string row = "";
                if (pos == y) {
                    row += "\x1b[44m>";
                } else {
                    row += " ";
                }

                row += $" {options[y]}\x1b[49m";
                Console.WriteLine(row);
            }

            var input = Console.ReadKey();
            switch (input.Key) {
                case ConsoleKey.UpArrow:
                    if (pos <= 0) {
                        pos = options.Count() - 1;
                    } else {
                        pos -= 1;
                    }
                    break;
                case ConsoleKey.DownArrow:
                    if (pos >= (options.Count() - 1)) {
                        pos = 0;
                    } else {
                        pos += 1;
                    }
                    break;
                case ConsoleKey.Enter:
                    selectedOption = pos;
                    break;
            }

        } while (selectedOption == 999999999);
        return selectedOption;
    }

    public static int OptionSelector(string Header, string Footer, List<string> options) {
        int selectedOption = 999999999;
        int pos = 0;

        do {
            Console.Clear();
            Console.WriteLine(Header);
            
            for (int y = 0; y != options.Count(); y++) {
                string row = "";
                if (pos == y) {
                    row += "\x1b[44m>";
                } else {
                    row += " ";
                }

                row += $" {options[y]}\x1b[49m";
                Console.WriteLine(row);
            }

            Console.WriteLine(Footer);

            var input = Console.ReadKey();
            switch (input.Key) {
                case ConsoleKey.UpArrow:
                    if (pos <= 0) {
                        pos = options.Count() - 1;
                    } else {
                        pos -= 1;
                    }
                    break;
                case ConsoleKey.DownArrow:
                    if (pos >= (options.Count() - 1)) {
                        pos = 0;
                    } else {
                        pos += 1;
                    }
                    break;
                case ConsoleKey.Enter:
                    selectedOption = pos;
                    break;
            }
        } while (selectedOption == 999999999);
        return selectedOption;
    }

    public static Dictionary<int, bool> CheckBoxSelector(List<string> options_string, List<bool> options_bool) {
        bool done = false;
        int pos = 0;

        do {
            Console.Clear();
            for (int y = 0; y != options_string.Count(); y++) {
                string row = "";
                if (pos == y) {
                    row += "\x1b[44m>";
                } else {
                    row += " ";
                }

                if (options_bool[y]) {
                    row += " [X] ";
                } else {
                    row += " [ ] ";
                }

                row += $" {options_string[y]}\x1b[49m";
                Console.WriteLine(row);
            }
            var input = Console.ReadKey();
            switch (input.Key) {
                case ConsoleKey.UpArrow:
                    if (pos <= 0) {
                        pos = options_string.Count() - 1;
                    } else {
                        pos -= 1;
                    }
                    break;
                case ConsoleKey.DownArrow:
                    if (pos >= (options_string.Count() - 1)) {
                        pos = 0;
                    } else {
                        pos += 1;
                    }
                    break;
                case ConsoleKey.Spacebar:
                    if (options_bool[pos]) {
                        options_bool[pos] = false;
                    } else {
                        options_bool[pos] = true;
                    }
                    break;
                case ConsoleKey.Enter:
                    done = true;
                    break;
            }
        } while (!done);

        int curr_pos = 0;
        Dictionary<int, bool> returnDict = new ();
        foreach (string option in options_string) {
            returnDict.Add(curr_pos, options_bool[curr_pos]);
            curr_pos++;
        }
        return returnDict;
    }

    public static Dictionary<string, bool> CheckBoxSelector(List<string> options_string) {
        List<bool> options_bool = new ();
        for (int i = 0; i == options_string.Count() - 1; i++) {
            options_bool.Add(false);
        }
        //DEBUG
        // Console.WriteLine(options_string.Count());
        // Console.WriteLine(options_bool.Count());


        bool done = false;
        int pos = 0;

        do {
            Console.Clear();
            for (int y = 0; y != options_string.Count(); y++) {
                string row = "";
                if (pos == y) {
                    row += "\x1b[44m>";
                } else {
                    row += " ";
                }

                if (options_bool[y]) {
                    row += " [X] ";
                } else {
                    row += " [ ] ";
                }

                row += $" {options_string[y]}\x1b[49m";
                Console.WriteLine(row);
            }
            var input = Console.ReadKey();
            switch (input.Key) {
                case ConsoleKey.UpArrow:
                    if (pos <= 0) {
                        pos = options_string.Count() - 1;
                    } else {
                        pos -= 1;
                    }
                    break;
                case ConsoleKey.DownArrow:
                    if (pos >= (options_string.Count() - 1)) {
                        pos = 0;
                    } else {
                        pos += 1;
                    }
                    break;
                case ConsoleKey.Spacebar:
                    if (options_bool[pos]) {
                        options_bool[pos] = false;
                    } else {
                        options_bool[pos] = true;
                    }
                    break;
                case ConsoleKey.Enter:
                    done = true;
                    break;
            }
        } while (!done);

        int curr_pos = 0;
        Dictionary<string, bool> returnDict = new ();
        foreach (string option in options_string) {
            returnDict.Add(option, options_bool[curr_pos]);
            curr_pos++;
        }
        return returnDict;
    }

    public static Dictionary<string, bool> CheckBoxSelector(string Header, List<string> options_string) {
        List<bool> options_bool = new ();
        for (int i = 0; i != options_string.Count(); i++) {
            options_bool.Add(false);
        }

        bool done = false;
        int pos = 0;

        do {
            Console.Clear();
            Console.WriteLine(Header);
            for (int y = 0; y != options_string.Count(); y++) {
                string row = "";
                if (pos == y) {
                    row += "\x1b[44m>";
                } else {
                    row += " ";
                }

                if (options_bool[y]) {
                    row += " [X] ";
                } else {
                    row += " [ ] ";
                }

                row += $" {options_string[y]}\x1b[49m";
                Console.WriteLine(row);
            }
            var input = Console.ReadKey();
            switch (input.Key) {
                case ConsoleKey.UpArrow:
                    if (pos <= 0) {
                        pos = options_string.Count() - 1;
                    } else {
                        pos -= 1;
                    }
                    break;
                case ConsoleKey.DownArrow:
                    if (pos >= (options_string.Count() - 1)) {
                        pos = 0;
                    } else {
                        pos += 1;
                    }
                    break;
                case ConsoleKey.Spacebar:
                    if (options_bool[pos]) {
                        options_bool[pos] = false;
                    } else {
                        options_bool[pos] = true;
                    }
                    break;
                case ConsoleKey.Enter:
                    done = true;
                    break;
            }
        } while (!done);

        int curr_pos = 0;
        Dictionary<string, bool> returnDict = new ();
        foreach (string option in options_string) {
            returnDict.Add(option, options_bool[curr_pos]);
            curr_pos++;
        }
        return returnDict;
    }

    public static string DateSelector(string Header) {
        string str_date = DateTime.Now.ToString("dd/MM/yyyy");
        string selectedDate = null;
        string errorMessage = null;
        int posY = 0;
        int posX = 0;

        do {
            Console.Clear();
            Console.WriteLine(Header);

            if (errorMessage is not null) {
                Console.WriteLine(errorMessage);
                errorMessage = null;
            }

            for (int y = 0; y != 3; y++) {
                string row = "";
                if (y == 0) {
                    for (int x = 0; x != str_date.Length; x++) {
                        if (x == 2 | x == 5) {
                            row += " ";
                        } else {
                            if (posX == x & posY == y) {
                                row += "\x1b[44m";
                            }
                            row += "↑\x1b[49m";
                        }
                    }
                } else if (y == 1) {
                    row = str_date;
                } else {
                    for (int x = 0; x != str_date.Length; x++) {
                        if (x == 2 | x == 5) {
                            row += " ";
                        } else {
                            if (posX == x & posY == y) {
                                row += "\x1b[44m";
                            }
                            row += "↓\x1b[49m";
                        }
                    }
                }
                Console.WriteLine(row);
            }

            var input = Console.ReadKey();
            switch (input.Key) {
                case ConsoleKey.UpArrow:
                    if (posY == 2) {
                        posY = 0;
                    } else {
                        posY = 2;
                    }
                    break;
                case ConsoleKey.DownArrow:
                    if (posY == 0) {
                        posY = 2;
                    } else {
                        posY = 0;
                    }
                    break;
                case ConsoleKey.LeftArrow:
                    posX--;
                    if (posX == 2 | posX == 5) {
                        posX--;
                    }
                    if (posX < 0) {
                        posX = 9;
                    }
                    break;
                case ConsoleKey.RightArrow:
                    posX++;
                    if (posX == 2 | posX == 5) {
                        posX++;
                    }
                    if (posX > 9) {
                        posX = 0;
                    }
                    break;
                case ConsoleKey.Spacebar:
                    try {
                        Dictionary<int, string> dict_posDayMonthYear = new Dictionary<int, string> {
                            {0, "D"}, {1, "D"},
                            {2, "S"},
                            {3, "M"}, {4, "M"},
                            {5, "S"},
                            {6, "Y"}, {7, "Y"}, {8, "Y"}, {9, "Y"}
                        };

                        DateTime date = DateTime.ParseExact(str_date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        switch (dict_posDayMonthYear[posX]) {
                            case "D":
                                if (posY == 0) {
                                    if (posX == 0) {
                                        date = date.AddDays(10);
                                    } else {
                                        date = date.AddDays(1);
                                    }
                                } else {
                                    if (posX == 0) {
                                        date = date.AddDays(-10);
                                    } else {
                                        date = date.AddDays(-1);
                                    }
                                }
                                break;
                            case "M":
                                if (posY == 0) {
                                    if (posX == 3) {
                                        date = date.AddMonths(10);
                                    } else {
                                        date = date.AddMonths(1);
                                    }
                                } else {
                                    if (posX == 3) {
                                        date = date.AddMonths(-10);
                                    } else {
                                        date = date.AddMonths(-1);
                                    }
                                }
                                break;
                            case "Y":
                                if (posY == 0) {
                                    if (posX == 6) {
                                        date = date.AddYears(1000);
                                    } else if (posX == 7) {
                                        date = date.AddYears(100);
                                    } else if (posX == 8) {
                                        date = date.AddYears(10);
                                    } else {
                                        date = date.AddYears(1);
                                    }
                                } else {
                                    if (posX == 6) {
                                        date = date.AddYears(-1000);
                                    } else if (posX == 7) {
                                        date = date.AddYears(-100);
                                    } else if (posX == 8) {
                                        date = date.AddYears(-10);
                                    } else {
                                        date = date.AddYears(-1);
                                    }
                                }
                                break;
                        }

                        if (date < DateTime.Now) {
                            errorMessage = "You can't select a date in the past, please select a date in the future";
                            date = DateTime.Now;
                        }
                        str_date = date.ToString("dd/MM/yyyy");
                    }
                    catch (System.Exception)
                    {
                        errorMessage = "You tried to select an invalid date, please select a date";
                    }
                    break;
                case ConsoleKey.C:
                    str_date = DateTime.Now.ToString("dd/MM/yyyy");
                    break;
                case ConsoleKey.Enter:
                    selectedDate = str_date;
                    break;
            }
        } while (selectedDate is null);

        return selectedDate;
    }

    public static TimeOnly TimeSelector(string Header, string startTime="00:00", string minValue="00:01", string maxValue="24:00") {
        TimeOnly time = TimeOnly.FromDateTime(DateTime.ParseExact(startTime, "HH:mm", CultureInfo.InvariantCulture));
        bool Done = false;
        string errorMessage = null;
        int posY = 0;
        int posX = 0;

        do {
            Console.Clear();
            Console.WriteLine(Header);

            if (errorMessage is not null) {
                Console.WriteLine(errorMessage);
                errorMessage = null;
            }

            for (int y = 0; y != 3; y++) {
                string row = "";
                if (y == 0) {
                    for (int x = 0; x != 5; x++) {
                        if (x == 2) {
                            row += " ";
                        } else {
                            if (posX == x & posY == y) {
                                row += "\x1b[44m";
                            }
                            row += "↑\x1b[49m";
                        }
                    }
                } else if (y == 1) {
                    row = time.ToString("HH:mm");
                } else {
                    for (int x = 0; x != 5; x++) {
                        if (x == 2) {
                            row += " ";
                        } else {
                            if (posX == x & posY == y) {
                                row += "\x1b[44m";
                            }
                            row += "↓\x1b[49m";
                        }
                    }
                }
                Console.WriteLine(row);
            }

            var input = Console.ReadKey();
            switch (input.Key) {
                case ConsoleKey.UpArrow:
                    if (posY == 2) {
                        posY = 0;
                    } else {
                        posY = 2;
                    }
                    break;
                case ConsoleKey.DownArrow:
                    if (posY == 0) {
                        posY = 2;
                    } else {
                        posY = 0;
                    }
                    break;
                case ConsoleKey.LeftArrow:
                    posX--;
                    if (posX == 2) {
                        posX--;
                    }
                    if (posX < 0) {
                        posX = 5;
                    }
                    break;
                case ConsoleKey.RightArrow:
                    posX++;
                    if (posX == 2) {
                        posX++;
                    }
                    if (posX > 5) {
                        posX = 0;
                    }
                    break;
                case ConsoleKey.Spacebar:
                    Dictionary<int, string> dict_posHourMinute = new Dictionary<int, string> {
                            {0, "H"}, {1, "H"},
                            {2, "S"},
                            {3, "M"}, {4, "M"}
                    };

                    if (dict_posHourMinute[posX] == "H") {
                            if (posY == 0) {
                                if (posX == 0) {
                                    time = time.AddHours(10);
                                } else {
                                    time = time.AddHours(1);
                                }
                            } else {
                                if (posX == 0) {
                                    time = time.AddHours(-10);
                                } else {
                                    time = time.AddHours(-1);
                                }
                            }
                    } else if (dict_posHourMinute[posX] == "M") {
                        if (posY == 0) {
                                if (posX == 3) {
                                    time = time.AddMinutes(10);
                                } else {
                                    time = time.AddMinutes(1);
                                }
                            } else {
                                if (posX == 3) {
                                    time = time.AddMinutes(-10);
                                } else {
                                    time = time.AddMinutes(-1);
                                }
                            }
                    }
                    break;
                case ConsoleKey.C:
                    time = TimeOnly.FromDateTime(DateTime.ParseExact(startTime, "HH:mm", CultureInfo.InvariantCulture));
                    break;
                case ConsoleKey.Enter:
                    Done = true;
                    break;
            }

        } while (!Done);

        return time;
    }

    public static int IntSelector(string header, int minValue=1, int maxValue=5, int startInAmount=999999999, string icon=null) {
        bool done = false;
        int returnValue = 0;
        if (startInAmount == 999999999) {
            startInAmount = minValue;
        }
        int selectedAmount = startInAmount;
        int pos = 0;
        
        do {
            Console.Clear();
            Console.WriteLine(header);
            string row = "";

            if (icon is not null) {
                string icons = "";
                for (int x = 0; x != selectedAmount; x++) {
                    icons += icon;
                }
                Console.WriteLine(icons);
                
                row += "   ";
            }

            for (int x = 0; x != 3; x++) {
                if (x == pos) {
                    row += "\x1b[44m";
                }

                if (x == 0) {
                    row += "<\x1b[49m";
                } else if (x == 2) {
                    row += ">\x1b[49m";
                } else {
                    row += $" {selectedAmount} ";
                }
            }
            Console.WriteLine(row);

            var input = Console.ReadKey();
            switch (input.Key) {
                case ConsoleKey.LeftArrow:
                    if (pos == 2) {
                        pos = 0;
                    } else {
                        pos = 2;
                    }
                    break;
                case ConsoleKey.RightArrow:
                    if (pos == 2) {
                        pos = 0;
                    } else {
                        pos = 2;
                    }
                    break;
                case ConsoleKey.Spacebar:
                    if (pos == 0) {
                        if (selectedAmount > minValue) {
                            selectedAmount--;
                        }
                    } else {
                        if (selectedAmount < maxValue) {
                            selectedAmount++;
                        }
                    }
                    break;
                case ConsoleKey.Enter:
                    done = true;
                    returnValue = selectedAmount;
                    break;
            }
        } while (!done);

        return returnValue;
    }
}