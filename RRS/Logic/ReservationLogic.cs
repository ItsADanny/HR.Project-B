public static class ReservationLogic {
    public static string RetrieveReservations(int restaurantID) {
        string returnValue = "";

        int i = 0;
        List<Reservations> reservations = Database.SelectReservations(restaurantID);
        foreach (Reservations reservation in reservations) {
            i++;
            ReservationTimeSlots reservationTimeSlot = Database.SelectReservationTimeSlots(reservation.TimeSlotID);
            Accounts accounts = Database.SelectAccount(reservation.AccountID);

            string reservation_status = "";
            if (reservation.Status == 1) {
                reservation_status = "CANCELLED";
            }

            if (returnValue != "") {
                returnValue += "\n";
            }

            returnValue += $"{i} - Reservation for: {accounts.FirstName} {accounts.LastName}, Table: {reservation.TableID}, Date: {reservationTimeSlot.GetDate()}, from {reservationTimeSlot.GetStartTime()} to {reservationTimeSlot.GetEndTime()}";
        }

        return returnValue;
    }

    public static bool CreateReservation(int restaurantID, string firstName, string lastName, string email, string phoneNumber, string date, string startTime, string endTime, int table) {
        Accounts account = new (email, firstName, lastName, phoneNumber, 3);
        ReservationTimeSlots timeslot = new ReservationTimeSlots(restaurantID, date, startTime, endTime);
        
        Database.Insert(account);
        Database.Insert(timeslot);

        int DB_accountID = Database.Temp_GetLastID("Accounts");
        int DB_TimeslotID = Database.Temp_GetLastID("ReservationTimeSlots");

        Reservations reservation = new Reservations(restaurantID, DB_TimeslotID, table, DB_accountID, 0);

        return Database.Insert(reservation);
    }
}