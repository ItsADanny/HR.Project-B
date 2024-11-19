public static class TimeSlotLogic {
    public static void PrintTimeSlots() {
        foreach (ReservationTimeSlots reservationTimeSlots in Database.SelectReservationTimeSlots()) {
            Console.WriteLine($"{reservationTimeSlots.ID} - Start time:{reservationTimeSlots.StartDateTime.ToString("HH:mm")}, End time:{reservationTimeSlots.EndDateTime.ToString("HH:mm")}");
        }
    }

    public static bool CreateTimeslot(string date, string startTime, string endTime) {
        return Database.Insert(new ReservationTimeSlots(0, date, startTime, endTime));
    }

    public static string GetSelectedTimeSlot_Reservation() {
        List<ReservationTimeSlots> timeSlots = Database.SelectReservationTimeSlots();
        while (true) {
            foreach (ReservationTimeSlots timeslot in timeSlots) {
                Console.WriteLine($"{timeslot.ID} - date: {timeslot.GetDate()} from {timeslot.GetStartTime24()} until {timeslot.GetEndTime24()}");
            }
            Display.PrintText("Please select an Timeslot you want to book:");
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
            Display.PrintText("Please select an Timeslot you want to delete:");
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

    public static bool DeleteTimeSlot(string TimeSlotID) {
        return Database.DeleteTimeSlot(TimeSlotID);
    }
}