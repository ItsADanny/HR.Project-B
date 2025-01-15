public static class TimeSlotLogic {

    public static List<ReservationTimeSlots> GetTimeSlots(int restaurantID) {
        return Database.SelectReservationTimeSlots(restaurantID);
    }

    public static bool CreateTimeslot(string date, string startTime, string endTime) {
        return Database.Insert(new ReservationTimeSlots(0, date, startTime, endTime));
    }

    public static bool CreateTimeslot (int restaurantID, string date, string startTime, string endTime, Accounts LoggedInAccount) {
            return Database.Insert(new ReservationTimeSlots(restaurantID, date, startTime, endTime));
    }

    public static string GetSelectedTimeSlot_Reservation() {
        List<ReservationTimeSlots> timeSlots = Database.SelectReservationTimeSlots();
        while (true) {
            foreach (ReservationTimeSlots timeslot in timeSlots) {
                Console.WriteLine($"date: {timeslot.GetDate()} from {timeslot.GetStartTime24()} until {timeslot.GetEndTime24()}");
            }
            Display.PrintText("Please select a Timeslot you want to book:");
            string input = Console.ReadLine();

            foreach (ReservationTimeSlots timeslot in timeSlots) {
                if (input == timeslot.ID.ToString()) {
                    return input;
                }
            }

            Display.PrintText($"Invalid input, please input a valid timeslot number");
            Thread.Sleep(1500);
        }
    }

    public static string GetSelectedTimeSlot() {
        List<ReservationTimeSlots> timeSlots = Database.SelectReservationTimeSlots();
        while (true) {
            Display.PrintText("Please select a Timeslot you want to delete:");
            string input = Console.ReadLine();

            foreach (ReservationTimeSlots timeslot in timeSlots) {
                if (input == timeslot.ID.ToString()) {
                    return input;
                }
            }

            Display.PrintText($"Invalid input, please input a valid timeslot number");
            Thread.Sleep(1500);
        }
    }

    public static ReservationTimeSlots GetSelectedTimeSlot(int timeslotID) => Database.SelectReservationTimeSlot(timeslotID);

    public static bool DeleteTimeSlot(string TimeSlotID, Accounts LoggedinAccounts) {
        return Database.DeleteTimeSlot(TimeSlotID);
    }

    public static bool DeleteTimeSlot(int TimeSlotID, Accounts LoggedinAccounts) {
        return Database.DeleteTimeSlot(TimeSlotID);
    }

    public static List<List<string>> GenerateTimeSlots(TimeOnly starttimeday, TimeOnly timeslotSize, int timeslotAmount) {
        List<List<string>> returnValue = new ();

        TimeOnly nextTime = starttimeday;
        for (int i = 0; i != timeslotAmount; i++) {
            // Console.WriteLine($"GenerateTimeSlots {i}");
            // Thread.Sleep(1500);

            List<string> addValue = new ();

            addValue.Add(nextTime.ToString("HH:mm"));
            nextTime = nextTime.Add(timeslotSize.ToTimeSpan());
            addValue.Add(nextTime.ToString("HH:mm"));
            
            returnValue.Add(addValue);
        }

        return returnValue;
    }

    public static int GetIDFromDisplayString(string DisplayString, int restaurantID) {
        int foundID = 0;
        List<ReservationTimeSlots> timeslots = GetTimeSlots(restaurantID);
        foreach (ReservationTimeSlots timeslot in timeslots) {
            if ($"{timeslot.GetDate()} - from {timeslot.GetStartTime24()} to {timeslot.GetEndTime24()}" == DisplayString) {
                foundID = timeslot.ID;
            }
        }
        return foundID;
    }

    public static List<ReservationTimeSlots> FilterUpcoming(List<ReservationTimeSlots> TimeSlotsInput, bool filterForTodayOnly) {
        List<ReservationTimeSlots> returnValue = new();
        DateTime currDateTime = DateTime.Now;
        foreach (ReservationTimeSlots timeslot in TimeSlotsInput) {
            if (filterForTodayOnly) {
                if (timeslot.StartDateTime >= currDateTime && timeslot.StartDateTime.Date == currDateTime.Date) {
                    returnValue.Add(timeslot);
                }
            } else {
                if (timeslot.StartDateTime >= currDateTime) {
                    returnValue.Add(timeslot);
                }
            }
        }
        return returnValue;
    }

    public static string ToDisplayString(ReservationTimeSlots reservationTimeSlots) => $"{reservationTimeSlots.GetDate()} - from {reservationTimeSlots.GetStartTime24()} to {reservationTimeSlots.GetEndTime24()}";

    public static List<string> ToDisplayString(List<ReservationTimeSlots> reservationTimeSlots) {
        List<string> returnValue = new ();
        foreach (ReservationTimeSlots timeslot in reservationTimeSlots) {
            returnValue.Add(ToDisplayString(timeslot));
        }
        return returnValue;
    }
}