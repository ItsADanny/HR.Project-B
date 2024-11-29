public static class ReservationLogic {
    public static string RetrieveReservations(int restaurantID) 
    {
        string returnResult = "";
        int i = 0;

        //grab reserv list
        List<Reservations> reservations = Database.SelectReservations(restaurantID);

        foreach (Reservations reservation in reservations) 
        {
            i++;
            //grab reservation timeslot
            ReservationTimeSlots reservationTimeSlot = Database.SelectReservationTimeSlot(reservation.TimeSlotID);
            //retrieve acc info
            Accounts accounts = Database.SelectAccount(reservation.AccountID);

            string reservationStatus = "";
            
            //check actual status
            if (reservation.Status == 1) 
            {
                reservationStatus = "CANCELLED";
            }
            
            //add enter to every line
            if (returnResult != "") 
            {
                returnResult += "\n";
            }

            //format return
            returnResult += $"{i} - Reservation for: {accounts.FirstName} {accounts.LastName}, Table: {reservation.TableID}, Date: {reservationTimeSlot.GetDate()}, from {reservationTimeSlot.GetStartTime24()} to {reservationTimeSlot.GetEndTime24()}";
        }
        return returnResult;
    }

    public static string RetrieveReservations(int restaurantID, int AccountID) {
        string returnResult = "";
        int i = 0;

        //grab reserv list
        List<Reservations> reservations = Database.SelectReservations(restaurantID, AccountID);

        foreach (Reservations reservation in reservations) 
        {
            i++;
            //grab reservation timeslot
            ReservationTimeSlots reservationTimeSlot = Database.SelectReservationTimeSlot(reservation.TimeSlotID);
            //retrieve acc info
            Accounts accounts = Database.SelectAccount(reservation.AccountID);

            string reservationStatus = "";
            
            //check actual status
            if (reservation.Status == 1) 
            {
                reservationStatus = "CANCELLED";
            }
            
            //add enter to every line
            if (returnResult != "") 
            {
                returnResult += "\n";
            }
            //format return
            returnResult += $"{i} - Reservation: {reservation.ID}, Table: {reservation.TableID}, Date: {reservationTimeSlot.GetDate()}, from {reservationTimeSlot.GetStartTime24()} to {reservationTimeSlot.GetEndTime24()}";
        }
        return returnResult;
    }

    public static bool CreateReservation(int restaurantID, int TimeSlotID, int AccountID, int table) {
        return Database.Insert(new Reservations(restaurantID, TimeSlotID, table, AccountID, 0));
    }

    public static bool CancelReservation(int timeslotID, Accounts LoggedAccount) {
        //1 defines that a reservation has been cancelled
        return Database.UpdateReservationStatus(timeslotID, 1);
    }
}