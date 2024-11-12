public static class TimeSlotDisplay {
    public static void Menu(Accounts LoggedInAccount) {
        bool canChangeTimeSlots = AccountLogic.CanDisplay_ChangeTimeSlots(LoggedInAccount);
        while (true) {
            Console.Clear();
            PrintHeader();
            Console.WriteLine("\nOptions:");
            Console.WriteLine("1 - View timeslots");
            if (canChangeTimeSlots) {
                Console.WriteLine("2 - Create a new timeslot");
                Console.WriteLine("3 - Edit timeslot");
                Console.WriteLine("4 - Delete timeslot\n\n");
            } else {
                Console.WriteLine("\n");
            }
            Console.WriteLine("================================================================");
            Console.WriteLine("Please select an option (enter Q to exit):");

            string userInput = Console.ReadLine();
                if (userInput.ToLower() == "q") {
                    break;
                } else {
                    if (canChangeTimeSlots) {
                        switch (userInput) {
                            case "1":
                                ViewTimeSlots();
                                break;
                            case "2":

                                break;
                            case "3":

                                break;
                            case "4":

                                break;
                            default:
                                Console.WriteLine("Invalid input, please enter a valid choice");
                                Thread.Sleep(1500);
                                break;
                        }
                    } else {
                        switch (userInput) {
                            case "1":
                                ViewTimeSlots();
                                break;
                            default:
                                Console.WriteLine("Invalid input, please enter a valid choice");
                                Thread.Sleep(1500);
                                break;
                        }
                    }
                }
        }
    }

    private static void ViewTimeSlots() {
        PrintHeader();
        TimeSlotLogic.PrintTimeSlots("");
        Console.WriteLine("====================================================================\n\n");
        Console.WriteLine("Press ENTER to exit");
        Console.ReadLine();
    }

    private static void PrintHeader() {
        Console.WriteLine("====================================================================");
        Console.WriteLine("▗▄▄▄▖▗▄▄▄▖▗▖  ▗▖▗▄▄▄▖ ▗▄▄▖▗▖    ▗▄▖▗▄▄▄▖▗▄▄▖");
        Console.WriteLine("  █    █  ▐▛▚▞▜▌▐▌   ▐▌   ▐▌   ▐▌ ▐▌ █ ▐▌   ");
        Console.WriteLine("  █    █  ▐▌  ▐▌▐▛▀▀▘ ▝▀▚▖▐▌   ▐▌ ▐▌ █  ▝▀▚▖");
        Console.WriteLine("  █  ▗▄█▄▖▐▌  ▐▌▐▙▄▄▖▗▄▄▞▘▐▙▄▄▖▝▚▄▞▘ █ ▗▄▄▞▘");
        Console.WriteLine("====================================================================");
    }
}