public static class TimeSlotDisplay {
    public static void Menu(Accounts LoggedInAccount) {
        bool canChangeTimeSlots = AccountLogic.CanDisplay_ChangeTimeSlots(LoggedInAccount);
        while (true) {
            Console.Clear();
            PrintHeader();
            Console.WriteLine("\nOptions:");
            Console.WriteLine("1 - View timeslots");
            if (canChangeTimeSlots) {
                // Console.WriteLine("2 - Create a new timeslot");
                // Console.WriteLine("3 - Edit timeslot");
                // Console.WriteLine("4 - Delete timeslot\n\n");
                //BELOW IS TEMPORARY
                Console.WriteLine("2 - Create a new timeslot");
                Console.WriteLine("3 - Delete timeslot\n\n");
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
                            CreateNewTimeSlot();
                            break;
                        // THIS WILL BE ADDED ON A LATER DATE, ITS NOT SUPER IMPORTANT FOR NOW
                        // case "3":
                        //     EditTimeSlot();
                        //     break;
                        // case "4":
                        //     DeleteTimeSlot();
                        //     break;
                        case "3": //THIS IS A TEMPORARY PART WHICH WILL BE REPLACED WITH THE STUFF ABOVE THIS LINE
                            DeleteTimeSlot();
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
        Console.Clear();
        PrintHeader();
        TimeSlotLogic.PrintTimeSlots();
        Console.WriteLine("====================================================================\n\n");
        Console.WriteLine("Press ENTER to exit");
        Console.ReadLine();
    }

    private static void CreateNewTimeSlot() {
        Console.Clear();
        PrintHeader();
        Console.WriteLine("\nDate for the new timeslot:");
        string date = Functions.RequestValidDate();
        string starttime = Functions.RequestValidTime24("Start time");
        string endtime = Functions.RequestValidTime24("End time");
        if (TimeSlotLogic.CreateTimeslot(date, starttime, endtime)) {
            Console.WriteLine("Timeslot created, returning to timeslot menu");
        } else {
            Console.WriteLine("There was an problem while trying to create you timeslot, please try again later");
        }
        Thread.Sleep(1500);
    }

    private static void DeleteTimeSlot() {
        Console.Clear();
        PrintHeader();
        TimeSlotLogic.PrintTimeSlots();
        Console.WriteLine("====================================================================\n\n");
        string TimeSlotID = TimeSlotLogic.GetSelectedTimeSlot();
        if (TimeSlotLogic.DeleteTimeSlot(TimeSlotID)) {
            Console.WriteLine("Timeslot deleted, returning to timeslot menu");
        } else {
            Console.WriteLine("There was an error while trying to delete the selected timeslot, please try again later");
        }
        Thread.Sleep(1500);
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