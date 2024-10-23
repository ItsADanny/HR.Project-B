public static class ProgramDisplay {
    private static int SelectedRestaurant = 0;

    public static void Display() {

        int attempts = 4;
        while (true) {
            // Console.Clear();
            Console.WriteLine("[TEMP] MATCHMAKING RESTAURANT [TEMP]");
            Console.WriteLine("====================================\n");
            Console.WriteLine("Login");
            Console.WriteLine("------------------------------------");
            Console.WriteLine("E-mail:");
            string input_email = Console.ReadLine();
            Console.WriteLine("Password:");
            string input_password = Functions.PasswordReadLine();

            Accounts account = Functions.Login(input_email, input_password);

            if (account is not null) {
                attempts = 4;
                Functions.DisplayEnvironment(account);
            } else {
                attempts--;
                Console.WriteLine($"Incorrect login: you have {attempts} left.");
            }

            if (attempts == 0) {
                Console.WriteLine("All attempts have been used, Program shutting down.");
                break;
            }
        }
        

    }

}