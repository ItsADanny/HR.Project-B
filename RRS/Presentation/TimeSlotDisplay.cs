public static class TimeSlotDisplay {

    private readonly static string Header = "====================================================================\n▗▄▄▄▖▗▄▄▄▖▗▖  ▗▖▗▄▄▄▖ ▗▄▄▖▗▖    ▗▄▖▗▄▄▄▖▗▄▄▖\n  █    █  ▐▛▚▞▜▌▐▌   ▐▌   ▐▌   ▐▌ ▐▌ █ ▐▌   \n  █    █  ▐▌  ▐▌▐▛▀▀▘ ▝▀▚▖▐▌   ▐▌ ▐▌ █  ▝▀▚▖\n  █  ▗▄█▄▖▐▌  ▐▌▐▙▄▄▖▗▄▄▞▘▐▙▄▄▖▝▚▄▞▘ █ ▗▄▄▞▘\n====================================================================";
    
    public static void Menu(int restaurantID, Accounts LoggedInAccount) {
        List<string> options = ["View timeslots"];
        if (AccountLogic.CanDisplay("changeTimeslots", LoggedInAccount)) {
            options.Add("Create a new timeslot");
            options.Add("Delete timeslot");
        }
        options.Add("Exit");

        switch(Functions.OptionSelector(Header, options)) {
            case "View timeslots":
                View(restaurantID);
                break;
            case "Create a new timeslot":
                Create(restaurantID, LoggedInAccount);
                break;
            case "Delete timeslot":
                Delete(restaurantID, LoggedInAccount);
                break;
            case "Exit":
                break;
        }
    }
    
    private static void View(int restaurantID) {
        Console.Clear();
        Console.WriteLine(Header);
        foreach (ReservationTimeSlots timeslot in TimeSlotLogic.FilterUpcoming(TimeSlotLogic.GetTimeSlots(restaurantID), false)) {
            Console.WriteLine($"{timeslot.ID} - date: {timeslot.GetDate()} from {timeslot.GetStartTime24()} until {timeslot.GetEndTime24()}");
        }
        Console.WriteLine("====================================================================\n\n");
        Console.WriteLine("Press ENTER to exit");
        Console.ReadLine();
    }

    private static void Create(int restaurantID, Accounts LoggedInAccount) {
        Console.Clear();
        Console.WriteLine(Header);
        Console.WriteLine("\nDate for the new timeslot:");
        string date = Functions.RequestValidDate();
        if (Functions.OptionSelector($"{Header}\nDate for the new timeslot:\n{date}", ["Yes", "No"]) == "Yes") {
            TimeOnly startTimeDay = Functions.TimeSelector("Please select the opening time of the restaurant\n");
            TimeOnly TimeSlotSize = Functions.TimeSelector("Please select an timeslot size\n");
            Console.Clear();
            int timeslotAmount = Functions.RequestValidInt("Timeslot amount (Max 5)", 1, 5);
            Console.Clear();
            List<List<string>> timeslots = TimeSlotLogic.GenerateTimeSlots(startTimeDay, TimeSlotSize, timeslotAmount);
            Dictionary<string, bool> successes = new ();

            foreach (List<string> timeslot in timeslots) {
                string starttime = timeslot[0];
                string endtime = timeslot[1];
                string timeslot_str = $"{date} - start time: {starttime}, end time: {endtime}";
                
                if (TimeSlotLogic.CreateTimeslot(restaurantID, date, starttime, endtime, LoggedInAccount)) {
                    successes.Add(timeslot_str, true);
                } else {
                    successes.Add(timeslot_str, false);
                }
            }

            List<string> failed_timeslots = [];
            int succes = 0;
            int failures = 0;

            foreach (KeyValuePair<string, bool> row in successes) {
                if (row.Value) {
                    succes++;
                } else {
                    failures++;
                    failed_timeslots.Add(row.Key);
                }
            }

            if (failures > 0) {
                Console.WriteLine($"\x1b[32m{succes}\x1b[39m timeslots succesfully created");
                Console.WriteLine($"\x1b[31m{failures}\x1b[39m timeslots had an erro while trying to be created\nHere is an list of the uncreated timeslots:\n");
                foreach (string failed_timeslot in failed_timeslots) {
                    Console.WriteLine("");
                }
            } else {
                Console.WriteLine($"\x1b[32m{succes}\x1b[39m timeslots succesfully created, returning to timeslot menu");
            }

        } else {
            string starttime = Functions.RequestValidTime24("Start time");
            string endtime = Functions.RequestValidTime24("End time");

            if (TimeSlotLogic.CreateTimeslot(date, starttime, endtime)) {
                Console.WriteLine("Timeslot created, returning to timeslot menu");
            } else {
                Console.WriteLine("There was a problem while trying to create your timeslot, please try again later");
            }
        }

        Thread.Sleep(1500);
    }

    private static void Delete(int restaurantID, Accounts LoggedInAccount) {
        string header = Header + "\n\nPlease select the timeslots you want to delete:";
        Dictionary<string, bool> slotsToDelete = Functions.CheckBoxSelector(header, TimeSlotLogic.ToDisplayString(TimeSlotLogic.FilterUpcoming(TimeSlotLogic.GetTimeSlots(restaurantID), false)));
        
        foreach (KeyValuePair<string, bool> row in slotsToDelete) {
            if (row.Value) {
                int timeslotID = TimeSlotLogic.GetIDFromDisplayString(row.Key, restaurantID);
                if (TimeSlotLogic.DeleteTimeSlot(timeslotID, LoggedInAccount)) {
                    if (ReservationLogic.CancelReservation(timeslotID, LoggedInAccount)) {
                        Console.WriteLine($"Timeslot \"{row.Key}\" deleted, associated reservations have been cancelled");
                    } else {
                        Console.WriteLine($"Timeslot \"{row.Key}\" deleted, but there was an error while trying to cancel associated reservations.\nPlease cancel these reservations manually");
                    }
                } else {
                    Console.WriteLine($"There was an error while trying to delete timeslot \"{row.Key}\", please try again later");
                }
            }
        }

        Thread.Sleep(1500);
        Console.WriteLine("====================================================================\n\nPress any key to continue");
        Console.ReadKey();
    }
}